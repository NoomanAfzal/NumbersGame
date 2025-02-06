// dnSpy decompiler from Assembly-CSharp.dll class: TopPanel
using System;
using UnityEngine;
public class TopPanel : BasePanel
{
	protected new void Start()
	{
		base.Start();
	}
	public Game game;
	protected new void OnEnable()
	{
		base.OnEnable();
		if (!GameUser.Instance.forceTheme)
		{
			this.waiRotTim = 5f;
		}
	}
	
    public void ShowRewardedvideo()
    {
	    //noman you commented this line of adsss
//        GoogleAdMobController.instance.ShowRewardedAd(OnRandomReward);

		
    }
	public void SetGameScriptToTrue()
	{
		game.enabled = true;
	}
	/*public void OnRandomReward(Reward reward)
	{
		
		game.enabled = false;
		Base._instance.randomRewardindex = UnityEngine.Random.Range(0, 3);
		Base._instance.randomRewardCount = UnityEngine.Random.Range(2, 4);
		switch (Base._instance.randomRewardindex)
		{
			case 0:
				RewardScriptableObject.instance.tipRemoveCount += Base._instance.randomRewardCount;
				UIManager.selfInstance.rewardPopup.bodyText.text = "You've Got " + Base._instance.randomRewardCount + " Remove Block Tips";
				UIManager.selfInstance.rewardPopup.tipImg.sprite = UIManager.selfInstance.rewardPopup.removeSpr;

				break;
			case 1:
				RewardScriptableObject.instance.tipLightCount += Base._instance.randomRewardCount;
				UIManager.selfInstance.rewardPopup.bodyText.text = "You've Got " + Base._instance.randomRewardCount + " Hints";
				UIManager.selfInstance.rewardPopup.tipImg.sprite = UIManager.selfInstance.rewardPopup.lightSpr;

				break;
			case 2:
				RewardScriptableObject.instance.tipUndoCount += Base._instance.randomRewardCount;
				UIManager.selfInstance.rewardPopup.bodyText.text = "You've Got " + Base._instance.randomRewardCount + " Undo Blocks";
				UIManager.selfInstance.rewardPopup.tipImg.sprite = UIManager.selfInstance.rewardPopup.undoSpr;

				break;
		}
		UIManager.selfInstance.rewardPopup.gameObject.SetActive(true);
		Base._instance.UpdateCount();


	}*/
	public void IAPhint()
	{
		RewardScriptableObject.instance.tipLightCount += 5;
        Base._instance.UpdateCount();

    }
	public void IAPRemoveCount()
	{
        RewardScriptableObject.instance.tipRemoveCount += 5;
        Base._instance.UpdateCount();

    }
	public void IAPUndoCount()
	{
        RewardScriptableObject.instance.tipUndoCount += 5;
        Base._instance.UpdateCount();

    }
	public void Claim(int amount)
	{
		RewardScriptableObject.instance.tipRemoveCount += amount;
		

    }

 public void Adremove()
        {
            PlayerPrefs.SetInt("removeads", 1);
        }


    protected new void OnDisable()
	{
		base.OnDisable();
	}

	public void UpdateThemeBtn(float y)
	{
		RectTransform rectTransform = this.themeParentAnim.transform as RectTransform;
		Vector2 anchoredPosition = rectTransform.anchoredPosition;
		anchoredPosition.y = y;
		rectTransform.anchoredPosition = anchoredPosition;

        RectTransform rectTransform2 = this.videoParent.transform as RectTransform;
        Vector2 anchoredPosition2 = rectTransform2.anchoredPosition;
        anchoredPosition2.y = y;
        //rectTransform2.anchoredPosition = anchoredPosition2; //noman commented this line 
    }

	public void SwitchThemeBtn(bool isShow)
	{
		if (isShow)
		{
			this.themeParentAnim.SetInteger("AnimState", 0);
		}
		else
		{
			this.themeParentAnim.SetInteger("AnimState", 1);
		}
	}

	public void OnTheme()
	{
		if (this.isEnd)
		{
			return;
		}
		AudioSystem.PlayOneShotEffect("btn");
		GameUser instance = GameUser.Instance;
		instance.nowTheme++;
		instance.nowTheme = ((instance.nowTheme < UIManager.selfInstance.bkgPanel.themeCount) ? instance.nowTheme : 0);
		UIManager.selfInstance.bkgPanel.SelectTheme(instance.nowTheme);
		DateTime now = DateTime.Now;
		if (!instance.forceChristmas && ((now.Month >= 12 && now.Day >= 22) || (now.Month <= 1 && now.Day <= 3)))
		{
			instance.forceChristmas = true;
		}
		instance.forceTheme = true;
		UIManager.selfInstance.menuPanel.SwitchSpTheme();
		GameUser.Save();
		if (!UIManager.selfInstance.revivePanel.gameObject.activeSelf)
		{
			this.DelayNgs(0.32f);
		}
	}

   

	internal void DelayNgs(float delayTim)
	{
		
	}

	private void InvokeAds()
	{
		
	}

	private new void Update()
	{
		if (this.waiRotTim > 0f)
		{
			this.waiRotTim -= Time.deltaTime;
			if (this.waiRotTim <= 0f)
			{
				this.waiRotTim = -1f;
				if (!GameUser.Instance.forceTheme)
				{
					this.rotTim = 1f;
				}
			}
		}
		else if (this.rotTim > 0f)
		{
			this.rotTim -= Time.deltaTime * 1f;
			Vector3 localEulerAngles = this.themeIcon.localEulerAngles;
			localEulerAngles.z = Mathf.Lerp(-360f, 0f, this.rotTim);
			if (this.rotTim <= 0f)
			{
				localEulerAngles.z = 0f;
				this.rotTim = -1f;
				this.waiRotTim = UnityEngine.Random.Range(30f, 60f);
			}
			this.themeIcon.localEulerAngles = localEulerAngles;
		}
	}

	public Animator themeParentAnim;

    public GameObject videoParent;
	

	public RectTransform themeIcon;

	private float waiRotTim = -1f;

	private float rotTim = -1f;
}