// dnSpy decompiler from Assembly-CSharp.dll class: MenuPanel
using System;
using UnityEngine;
using UnityEngine.UI;

public class MenuPanel : BasePanel
{
	public Text bestScoreMainmenutxt;
	protected new void Start()
	{
		bestScoreMainmenutxt.text = PlayerPrefs.GetInt("BestScore").ToString();
		base.Start();
		GameUser instance = GameUser.Instance;
		if (!instance.forceTheme && instance.bestRecord > 0L)
		{
			UIManager.UpdateRandomState(-1);
            instance.nowTheme = UnityEngine.Random.Range(0, 2);
		}
		bool flag = false;
		DateTime now = DateTime.Now;
		if ((now.Month >= 12 && now.Day >= 22) || (now.Month <= 1 && now.Day <= 3))
		{
			if (!instance.forceChristmas && now.Month >= 12 && now.Day >= 22 && now.Day <= 25)
			{
				instance.nowTheme = 3;
			}
			flag = true;
		}
		UIManager.selfInstance.bkgPanel.SelectTheme(instance.nowTheme);
		if (instance.nowTheme == 3 && flag)
		{
			this.spTheme1.SetActive(true);
			this.commonLogo.SetActive(false);
		}
		else
		{
			this.spTheme1.SetActive(false);
			this.commonLogo.SetActive(true);
		}
    }

	public void SwitchSpTheme()
	{
		GameUser instance = GameUser.Instance;
		DateTime now = DateTime.Now;
		if (instance.nowTheme == 3 && ((now.Month >= 12 && now.Day >= 22) || (now.Month <= 1 && now.Day <= 3)))
		{
			this.spTheme1.SetActive(true);
			this.commonLogo.SetActive(false);
		}
		else
		{
			this.spTheme1.SetActive(false);
			this.commonLogo.SetActive(true);
		}
	}

	protected new void OnEnable()
	{
		base.OnEnable();
		this.alreadyShowNgs = false;
		UIManager.selfInstance.topPanel.SwitchThemeBtn(true);
		KeyBoardListener kbl = UIManager.selfInstance.kbl;
		kbl.onBackKeyEvent = (KeyBoardListener.OnBackKeyEvent)Delegate.Combine(kbl.onBackKeyEvent, new KeyBoardListener.OnBackKeyEvent(this.OnBackKeyEvent));
        soundOn.gameObject.SetActive(AudioSystem.isSound);
        soundOff.gameObject.SetActive(!AudioSystem.isSound);
    }

	protected new void OnDisable()
	{
		base.OnDisable();
		KeyBoardListener kbl = UIManager.selfInstance.kbl;
		kbl.onBackKeyEvent = (KeyBoardListener.OnBackKeyEvent)Delegate.Remove(kbl.onBackKeyEvent, new KeyBoardListener.OnBackKeyEvent(this.OnBackKeyEvent));
	}

	private void OnBackKeyEvent()
	{
		/*if (!this.alreadyShowNgs && UIManager.selfInstance.VAinstance.adData.busyAd && VectorAds.invokeAds(104, string.Empty) == "true")
		{
			this.alreadyShowNgs = true;
			if (!UIManager.selfInstance.VAinstance.ShowNGS(true))
			{
				Storage.SaveConfig();
				if (Application.platform == RuntimePlatform.Android)
				{
					VectorAds.invokeAds(115, string.Empty);
				}
			}
		}
		else
		{
			Storage.SaveConfig();
			if (Application.platform == RuntimePlatform.Android)
			{
				VectorAds.invokeAds(115, string.Empty);
			}
		}*/
	}

	public void OnStart()
	{
		AudioSystem.PlayOneShotEffect("btn");
		base.NextPanel(UIManager.selfInstance.gamePanel.gameObject);
	}

	public void DailyClick(){
		dailyReward.SetActive(true);
	}

	public void OnDailyClosw()
	{
		//dailyReward.SetActive(false);

	}


public void ThemeClick(){
		ThemePanel.SetActive(true);
	}

	public void OnThemeClosw()
	{
		ThemePanel.SetActive(false);

	}


	public void OnSetting()
	{
		AudioSystem.PlayOneShotEffect("btn");
		base.NextPanel(UIManager.selfInstance.settingPanel.gameObject);
	}

    private new void Update()
    {
        bestScoreMainmenutxt.text = PlayerPrefs.GetInt("BestScore").ToString();
        //enNoAds.transform.GetComponent<Image>().raycastTarget = !PlayerPrefs.HasKey("removeAds");
    }

    public void OnAnimInAfter()
	{
		if (!this.firstIn)
		{
			this.firstIn = true;
			if (Storage.ReadConfig("refusenotification", "false") == "false")
			{
				this.RequestLocalNotice();
			}
		}
		/*else if (UIManager.selfInstance.prePanel != null && UIManager.selfInstance.VAinstance.adData.busyAd)
		{
			UIManager.selfInstance.VAinstance.ShowNGS(false);
		}*/
	}

	public void RequestLocalNotice()
	{
		/*if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			VectorNativeInstance vninstance = UIManager.selfInstance.VNinstance;
			vninstance.mCallBack = (VectorNativeInstance.InvokeNativeCallback)Delegate.Combine(vninstance.mCallBack, new VectorNativeInstance.InvokeNativeCallback(this.OnRequestLocalNotice));
			VectorNative.invokeNative(130, string.Empty);
		}
		else
		{
			this.AddNotification();
		}*/
	}

    private void AddNotification()
	{
		DateTime now = DateTime.Now;
		int num = 82800;
		string[] array = new string[]
		{
			"txtNotification0",
			"txtNotification1",
			"txtNotification2"
		};
		/*VectorNative.invokeNative(123, string.Concat(new object[]
		{
			"{\"key\":\"2248hexa_daily\", \"content\":\"",
			Localization.getLocalString(array[UnityEngine.Random.Range(0, array.Length)], "Let's play 2248 Hexa!"),
			"\", \"isRepeat\":true, \"delayTim\":",
			num,
			", \"badgeNum\":1}"
		}));*/
		Storage.WriteConfig("refusenotification", "false");
	}

	private void OnRequestLocalNotice(int type, string msg)
	{
		if (type == 4)
		{
			if (msg == "true")
			{
				this.AddNotification();
			}
			else
			{
				Storage.WriteConfig("refusenotification", "true");
			}
			//VectorNativeInstance vninstance = UIManager.selfInstance.VNinstance;
			//vninstance.mCallBack = (VectorNativeInstance.InvokeNativeCallback)Delegate.Remove(vninstance.mCallBack, new VectorNativeInstance.InvokeNativeCallback(this.OnRequestLocalNotice));
		}
	}

    public void ShowIapPanel()
    {
        if (this.isEnd)
        {
            return;
        }
        AudioSystem.PlayOneShotEffect("btn");
        //Purchaser.instance.BuyNonConsumable();
        iapPanel.SetActive(true);
    }

    public void HideIapPanel()
    {
        if (this.isEnd)
        {
            return;
        }
        AudioSystem.PlayOneShotEffect("btn");
        //Purchaser.instance.BuyNonConsumable();
        iapPanel.SetActive(false);
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

    public void OnRate()
    {
        if (this.isEnd)
        {
            return;
        }
        AudioSystem.PlayOneShotEffect("btn");
        UIManager.selfInstance.ratePanel.gameObject.SetActive(true);
        //VectorNative.invokeNative(106, string.Empty);
    }

public void QuitGame()
    {
#if UNITY_EDITOR
        // This is needed to quit the game in the Unity Editor
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // This is used for quitting the application in a standalone build or on a device
        Application.Quit();
#endif
    }


    private void Awake()
    {
        instance = this;
    }

    public GameObject spTheme1;

	public GameObject commonLogo;

	private bool firstIn;

	private bool alreadyShowNgs;

    public GameObject soundOn;

    public GameObject soundOff;

    public GameObject enNoAds;

    public GameObject rateGo;

    public GameObject iapPanel;

	public GameObject dailyReward;

	public GameObject ThemePanel;

    public static MenuPanel instance;
}