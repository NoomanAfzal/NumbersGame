// dnSpy decompiler from Assembly-CSharp.dll class: Localization
using System;
using System.Collections.Generic;
using com.vector;
using UnityEngine;

public sealed class Localization
{
	private Localization()
	{
	}

	private static bool IosJugeLanguage(string countrycode, string head)
	{
		return countrycode == head || countrycode.Contains(head + "-");
	}

	private static bool AndroidJugeLanguage(string countrycode, string head)
	{
		return countrycode == head || countrycode.Contains(head + "_");
	}

	public static void Init()
	{
		int num = -1;
		if (num == -1)
		{
			string countrycode = VectorNative.invokeNative(119, string.Empty);
			if (Application.platform == RuntimePlatform.IPhonePlayer)
			{
				if (Localization.IosJugeLanguage(countrycode, "en"))
				{
					num = 0;
				}
				else if (Localization.IosJugeLanguage(countrycode, "fr"))
				{
					num = 1;
				}
				else if (Localization.IosJugeLanguage(countrycode, "de"))
				{
					num = 2;
				}
				else if (Localization.IosJugeLanguage(countrycode, "it"))
				{
					num = 3;
				}
				else if (Localization.IosJugeLanguage(countrycode, "es"))
				{
					num = 4;
				}
				else if (Localization.IosJugeLanguage(countrycode, "pt"))
				{
					num = 5;
				}
				else if (Localization.IosJugeLanguage(countrycode, "ja"))
				{
					num = 6;
				}
				else if (Localization.IosJugeLanguage(countrycode, "ko"))
				{
					num = 7;
				}
				else if (Localization.IosJugeLanguage(countrycode, "zh-Hans"))
				{
					num = 8;
				}
				else if (Localization.IosJugeLanguage(countrycode, "zh-HK") || Localization.IosJugeLanguage(countrycode, "zh-Hant"))
				{
					num = 9;
				}
				else if (Localization.IosJugeLanguage(countrycode, "ru"))
				{
					num = 10;
				}
				else if (Localization.IosJugeLanguage(countrycode, "id"))
				{
					num = 11;
				}
				else
				{
					num = 0;
				}
			}
			else if (Localization.AndroidJugeLanguage(countrycode, "en"))
			{
				num = 0;
			}
			else if (Localization.AndroidJugeLanguage(countrycode, "fr"))
			{
				num = 1;
			}
			else if (Localization.AndroidJugeLanguage(countrycode, "de"))
			{
				num = 2;
			}
			else if (Localization.AndroidJugeLanguage(countrycode, "it"))
			{
				num = 3;
			}
			else if (Localization.AndroidJugeLanguage(countrycode, "es"))
			{
				num = 4;
			}
			else if (Localization.AndroidJugeLanguage(countrycode, "pt"))
			{
				num = 5;
			}
			else if (Localization.AndroidJugeLanguage(countrycode, "ja"))
			{
				num = 6;
			}
			else if (Localization.AndroidJugeLanguage(countrycode, "ko"))
			{
				num = 7;
			}
			else if (Localization.AndroidJugeLanguage(countrycode, "zh_CH") || Localization.AndroidJugeLanguage(countrycode, "zh_CN"))
			{
				num = 8;
			}
			else if (Localization.AndroidJugeLanguage(countrycode, "zh_TW") || Localization.AndroidJugeLanguage(countrycode, "zh_HK"))
			{
				num = 9;
			}
			else if (Localization.AndroidJugeLanguage(countrycode, "ru"))
			{
				num = 10;
			}
			else if (Localization.AndroidJugeLanguage(countrycode, "in"))
			{
				num = 11;
			}
			else
			{
				num = 0;
			}
		}
		Localization.instance.curIndex = num;
		Localization.instance.langs = new List<string>();
		Localization.instance.langData = new Dictionary<string, List<string>>();
		List<List<string>> list = CSVReader.ReadCSVData("binaryData/localization", 0);
		for (int i = 1; i < list[0].Count; i++)
		{
			Localization.instance.langs.Add(list[0][i]);
		}
		for (int j = 1; j < list.Count; j++)
		{
			List<string> list2 = new List<string>();
			for (int k = 1; k < list[j].Count; k++)
			{
				list2.Add(list[j][k]);
			}
			Localization.instance.langData.Add(list[j][0], list2);
		}
	}

	public static Localization getInstance()
	{
		return Localization.instance;
	}

	public static bool IsChina()
	{
		return Localization.instance.curIndex == 8;
	}

	public static string getCurFlag()
	{
		return Localization.instance.langs[Localization.instance.curIndex];
	}

	public static bool switchLang(int index)
	{
		if (index < Localization.instance.langs.Count && index >= 0)
		{
			Localization.instance.curIndex = index;
			Storage.WriteConfig("localization", index.ToString());
			return true;
		}
		return false;
	}

	public static bool switchLang(string langStr)
	{
		int index = -1;
		for (int i = 0; i < Localization.instance.langs.Count; i++)
		{
			if (Localization.instance.langs[i] == langStr)
			{
				index = i;
				break;
			}
		}
		return Localization.switchLang(index);
	}

	public static string getLocalString(string key, string defaultText = "")
	{
		if (Localization.instance.langData.ContainsKey(key))
		{
			return Localization.instance.langData[key][Localization.instance.curIndex];
		}
		return defaultText;
	}

	private static readonly Localization instance = new Localization();

	public int curIndex;

	private Dictionary<string, List<string>> langData;

	public List<string> langs;
}
