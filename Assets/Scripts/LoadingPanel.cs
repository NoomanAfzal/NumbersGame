// dnSpy decompiler from Assembly-CSharp.dll class: LoadingPanel
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingPanel : BasePanel
{
	public static void NextScene(string nextScene)
	{
		LoadingPanel.nextScene = nextScene;
		SceneManager.LoadScene(nextScene);
	}

	private new void Start()
	{
		base.Start();
		base.StartCoroutine("LoadScene", LoadingPanel.nextScene);
	}

	private IEnumerator LoadScene(string scene_name)
	{
		this.async_operation = SceneManager.LoadSceneAsync(scene_name);
		this.async_operation.allowSceneActivation = false;
		while (this.async_operation.isDone)
		{
			yield return new WaitForEndOfFrame();
			this.UpdateProgress(this.async_operation.progress);
		}
		yield return new WaitForEndOfFrame();
		this.UpdateProgress(1f);
		yield return new WaitForEndOfFrame();
		this.async_operation.allowSceneActivation = true;
		yield break;
	}

	private void UpdateProgress(float pec)
	{
	}

	private new void OnEnable()
	{
		base.OnEnable();
	}

	private new void OnDisable()
	{
		base.OnDisable();
	}

	private new void Update()
	{
		base.Update();
	}

	private static string nextScene = string.Empty;

	[HideInInspector]
	public AsyncOperation async_operation;
}
