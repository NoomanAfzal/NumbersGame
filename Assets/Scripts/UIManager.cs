// dnSpy decompiler from Assembly-CSharp.dll class: UIManager
using System;
using com.vector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	
	private void Awake()
	{
		InitScript.InitAll();
		this.VNinstance = UnityEngine.Object.FindObjectOfType<VectorNativeInstance>();
		this.VAinstance = UnityEngine.Object.FindObjectOfType<VectorAdsInstance>();
		this.kbl = UnityEngine.Object.FindObjectOfType<KeyBoardListener>();
		this.canvas = UnityEngine.Object.FindObjectOfType<Canvas>();
		UIManager.selfInstance = this;
		if (AudioSystem.isSound)
		{
		}
		if (!GameUser.Instance.isNoAds)
		{
			VectorAds.invokeAds(101, string.Empty);
		}
	}
	public void Showinterstitial()
	{
		GoogleAdMobController.instance.ShowInterstitialAd();
	}
	private void OnDestory()
	{
		UIManager.selfInstance = null;
	}
	
	public static void ButtonVisible(EventTrigger btn, bool visible)
	{
		btn.enabled = visible;
		Image[] componentsInChildren = btn.GetComponentsInChildren<Image>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			if (visible)
			{
				componentsInChildren[i].color = Color.white;
			}
			else
			{
				componentsInChildren[i].color = Color.gray;
			}
		}
		Text[] componentsInChildren2 = btn.GetComponentsInChildren<Text>();
		for (int j = 0; j < componentsInChildren2.Length; j++)
		{
			if (visible)
			{
				componentsInChildren2[j].color = Color.white;
			}
			else
			{
				componentsInChildren2[j].color = Color.gray;
			}
		}
	}

	public static void UpdateRandomState(int seed = -1)
	{
		if (seed == -1)
		{
			UnityEngine.Random.InitState(Mathf.Abs(Guid.NewGuid().GetHashCode()) + (int)(DateTime.Now.Ticks % 1000000L / 100L));
		}
		else
		{
			UnityEngine.Random.InitState(seed);
		}
	}

	private void OnApplicationPause(bool pauseStatus)
	{
		if (!pauseStatus)
		{
			if (this.VAinstance.adData.busyAd && this.firstNgsTime <= 0f && !this.noAdsThisTime)
			{
				this.VAinstance.ShowNGS(false);
			}
			this.noAdsThisTime = false;
			this.firstNgsTime = 0f;
		}
	}

	private void Update()
	{
		this.firstNgsTime -= Time.deltaTime;
		this.firstNgsTime = ((this.firstNgsTime > 0f) ? this.firstNgsTime : 0f);
		this.resetRandomTim += Time.deltaTime;
		if (this.resetRandomTim >= 5f)
		{
			this.resetRandomTim = 0f;
			UIManager.UpdateRandomState(-1);
		}
	}

	public static UIManager selfInstance;

	public Canvas canvas;


	public MenuPanel menuPanel;

	public GamePanel gamePanel;

	public PausePanel pausePanel;

	public RevivePanel revivePanel;

	public SettingPanel settingPanel;

	public PassPanel passPanel;

	public RatePanel ratePanel;

    public RewardPopup rewardPopup;

	public BkgPanel bkgPanel;

	public TopPanel topPanel;

	[HideInInspector]
	public KeyBoardListener kbl;

	[HideInInspector]
	public VectorNativeInstance VNinstance;

	[HideInInspector]
	public VectorAdsInstance VAinstance;

	internal BasePanel curMainPanel;

	internal BasePanel prePanel;

	internal bool noAdsThisTime;

	private float resetRandomTim;

	private float firstNgsTime = 10f;
}
