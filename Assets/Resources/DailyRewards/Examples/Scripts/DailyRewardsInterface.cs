using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using System.Collections.Generic;

namespace NiobiumStudios
{
    /**
     * The UI Logic Representation of the Daily Rewards
     **/
    public class DailyRewardsInterface : MonoBehaviour
    {
        public Canvas canvas;
        public GameObject dailyRewardPrefab;        // Prefab containing each daily reward

        [Header("Panel Debug")]
        public bool isDebug;
        public GameObject panelDebug;
        public Button buttonAdvanceDay;
        public Button buttonAdvanceHour;
        public Button buttonReset;
        public Button buttonReloadScene;

        [Header("Panel Reward Message")]
        public GameObject panelReward;              // Rewards panel
        public Text textReward;                     // Reward Text to show an explanatory message to the player
        public Button buttonCloseReward;            // The Button to close the Rewards Panel
        public Image imageReward;                   // The image of the reward

        [Header("Panel Reward")]
        public Button buttonClaim;                  // Claim Button
        public Button buttonClose;                  // Close Button
        public Button buttonCloseWindow;            // Close Button on the upper right corner
        public Text textTimeDue;                    // Text showing how long until the next claim
        public GridLayoutGroup dailyRewardsGroup;   // The Grid that contains the rewards
        public ScrollRect scrollRect;               // The Scroll Rect

        private bool readyToClaim;                  // Update flag
        private List<DailyRewardUI> dailyRewardsUI = new List<DailyRewardUI>();

        private DailyRewards dailyRewards;			// DailyReward Instance      

        void Awake()
        {
            // Initially disable the canvas.
            canvas.gameObject.SetActive(false);
            dailyRewards = GetComponent<DailyRewards>();
        }

        void Start()
        {
            // Build the UI elements first.
            InitializeDailyRewardsUI();

            // Subscribe to events after UI is built.
            dailyRewards.onClaimPrize += OnClaimPrize;
            dailyRewards.onInitialize += OnInitialize;

            // Setup debug panel.
            if (panelDebug)
                panelDebug.SetActive(isDebug);

            buttonClose.gameObject.SetActive(false);

            // Claim button calls ClaimPrize, then updates the UI.
            buttonClaim.onClick.AddListener(() =>
            {
                dailyRewards.ClaimPrize();
                readyToClaim = false;
                UpdateUI();
            });

            // Closes the reward panel.
            buttonCloseReward.onClick.AddListener(() =>
            {
                bool keepOpen = dailyRewards.keepOpen;
                panelReward.SetActive(false);
                canvas.gameObject.SetActive(keepOpen);
            });

            buttonClose.onClick.AddListener(() =>
            {
                canvas.gameObject.SetActive(false);
            });

            buttonCloseWindow.onClick.AddListener(() =>
            {
                canvas.gameObject.SetActive(false);
            });

            // Simulate next day.
            if (buttonAdvanceDay)
                buttonAdvanceDay.onClick.AddListener(() =>
                {
                    dailyRewards.debugTime = dailyRewards.debugTime.Add(new TimeSpan(1, 0, 0, 0));
                    UpdateUI();
                });

            // Simulate next hour.
            if (buttonAdvanceHour)
                buttonAdvanceHour.onClick.AddListener(() =>
                {
                    dailyRewards.debugTime = dailyRewards.debugTime.Add(new TimeSpan(1, 0, 0));
                    UpdateUI();
                });

            // Reset rewards.
            if (buttonReset)
                buttonReset.onClick.AddListener(() =>
                {
                    dailyRewards.Reset();
                    dailyRewards.debugTime = new TimeSpan();
                    dailyRewards.lastRewardTime = DateTime.MinValue;
                    readyToClaim = false;
                });

            // Reload scene.
            if (buttonReloadScene)
                buttonReloadScene.onClick.AddListener(() =>
                {
                    Application.LoadLevel(Application.loadedLevelName);
                });

            UpdateUI();
        }

        void OnDestroy()
        {
            // Unsubscribe from events.
            if (dailyRewards != null)
            {
                dailyRewards.onClaimPrize -= OnClaimPrize;
                dailyRewards.onInitialize -= OnInitialize;
            }
        }

        // Initializes the UI list based on the number of rewards.
        private void InitializeDailyRewardsUI()
        {
            dailyRewardsUI.Clear();

            for (int i = 0; i < dailyRewards.rewards.Count; i++)
            {
                int day = i + 1;
                var reward = dailyRewards.GetReward(day);

                GameObject dailyRewardGo = Instantiate(dailyRewardPrefab);
                DailyRewardUI dailyRewardUI = dailyRewardGo.GetComponent<DailyRewardUI>();

                // Set parent with 'false' to preserve the prefab's scale.
                dailyRewardUI.transform.SetParent(dailyRewardsGroup.transform, false);

                dailyRewardUI.day = day;
                dailyRewardUI.reward = reward;
                dailyRewardUI.Initialize();

                dailyRewardsUI.Add(dailyRewardUI);
            }
        }

        public void UpdateUI()
        {
            dailyRewards.CheckRewards();

            bool isRewardAvailableNow = false;
            int lastRewardClaimed = dailyRewards.lastReward;
            int availableReward = dailyRewards.availableReward;

            // Update each UI element.
            foreach (var dailyRewardUI in dailyRewardsUI)
            {
                int day = dailyRewardUI.day;

                if (day == availableReward)
                {
                    dailyRewardUI.state = DailyRewardUI.DailyRewardState.UNCLAIMED_AVAILABLE;
                    isRewardAvailableNow = true;
                }
                else if (day <= lastRewardClaimed)
                {
                    dailyRewardUI.state = DailyRewardUI.DailyRewardState.CLAIMED;
                }
                else
                {
                    dailyRewardUI.state = DailyRewardUI.DailyRewardState.UNCLAIMED_UNAVAILABLE;
                }

                dailyRewardUI.Refresh();
            }

            // Set button visibility.
            buttonClaim.gameObject.SetActive(isRewardAvailableNow);
            buttonClose.gameObject.SetActive(!isRewardAvailableNow);

            if (isRewardAvailableNow)
            {
                SnapToReward();
                textTimeDue.text = "You can claim your reward!";
            }

            readyToClaim = isRewardAvailableNow;
        }

        // Scrolls the ScrollRect so that the target reward is visible.
        public void SnapToReward()
        {
            // Guard: do nothing if the UI list is empty.
            if (dailyRewardsUI == null || dailyRewardsUI.Count == 0)
            {
                Debug.LogWarning("DailyRewardsUI list is empty. SnapToReward aborted.");
                return;
            }

            Canvas.ForceUpdateCanvases();

            // Use availableReward if set, otherwise use lastReward (or default to day 1).
            int targetDay = dailyRewards.availableReward > 0 ? dailyRewards.availableReward :
                            (dailyRewards.lastReward > 0 ? dailyRewards.lastReward : 1);

            // Convert the 1-based day to a 0-based index.
            int targetIndex = Mathf.Clamp(targetDay - 1, 0, dailyRewardsUI.Count - 1);

            var target = dailyRewardsUI[targetIndex].GetComponent<RectTransform>();
            var content = scrollRect.content;
            float normalizedPosition = (float)target.GetSiblingIndex() / (float)content.childCount;
            scrollRect.verticalNormalizedPosition = normalizedPosition;
        }

        void Update()
        {
            dailyRewards.TickTime();
            CheckTimeDifference();
        }

        private void CheckTimeDifference()
        {
            if (!readyToClaim)
            {
                TimeSpan difference = dailyRewards.GetTimeDifference();

                // If the countdown reaches zero, update the UI.
                if (difference.TotalSeconds <= 0)
                {
                    readyToClaim = true;
                    UpdateUI();
                    SnapToReward();
                    return;
                }

                string formattedTs = dailyRewards.GetFormattedTime(difference);
                textTimeDue.text = string.Format(" Next Reward in \n {0} ", formattedTs);
            }
        }

        // Called when the player claims a reward.
        private void OnClaimPrize(int day)
        {
            panelReward.SetActive(true);

            var reward = dailyRewards.GetReward(day);
            var unit = reward.unit;
            var rewardQt = reward.reward;
            imageReward.sprite = reward.sprite;
            if (rewardQt > 0)
            {
                textReward.text = string.Format("You got {0} {1}!", reward.reward, unit);
            }
            else
            {
                textReward.text = string.Format("You got {0}!", unit);
            }
            RewardScriptableObject.instance.tipRemoveCount += rewardQt;
            Base._instance.UpdateCount();
        }

        // Called when DailyRewards finishes initialization.
        private void OnInitialize(bool error, string errorMessage = "")
        {
            // If the UI wasn’t built for some reason, build it now.
            if (dailyRewardsUI.Count == 0)
            {
                InitializeDailyRewardsUI();
            }

            if (!error)
            {
                bool showWhenNotAvailable = dailyRewards.keepOpen;
                bool isRewardAvailable = dailyRewards.availableReward > 0;

                UpdateUI();
                canvas.gameObject.SetActive(showWhenNotAvailable || (!showWhenNotAvailable && isRewardAvailable));
                SnapToReward();
                CheckTimeDifference();
            }
            else
            {
                Debug.LogError("Initialization error: " + errorMessage);
            }
        }
    }
}