using UnityEngine;
using System;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;

namespace NiobiumStudios
{
    /**
     * Daily Rewards keeps track of the player daily rewards based on the time they last claimed a reward.
     **/
    public class DailyRewards : DailyRewardsCore<DailyRewards>
    {
        public List<Reward> rewards;        // Rewards list 
        public DateTime lastRewardTime;     // The last time the user claimed a reward
        public int availableReward;         // The reward position available to claim
        public int lastReward;              // The last reward the player claimed
        public bool keepOpen = true;        // Keep the UI open even when there are no rewards available?

        // Delegates
        public delegate void OnClaimPrize(int day);
        public OnClaimPrize onClaimPrize;

        public delegate void OnInitialize(bool error, string errorMessage);
        public OnInitialize onInitialize;

        // Needed constants.
        private const string LAST_REWARD_TIME = "LastRewardTime";
        private const string LAST_REWARD = "LastReward";
        private const string DEBUG_TIME = "DebugTime";
        private const string FMT = "O";

        public TimeSpan debugTime;         // For debug purposes only

        void Start()
        {
            // Initialize the timer.
            InitializeTimer();
        }

        private void InitializeTimer()
        {
            base.InitializeDate();

            if (base.isErrorConnect)
            {
                if (onInitialize != null)
                    onInitialize(true, base.errorMessage);
            }
            else
            {
                LoadDebugTime();
                CheckRewards();

                if (onInitialize != null)
                    onInitialize(false, "");
            }
        }

        protected override void OnApplicationPause(bool pauseStatus)
        {
            base.OnApplicationPause(pauseStatus);
            CheckRewards();
        }

        // Returns the time difference until the next reward is available.
        public TimeSpan GetTimeDifference()
        {
            TimeSpan difference = (lastRewardTime - now);
            difference = difference.Subtract(debugTime);
            return difference.Add(new TimeSpan(0, 24, 0, 0));
        }

        private void LoadDebugTime()
        {
            int debugHours = PlayerPrefs.GetInt(GetDebugTimeKey(), 0);
            debugTime = new TimeSpan(debugHours, 0, 0);
        }

        // Checks if the player can claim a reward.
        public void CheckRewards()
        {
            string lastClaimedTimeStr = PlayerPrefs.GetString(GetLastRewardTimeKey());
            lastReward = PlayerPrefs.GetInt(GetLastRewardKey());

            // If a claim was previously made...
            if (!string.IsNullOrEmpty(lastClaimedTimeStr))
            {
                lastRewardTime = DateTime.ParseExact(lastClaimedTimeStr, FMT, CultureInfo.InvariantCulture);

                // Use debug time to simulate advanced time.
                DateTime advancedTime = now.AddHours(debugTime.TotalHours);
                TimeSpan diff = advancedTime - lastRewardTime;
                Debug.Log("Last claim was " + (long)diff.TotalHours + " hours ago.");

                int days = (int)(Math.Abs(diff.TotalHours) / 24);
                if (days == 0)
                {
                    // Not enough time passed.
                    availableReward = 0;
                    return;
                }

                // If between 1 and 2 days have passed...
                if (days >= 1 && days < 2)
                {
                    // If at the end of the cycle, reset to the first reward.
                    if (lastReward == rewards.Count)
                    {
                        availableReward = 1;
                        lastReward = 0;
                        return;
                    }

                    availableReward = lastReward + 1;
                    Debug.Log("Player can claim prize " + availableReward);
                    return;
                }

                // If 2 or more days have passed, reset rewards.
                if (days >= 2)
                {
                    availableReward = 1;
                    lastReward = 0;
                    Debug.Log("Prize reset");
                }
            }
            else
            {
                // First time: set the first reward as available.
                availableReward = 1;
            }
        }

        // Called when the player claims a reward.
        public void ClaimPrize()
        {
            if (availableReward > 0)
            {
                if (onClaimPrize != null)
                    onClaimPrize(availableReward);

                Debug.Log("Reward [" + rewards[availableReward - 1] + "] Claimed!");
                PlayerPrefs.SetInt(GetLastRewardKey(), availableReward);

                string lastClaimedStr = now.AddHours(debugTime.TotalHours).ToString(FMT);
                PlayerPrefs.SetString(GetLastRewardTimeKey(), lastClaimedStr);
                PlayerPrefs.SetInt(GetDebugTimeKey(), (int)debugTime.TotalHours);
            }
            else if (availableReward == 0)
            {
                Debug.LogError("Error! The player is trying to claim the same reward twice.");
            }

            CheckRewards();
        }

        // Returns the key used to store the last reward in PlayerPrefs.
        private string GetLastRewardKey()
        {
            return LAST_REWARD;
        }

        // Returns the key used to store the last reward time in PlayerPrefs.
        private string GetLastRewardTimeKey()
        {
            return LAST_REWARD_TIME;
        }

        // Returns the key used to store the debug time in PlayerPrefs.
        private string GetDebugTimeKey()
        {
            return DEBUG_TIME;
        }

        // Returns the reward corresponding to a given day.
        public Reward GetReward(int day)
        {
            return rewards[day - 1];
        }

        // Resets the daily rewards (for testing purposes).
        public void Reset()
        {
            PlayerPrefs.DeleteKey(GetLastRewardKey());
            PlayerPrefs.DeleteKey(GetLastRewardTimeKey());
            PlayerPrefs.DeleteKey(GetDebugTimeKey());
        }
    }
}