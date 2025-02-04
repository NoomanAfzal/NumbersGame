// dnSpy decompiler from Assembly-CSharp.dll class: com.vector.VectorAds
using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace com.vector
{
	public abstract class VectorAds : MonoBehaviour
	{
		//[DllImport("__Internal")]
		//private static extern string _invokeAds(int type, string msg);

		public static string invokeAds(int type, string msg = "")
		{
			//if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
			//{
			//	AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			//	AndroidJavaObject @static = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
			//	string text = @static.Call<string>("_invokeAds", new object[]
			//	{
			//		type,
			//		msg
			//	});
			//	UnityEngine.Debug.Log("invokeAds done! ret : " + text);
			//	return text;
			//}
			return string.Empty;
		}

		internal abstract void AdsCallback(int type, string msg);

		internal void invokeAdsCallback(string msg)
		{
			//int num = msg.IndexOf(',');
			//int type = int.Parse(msg.Substring(0, num));
			//string msg2 = msg.Substring(num + 1);
			//this.AdsCallback(type, msg2);
		}

		public const int VA_INIT = 100;

		public const int VA_SHOW_BANNER = 101;

		public const int VA_HIDE_BANNER = 102;

		public const int VA_SET_BANNER_PEC = 103;

		public const int VA_IS_NGS_CACHE = 104;

		public const int VA_SHOW_NGS = 105;

		public const int VA_IS_AV_CACHE = 106;

		public const int VA_SHOW_AV = 107;

		public const int VA_REMOVEADS = 108;

		public const int VA_ADD_UM_EVENT = 109;

		public const int VA_START_UM_LEVEL = 110;

		public const int VA_FAIL_UM_LEVEL = 111;

		public const int VA_END_UM_LEVEL = 112;

		public const int VA_GET_UM_OLPARAMS = 113;

		public const int VA_ADD_UM_CALEVENT = 114;

		public const int VA_LEAVE_GAME = 115;

		public const int VA_GET_FIREBASE_OLPARAMS = 200;

		public const int VA_IG_NGS = 0;

		public const int VA_IG_RV = 1;

		public const string VA_META_UmengID = "VA_UmengID";

		public const string VA_META_ApplovinID = "AppLovinSdkKey";
	}
}
