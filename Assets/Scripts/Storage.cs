// dnSpy decompiler from Assembly-CSharp.dll class: Storage
using System;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public sealed class Storage
{
	public static void Init(string iemi = "vectorAlwaysLoveU")
	{
		Storage.iemi = iemi + "vectorAlwaysLoveU";
		if (!Storage.ReadConfig("version", "null").Equals("1.0"))
		{
			Storage.WriteConfig("version", "1.0");
		}
	}

	public static string md5(string input)
	{
		MD5 md = MD5.Create();
		byte[] bytes = Encoding.UTF8.GetBytes(input);
		byte[] array = md.ComputeHash(bytes);
		StringBuilder stringBuilder = new StringBuilder();
		for (int i = 0; i < array.Length; i++)
		{
			stringBuilder.Append(array[i].ToString("x2"));
		}
		return stringBuilder.ToString();
	}

	public static string ReadConfig(string type, string defaultValue)
	{
		string @string = PlayerPrefs.GetString("key_" + type, defaultValue);
		string string2 = PlayerPrefs.GetString("key_" + type + "_s", string.Empty);
		if (Storage.md5(Storage.iemi + @string + Storage.iemi) == string2)
		{
			return @string;
		}
		return defaultValue;
	}

	public static void WriteConfig(string type, string value)
	{
		PlayerPrefs.SetString("key_" + type, value);
		PlayerPrefs.SetString("key_" + type + "_s", Storage.md5(Storage.iemi + value + Storage.iemi));
	}

	public static void SaveConfig()
	{
		PlayerPrefs.Save();
	}

	public static void RemoveConfig(string type)
	{
		PlayerPrefs.DeleteKey(type);
	}

	public static void RemoveAllConfig()
	{
		PlayerPrefs.DeleteAll();
	}

	public const string TypeInfos = "useInfo";

	public const string TypeSound = "sound";

	public const string TypeRefuseNotification = "refusenotification";

	public const string TypeLocalization = "localization";

	public const string TypeGameData = "gameData";

	public const string TypeVersion = "version";

	public const string CurVersion = "1.0";

	public static string iemi = "vectorAlwaysLoveU";
}
