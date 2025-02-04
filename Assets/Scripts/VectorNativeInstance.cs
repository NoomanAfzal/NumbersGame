// dnSpy decompiler from Assembly-CSharp.dll class: VectorNativeInstance
using System;
using com.vector;
using UnityEngine;

public class VectorNativeInstance : VectorNative
{
	private void Awake()
	{
		if (!VectorNativeInstance.alreadyCreate)
		{
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}
		else
		{
			UnityEngine.Object.DestroyImmediate(base.gameObject);
		}
		VectorNativeInstance.alreadyCreate = true;
	}

	private void Update()
	{
	}

	internal override void NativeCallback(int type, string msg)
	{
		if (type == 2 || type == 0)
		{
			GameUser instance = GameUser.Instance;
			if (msg == "nlhexa_noads")
			{
				VectorAds.invokeAds(108, string.Empty);
				instance.isNoAds = true;
			}
			GameUser.Save();
		}
		if (this.mCallBack != null)
		{
			this.mCallBack(type, msg);
		}
	}

	public VectorNativeInstance.InvokeNativeCallback mCallBack;

	private static bool alreadyCreate;

	public delegate void InvokeNativeCallback(int type, string msg);
}
