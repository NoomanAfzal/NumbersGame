// dnSpy decompiler from Assembly-CSharp.dll class: SettingPanel
using System;
using System.Collections;
using com.vector;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : BasePanel
{
	protected new void Start()
	{
		base.Start();
		if (AudioSystem.isSound)
		{
			this.soundOn.SetActive(true);
			this.soundOff.SetActive(false);
		}
		else
		{
			this.soundOn.SetActive(false);
			this.soundOff.SetActive(true);
		}
		if (Application.platform == RuntimePlatform.Android)
		{
			this.rateGo.SetActive(false);
			if (Storage.ReadConfig("refusenotification", "false") == "false")
			{
				//this.notfiOnGo.SetActive(true); noman commeneted this line
				//this.notfiOffGo.SetActive(false); noman commeneted this line
			}
			else
			{
				//this.notfiOnGo.SetActive(false); noman commeneted this line
				//this.notfiOffGo.SetActive(true); noman commeneted this line
			}
		}
		else
		{
			this.rateGo.SetActive(true);
			//this.notfiOnGo.SetActive(false); noman commeneted this line
			//this.notfiOffGo.SetActive(false); noman commeneted this line
		}
	}

	public void OnShare(){
		StartCoroutine(sharedata());
	}



	IEnumerator sharedata()
	{
		yield return new WaitForEndOfFrame ();
		if (Application.platform == RuntimePlatform.Android) {
			AndroidJavaClass intentClass = new AndroidJavaClass ("android.content.Intent");
			AndroidJavaObject intentObject = new AndroidJavaObject ("android.content.Intent");
			intentObject.Call<AndroidJavaObject> ("setAction", intentClass.GetStatic<string> ("ACTION_SEND"));
			intentObject.Call<AndroidJavaObject> ("setType", "text/plain");
			intentObject.Call<AndroidJavaObject> ("putExtra", intentClass.GetStatic<string> ("EXTRA_SUBJECT"), "Amazing Game : Chicken Run");
			//intentObject.Call<AndroidJavaObject> ("putExtra", intentClass.GetStatic<string> ("EXTRA_TEXT"), "Please download this amazing game\n" + "http://play.google.com/store/apps/details?id=" + Application.identifier);
			intentObject.Call<AndroidJavaObject> ("putExtra", intentClass.GetStatic<string> ("EXTRA_TEXT"), "Please download this amazing game\n" + "http://play.google.com/store/apps/details?id=" + Application.identifier);
			AndroidJavaClass unity = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
			AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject> ("currentActivity");
			currentActivity.Call ("startActivity", intentObject);
		}
	}

	protected new void OnEnable()
	{
		base.OnEnable();
		if (Localization.getCurFlag() == "China")
		{
			this.imgNoAds.sprite = this.cnNoAds;
		}
		else
		{
			this.imgNoAds.sprite = this.enNoAds;
		}
		UIManager.selfInstance.topPanel.SwitchThemeBtn(true);
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
		this.OnClose();
	}

	public void OnAnimInAfter()
	{
		if (UIManager.selfInstance.VAinstance.adData.busyAd)
		{
			UIManager.selfInstance.VAinstance.ShowNGS(false);
		}
	}

	public void OnClose()
	{
		if (this.isEnd)
		{
			return;
		}
		AudioSystem.PlayOneShotEffect("btn");
		base.NextPanel(UIManager.selfInstance.prePanel.gameObject);
	}

	public void OnSoundSwitch(bool isOpen)
	{
		if (this.isEnd)
		{
			return;
		}
		AudioSystem.SwitchSound(isOpen);
		if (AudioSystem.isSound)
		{
			this.soundOn.SetActive(true);
			this.soundOff.SetActive(false);
		}
		else
		{
			this.soundOn.SetActive(false);
			this.soundOff.SetActive(true);
		}
		AudioSystem.PlayOneShotEffect("btn");
	}

	public void OnNotificationSwitch(bool isOpen)
	{
		if (this.isEnd)
		{
			return;
		}
		AudioSystem.PlayOneShotEffect("btn");
		if (isOpen)
		{
		//	this.notfiOnGo.SetActive(true); noman commeneted this line
			//this.notfiOffGo.SetActive(false); noman commeneted this line
			UIManager.selfInstance.menuPanel.RequestLocalNotice();
		}
		else
		{
			//this.notfiOnGo.SetActive(false);  noman commeneted this line
			//this.notfiOffGo.SetActive(true); noman commeneted this line
			VectorNative.invokeNative(124, "2248hexa_daily");
			Storage.WriteConfig("refusenotification", "true");
		}
	}

	public void OnRate()
	{
		if (this.isEnd)
		{
			return;
		}
		AudioSystem.PlayOneShotEffect("btn");
		VectorNative.invokeNative(106, string.Empty);
	}

	public void OnFbLike()
	{

		Application.OpenURL("https://play.google.com/store/apps/dev?id=4931980280043170786");



		// if (this.isEnd)
		// {
		// 	return;
		// }
		// AudioSystem.PlayOneShotEffect("btn");
		// VectorNative.invokeNative(109, string.Empty);
	}

	public void OnTwitter()
	{

 Application.OpenURL("https://www.trostun.com/privacy-policy");

		// if (this.isEnd)
		// {
		// 	return;
		// }
		// AudioSystem.PlayOneShotEffect("btn");
		// VectorNative.invokeNative(110, string.Empty);
	}

	public void OnRank()
	{ 
        Application.OpenURL("https://www.trostunapps.com/"); 


		// if (this.isEnd)
		// {
		// 	return;
		// }
		// AudioSystem.PlayOneShotEffect("btn");
		// if (!InitScript.isLoginGameCenter)
		// {
		// 	VectorNative.invokeNative(128, string.Empty);
		// 	InitScript.isLoginGameCenter = true;
		// }
		// if (InitScript.isLoginGameCenter)
		// {
		// 	string text = "CgkI_PC18bcVEAIQAA";
		// 	if (Application.platform == RuntimePlatform.IPhonePlayer)
		// 	{
		// 		text = "nlhexa_leaderboard";
		// 	}
		// 	long num = GameUser.Instance.bestRecord;
		// 	if (UIManager.selfInstance.gamePanel.targetScore > num)
		// 	{
		// 		num = UIManager.selfInstance.gamePanel.targetScore;
		// 	}
		// 	VectorNative.invokeNative(102, string.Concat(new object[]
		// 	{
		// 		"{\"category\":\"",
		// 		text,
		// 		"\",\"score\":",
		// 		num,
		// 		"}"
		// 	}));
		// 	VectorNative.invokeNative(105, string.Concat(new string[]
		// 	{
		// 		"{    \"category\": \"",
		// 		text,
		// 		"\",\"timeScope\": ",
		// 		UIManager.selfInstance.VAinstance.adData.rankScope.ToString(),
		// 		"}"
		// 	}));
		// }
	}

	

	public void OnMoreGame()
	{
		if (this.isEnd)
		{
			return;
		}
		AudioSystem.PlayOneShotEffect("btn");
		VectorNative.invokeNative(104, string.Empty);
	}

	public void OnRemoveAds()
	{
		if (this.isEnd)
		{
			return;
		}
		AudioSystem.PlayOneShotEffect("btn");
		UIManager.selfInstance.noAdsThisTime = true;
		VectorAds.invokeAds(109, "setting_noads");
		VectorNative.invokeNative(112, "nlhexa_noads");
	}

	public void OnRestore()
	{
		if (this.isEnd)
		{
			return;
		}
		AudioSystem.PlayOneShotEffect("btn");
		VectorNative.invokeNative(113, string.Empty);
	}


	public void ShopActive(){
		ShopPanel.SetActive(true);
	}

	private new void Update()
	{
	}

	public GameObject soundOn;

	public GameObject soundOff;

	public Sprite enNoAds;

	public Sprite cnNoAds;

	public Image imgNoAds;

	//public GameObject notfiOnGo; noman commented this line

	public GameObject notfiOffGo;

	public GameObject rateGo;

	public GameObject ShopPanel;

	internal const string ANDROID_LEADERBOARD = "CgkI_PC18bcVEAIQAA";

	internal const string IOS_LEADERBOARD = "nlhexa_leaderboard";

	internal const string IAP_NOADS = "nlhexa_noads";
}