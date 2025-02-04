// dnSpy decompiler from Assembly-CSharp.dll class: com.vector.VectorNative
using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace com.vector
{
	public abstract class VectorNative : MonoBehaviour
	{
		//[DllImport("__Internal")]
		//private static extern string _invokeNative(int type, string msg);

		public static string invokeNative(int type, string msg = "")
		{
			//if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
			//{
			//	AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			//	AndroidJavaObject @static = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
			//	string text = @static.Call<string>("_invokeNative", new object[]
			//	{
			//		type,
			//		msg
			//	});
			//	UnityEngine.Debug.Log("invokeNative done! ret : " + text);
			//	return text;
			//}
			return string.Empty;
		}

		internal abstract void NativeCallback(int type, string msg);

		internal void invokeNativeCallback(string msg)
		{
			//int num = msg.IndexOf(',');
			//int type = int.Parse(msg.Substring(0, num));
			//string msg2 = msg.Substring(num + 1);
			//this.NativeCallback(type, msg2);
		}

		public const int VN_INIT = 100;

		public const int VN_GETITEMKEY = 101;

		public const int VN_UPLOADSCORE = 102;

		public const int VN_UPLOADARCHIVE = 103;

		public const int VN_MOREGAME = 104;

		public const int VN_SHOWRANK = 105;

		public const int VN_SHOWRATE = 106;

		public const int VN_SHOWSHARE = 107;

		public const int VN_TWITTERSHARE = 108;

		public const int VN_FBLIKE = 109;

		public const int VN_TWITTERFOLLOW = 110;

		public const int VN_GETIEMIKEY = 111;

		public const int VN_BUY = 112;

		public const int VN_RESTORE = 113;

		public const int VN_REMOVEADS = 114;

		public const int VN_ISREMOVEADS = 115;

		public const int VN_VIBRATE = 116;

		public const int VN_CLIPBOARD = 117;

		public const int VN_GETSHARELINK = 118;

		public const int VN_GETLANGUAGE = 119;

		public const int VN_GETLEADERSCORE = 120;

		public const int VN_SHOWACHIEVEMENT = 121;

		public const int VN_MAILUS = 122;

		public const int VN_ADD_LOCALNOTICE = 123;

		public const int VN_REMOVE_LOCALNOTICE = 124;

		public const int VN_REGISTER_PHONECALL = 125;

		public const int VN_UNREGISTER_PHONECALL = 126;

		public const int VN_ISOTHERAUDIO = 127;

		public const int VN_LOGIN_GAMECENTER = 128;

		public const int VN_OTHER_GAME = 129;

		public const int VN_REQUEST_LOCALNOTICE = 130;

		public const int IG_BUY_SUC = 0;

		public const int IG_BUY_FAIL = 1;

		public const int IG_RESTORE = 2;

		public const int IG_GAMECENTER = 3;

		public const int IG_GRANTEDNOTIFICATION = 4;

		public const int IG_PHONECALL = 125;

		public const string VectorExtra = "VectorExtra";

		public const string VN_META_FBLIKE = "VN_FBLIKE";

		public const string VN_META_TWFOLLOW = "VN_TWFOLLOW";

		public const string VN_META_ShareLink = "VN_ShareLink";

		public const string VN_META_ProductID = "VN_ProductID";
	}
}
