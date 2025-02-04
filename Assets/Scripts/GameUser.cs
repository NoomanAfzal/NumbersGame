// dnSpy decompiler from Assembly-CSharp.dll class: GameUser
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameUser
{
	public void CheckCompatible()
	{
		if (this.version != InitScript.version)
		{
			this.version = InitScript.version;
			GameUser.Save();
		}
	}

	public static GameUser Instance
	{
		get
		{
			return GameUser.instance;
		}
		private set
		{
			GameUser.instance = value;
		}
	}

	public void SaveLastGameData()
	{
		if (this.guideStep >= 4 && UIManager.selfInstance.gamePanel.gameObject.activeSelf && Game.main != null)
		{
			this.lastGames.Clear();
			this.lastTime = 0f;
			this.lastRecord = 0L;
			this.lastRecord = UIManager.selfInstance.gamePanel.targetScore;
			this.lastTime = UIManager.selfInstance.gamePanel.costTim;
			for (int i = 0; i < Game.main.cellSizeX; i++)
			{
				for (int j = 0; j < Game.main.cellSizeY; j++)
				{
					this.lastGames.Add(Game.main.dots[i][j].numberLevel);
				}
			}
			GameUser.Save();
		}
	}

	public void ClearLastGameData()
	{
		this.lastGames.Clear();
		this.lastTipTimes = 0;
		this.lastTipTimeCount = 0L;
		this.isNewGame = true;
		this.lastTime = 0f;
		this.lastRecord = 0L;
	}

	public static bool ReadFromLocal()
	{
		string text = Storage.ReadConfig("useInfo", string.Empty);
		if (string.IsNullOrEmpty(text))
		{
			return false;
		}
		GameUser.Instance = JsonUtility.FromJson<GameUser>(text);
		GameUser.Instance.CheckCompatible();
		return true;
	}

	public static void Save()
	{
		string value = JsonUtility.ToJson(GameUser.Instance);
		Storage.WriteConfig("useInfo", value);
	}

	public int guideStep;

	public bool alreadyRate;

	public bool isNoAds;

	public long bestRecord = -1L;

	public int nowTheme;

	public bool forceTheme;

	public bool forceChristmas;

	public int version = InitScript.version;

	public List<int> lastGames = new List<int>();

	public float lastTime;

	public long lastRecord;

	public string dateStr = string.Empty;

	public int lastTipTimes;

	public long lastTipTimeCount;

	public bool isNewGame = true;

	private static GameUser instance = new GameUser();
}
