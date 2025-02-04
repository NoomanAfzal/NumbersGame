// dnSpy decompiler from Assembly-CSharp.dll class: sfxCtrl
using System;
using UnityEngine;

public class sfxCtrl : MonoBehaviour
{
	private void Start()
	{
		if (this.sfx == null)
		{
			Destroy(base.gameObject);
			return;
		}
		AudioSource audioSource = base.gameObject.AddComponent<AudioSource>();
		audioSource.clip = this.sfx;
		this.totalTime = audioSource.clip.length * 2f;
		audioSource.Play();
	}

	public void PauseSfx()
	{
		if (!this.isPause)
		{
			this.AS.Pause();
			this.isPause = true;
		}
	}

	public void ResumeSfx()
	{
		if (this.isPause)
		{
			this.AS.UnPause();
			this.isPause = false;
		}
	}

	private void Update()
	{
		if (!this.isPause)
		{
			this.curTime += Time.deltaTime;
			if (this.curTime >= this.totalTime)
			{
				AudioSystem.RemoveEffect(base.gameObject);
				Destroy(base.gameObject);
				return;
			}
		}
	}

	public AudioClip sfx;

	private float totalTime;

	private float curTime;

	private bool isPause;

	private AudioSource AS;
}
