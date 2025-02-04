// dnSpy decompiler from Assembly-CSharp.dll class: utils
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class utils
{
	public static void setRandomSeed(int send)
	{
		utils.randomSend = send;
		utils.nowRand = new System.Random(send);
		utils.alreadyUse.Clear();
	}

	public static int getRandomBySeed()
	{
		int num = utils.nowRand.Next(23, 16777215);
		if (utils.alreadyUse.ContainsKey(num))
		{
			utils.randomSend = utils.nowRand.Next();
			utils.randomSend++;
			utils.nowRand = new System.Random(utils.randomSend);
			return utils.getRandomBySeed();
		}
		utils.randomSend = num;
		utils.alreadyUse.Add(utils.randomSend, true);
		return num;
	}

	public static int CurrentSeconds()
	{
		DateTime d = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
		return (int)(DateTime.UtcNow - d).TotalSeconds;
	}

	public static string GetWritablePath(string abPath = "")
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android)
		{
			return Application.persistentDataPath + string.Empty;
		}
		return Application.dataPath + ((!(abPath == string.Empty)) ? abPath : "/CacheData");
	}

	public static Vector2 xAxleDTV(float degree)
	{
		Vector2 vector = new Vector2(Mathf.Cos(degree * 0.0174532924f), Mathf.Sin(degree * 0.0174532924f));
		return vector.normalized;
	}

	public static Texture2D CaptureScreenshot(List<Camera> cams, Rect rect)
	{
		RenderTexture renderTexture = new RenderTexture((int)rect.width, (int)rect.height, 24, RenderTextureFormat.ARGB32);
		renderTexture.useMipMap = false;
		renderTexture.antiAliasing = 1;
		for (int i = 0; i < cams.Count; i++)
		{
			cams[i].targetTexture = renderTexture;
			cams[i].Render();
		}
		RenderTexture.active = renderTexture;
		Texture2D texture2D = new Texture2D((int)rect.width, (int)rect.height, TextureFormat.ARGB32, false);
		texture2D.ReadPixels(rect, 0, 0, false);
		texture2D.Apply();
		for (int j = 0; j < cams.Count; j++)
		{
			cams[j].targetTexture = null;
		}
		RenderTexture.active = null;
		UnityEngine.Object.Destroy(renderTexture);
		return texture2D;
	}

	public static void SaveTexture(string filePath, Texture2D tex)
	{
		if (File.Exists(filePath))
		{
			File.Delete(filePath);
		}
		FileStream fileStream = File.Create(filePath);
		byte[] array = tex.EncodeToJPG();
		fileStream.Write(array, 0, array.Length);
		fileStream.Close();
	}

	public static void SaveTxt(string filePath, string src)
	{
		if (File.Exists(filePath))
		{
			File.Delete(filePath);
		}
		StreamWriter streamWriter = new StreamWriter(filePath);
		streamWriter.Write(src);
		streamWriter.Close();
	}

	private static int randomSend = 0;

	private static System.Random nowRand = null;

	private static Dictionary<int, bool> alreadyUse = new Dictionary<int, bool>();
}
