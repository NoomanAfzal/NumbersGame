// dnSpy decompiler from Assembly-CSharp.dll class: AudioSystem
using System;
using System.Collections.Generic;
using UnityEngine;

public sealed class AudioSystem
{
	private AudioSystem()
	{
	}

	public static void Init()
	{
		UnityEngine.Object[] array = Resources.LoadAll("music/");
		AudioSystem.allMusic.Clear();
		for (int i = 0; i < array.Length; i++)
		{
			AudioSystem.allMusic.Add(array[i].name, array[i]);
		}
		AudioSystem.isSound = Storage.ReadConfig("sound", "true").Equals("true");
		AudioSystem.instance.sfxGo = new GameObject("sfx");
		AudioSystem.instance.sfxGo.transform.position = Vector3.zero;
		AudioSystem.instance.sfxs = new List<GameObject>();
	}

	public static void PauseSound()
	{
		if (AudioSystem.isSound)
		{
			foreach (GameObject gameObject in AudioSystem.instance.sfxs)
			{
				sfxCtrl component = gameObject.GetComponent<sfxCtrl>();
				component.PauseSfx();
			}
		}
	}

	public static void ResumeSound()
	{
		if (AudioSystem.isSound)
		{
			foreach (GameObject gameObject in AudioSystem.instance.sfxs)
			{
				sfxCtrl component = gameObject.GetComponent<sfxCtrl>();
				component.ResumeSfx();
			}
		}
	}

	public static void SwitchSound(bool open)
	{
		AudioSystem.isSound = open;
		Storage.WriteConfig("sound", (!open) ? "false" : "true");
	}

	public static void PlayOneShotEffect(string sound)
	{
		if (!AudioSystem.isSound)
		{
			return;
		}
		AudioSystem.playEffect(sound);
	}

	public static void playEffect(string sound)
	{
		if (!AudioSystem.isSound)
		{
			return;
		}
		GameObject gameObject = new GameObject();
		gameObject.transform.parent = AudioSystem.instance.sfxGo.transform;
		gameObject.transform.position = Vector3.zero;
		gameObject.AddComponent<sfxCtrl>().sfx = (AudioSystem.allMusic[sound] as AudioClip);
		AudioSystem.instance.sfxs.Add(gameObject);
	}

	public static void RemoveEffect(GameObject GO)
	{
		AudioSystem.instance.sfxs.Remove(GO);
	}

	public GameObject sfxGo;

	public static bool isSound = true;

	public static Dictionary<string, object> allMusic = new Dictionary<string, object>();

	private List<GameObject> sfxs;

	private static readonly AudioSystem instance = new AudioSystem();
}
