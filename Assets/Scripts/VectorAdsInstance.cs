// dnSpy decompiler from Assembly-CSharp.dll class: VectorAdsInstance
using System;
using com.vector;
using UnityEngine;

public class VectorAdsInstance : VectorAds
{
	private void Awake()
	{
		if (!VectorAdsInstance.alreadyCreate)
		{
			this.ngsTim = (float)utils.CurrentSeconds();
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}
		else
		{
			UnityEngine.Object.DestroyImmediate(base.gameObject);
		}
		VectorAdsInstance.alreadyCreate = true;
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			this.ngsCount = 3;
		}
		else
		{
			this.adData.isReview = true;
			this.ngsCount = 2;
		}
	}

	private void Update()
	{
		if (Application.internetReachability == NetworkReachability.NotReachable)
		{
			return;
		}
		this.checkTime -= Time.unscaledDeltaTime;
		if (this.checkTime <= 0f && !this.busyAdCheck)
		{
			if (Application.platform == RuntimePlatform.IPhonePlayer)
			{
				string text = VectorAds.invokeAds(113, "nlhexa_data");
				if (text != string.Empty)
				{
					this.adData = JsonUtility.FromJson<AdData>(text);
					this.busyAdCheck = true;
					if (!GameUser.Instance.isNoAds && this.adData.banner)
					{
						VectorAds.invokeAds(101, string.Empty);
					}
				}
			}
			else
			{
				string text2 = VectorAds.invokeAds(200, "nlhexa_data");
				if (text2 != string.Empty)
				{
					this.adData = JsonUtility.FromJson<AdData>(text2);
					this.busyAdCheck = true;
					if (!GameUser.Instance.isNoAds && this.adData.banner)
					{
						VectorAds.invokeAds(101, string.Empty);
					}
				}
			}
		}
		if (this.checkTime <= 0f)
		{
			this.checkTime = 5f;
		}
	}

	public bool ShowNGS(bool isForce = false)
	{
		this.ngsCount--;
		if (!GameUser.Instance.isNoAds && (float)utils.CurrentSeconds() - this.ngsTim >= (float)this.adData.minNgsTim && (isForce || (float)utils.CurrentSeconds() - this.ngsTim >= (float)this.adData.ngsTim || this.ngsCount <= 0))
		{
			this.ResetNgsCount();
			VectorAds.invokeAds(105, string.Empty);
			return true;
		}
		return false;
	}

	public void ResetNgsCount()
	{
		this.ngsTim = (float)utils.CurrentSeconds();
		this.ngsCount = this.adData.ngsCount;
	}

	internal override void AdsCallback(int type, string msg)
	{
		if (this.mCallBack != null)
		{
			this.mCallBack(type, msg);
		}
	}

	public VectorAdsInstance.InvokeAdsCallback mCallBack;

	private static bool alreadyCreate;

	private float checkTime;

	private bool busyAdCheck;

	private float ngsTim;

	private int ngsCount = 2;

	internal AdData adData = new AdData();

	public delegate void InvokeAdsCallback(int type, string msg);
}
