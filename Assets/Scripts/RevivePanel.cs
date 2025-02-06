// dnSpy decompiler from Assembly-CSharp.dll class: RevivePanel
using System;
using UnityEngine;
using UnityEngine.UI;
public class RevivePanel : BasePanel
{
	protected new void Start()
	{
		base.Start();
	}

    protected new void OnEnable()
    {
        base.OnEnable();
        this.isInVideo = false;
        this.timCount = 10f;
        this.labCount.text = this.timCount.ToString("N0");
        //if (GameUser.Instance.isNoAds || VectorAds.invokeAds(106, string.Empty) == "true" || VectorAds.invokeAds(104, string.Empty) == "true")
        //{
        this.imgVideo.sprite = this.spVideoStatus[0];
        //}
        //else
        //{
        //this.imgVideo.sprite = this.spVideoStatus[1];
        //}
        KeyBoardListener kbl = UIManager.selfInstance.kbl;
        kbl.onBackKeyEvent = (KeyBoardListener.OnBackKeyEvent)Delegate.Combine(kbl.onBackKeyEvent, new KeyBoardListener.OnBackKeyEvent(this.OnBackKeyEvent));
    }

	protected new void OnDisable()
	{
		base.OnDisable();
		KeyBoardListener kbl = UIManager.selfInstance.kbl;
		kbl.onBackKeyEvent = (KeyBoardListener.OnBackKeyEvent)Delegate.Remove(kbl.onBackKeyEvent, new KeyBoardListener.OnBackKeyEvent(this.OnBackKeyEvent));
	}

	private void OnBackKeyEvent()
	{
		this.OnClose(true);
	}

	public void OnClose(bool isHand = false)
	{
		if (this.isEnd || this.isInVideo)
		{
			return;
		}
		if (isHand)
		{
			AudioSystem.PlayOneShotEffect("btn");
		}
		base.NextFunction(delegate
		{
			base.gameObject.SetActive(false);
			UIManager.selfInstance.gamePanel.ToPassPanel();
		});
	}

	public void OnRevive()
	{
		// Noman you commented this line of adsss
		//GoogleAdMobController.instance.ShowRewardedAd(Reward);
        //if (GameUser.Instance.isNoAds)
        //{
        //	this.isInVideo = true;
        //	AudioSystem.PlayOneShotEffect("btn");
        //	base.NextFunction(delegate
        //	{
        //		base.gameObject.SetActive(false);
        //		UIManager.selfInstance.gamePanel.GameRevive();
        //	});
        //}
        //else if (VectorAds.invokeAds(106, string.Empty) == "true")
        //{
        //	this.isInVideo = true;
        //	AudioSystem.PlayOneShotEffect("btn");
        //	VectorAds.invokeAds(109, "revive_av");
        //	VectorAdsInstance vainstance = UIManager.selfInstance.VAinstance;
        //	vainstance.mCallBack = (VectorAdsInstance.InvokeAdsCallback)Delegate.Combine(vainstance.mCallBack, new VectorAdsInstance.InvokeAdsCallback(this.InvokeAdsCallback));
        //	VectorAds.invokeAds(107, string.Empty);
        //	UIManager.selfInstance.VAinstance.ResetNgsCount();
        //}
        //else if (VectorAds.invokeAds(104, string.Empty) == "true")
        //{
        //	this.isInVideo = true;
        //	AudioSystem.PlayOneShotEffect("btn");
        //	VectorAds.invokeAds(109, "revive_ngs");
        //	VectorAdsInstance vainstance2 = UIManager.selfInstance.VAinstance;
        //	vainstance2.mCallBack = (VectorAdsInstance.InvokeAdsCallback)Delegate.Combine(vainstance2.mCallBack, new VectorAdsInstance.InvokeAdsCallback(this.InvokeAdsCallback));
        //	VectorAds.invokeAds(105, string.Empty);
        //	UIManager.selfInstance.VAinstance.ResetNgsCount();
        //}
    }
	// Noman you commented this line of adsss
	/*public void Reward(Reward reward)
	{
        UIManager.selfInstance.gamePanel.GameRevive();
		ClosePanel();

    }*/

	  private void ClosePanel()
    {
        this.isInVideo = true;
        AudioSystem.PlayOneShotEffect("btn");
        base.NextFunction(delegate
        {
            base.gameObject.SetActive(false);
        });
    }
    public void InvokeAdsCallback(int type, string msg)
	{
		if (type != 1)
		{
			if (type == 0)
			{
				base.NextFunction(delegate
				{
					base.gameObject.SetActive(false);
					UIManager.selfInstance.gamePanel.GameRevive();
				});
			}
		}
		else
		{
			if (msg == "true")
			{
				base.NextFunction(delegate
				{
					base.gameObject.SetActive(false);
					UIManager.selfInstance.gamePanel.GameRevive();
				});
			}
		}
	}

	private new void Update()
	{
		if (!this.isInVideo)
		{
			this.timCount -= Time.deltaTime;
			if (this.timCount < 0f)
			{
				this.labCount.text = string.Empty;
				this.OnClose(false);
			}
			else
			{
				this.labCount.text = this.timCount.ToString("N0");
			}
		}
	}

	public Sprite[] spVideoStatus;

	public Text labCount;

	public Image imgVideo;

	private float timCount;

	private bool isInVideo;
}