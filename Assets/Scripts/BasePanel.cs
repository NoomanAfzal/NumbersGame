// dnSpy decompiler from Assembly-CSharp.dll class: BasePanel
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class BasePanel : MonoBehaviour
{
	protected void Start()
	{
	}

	protected void OnEnable()
	{
		this.isEnd = false;
		this.PlaySwitchAnim(this.PANEL_IN);
		this.LocalizationLabel();
	}

	public void LocalizationLabel()
	{
		Text[] componentsInChildren = base.GetComponentsInChildren<Text>();
		foreach (Text text in componentsInChildren)
		{
		
		}
	}

	protected void PlaySwitchAnim(int value)
	{
		if (this.switchAnim != null)
		{
			this.switchAnim.SetInteger("AnimState", value);
		}
	}

	internal void NextPanel(GameObject nextGO)
	{
		if (this.isEnd)
		{
			return;
		}
		this.isEnd = true;
		this.nextGO = nextGO;
		UIManager uimanager = UnityEngine.Object.FindObjectOfType<UIManager>();
		if (uimanager != null)
		{
			uimanager.prePanel = this;
		}
		if (this.switchAnim != null)
		{
			base.StopCoroutine("OnNextPanel");
			base.StartCoroutine("OnNextPanel");
		}
		else
		{
			base.gameObject.SetActive(false);
			nextGO.SetActive(true);
		}
	}

	private IEnumerator OnNextPanel()
	{
		//yield return new WaitForSeconds(0.16f);
		//this.PlaySwitchAnim(this.PANEL_OUT);
		yield return new WaitForEndOfFrame();
		//yield return new WaitForSeconds(this.switchAnim.GetCurrentAnimatorStateInfo(0).length);
		base.gameObject.SetActive(false);
		this.nextGO.SetActive(true);
		UIManager um = UnityEngine.Object.FindObjectOfType<UIManager>();
		if (um != null)
		{
			um.curMainPanel = this.nextGO.GetComponentInChildren<BasePanel>();
		}
		yield break;
	}

	internal void NextScene(string nextScene)
	{
		if (this.isEnd)
		{
			return;
		}
		this.isEnd = true;
		this.nextScene = nextScene;
		if (this.switchAnim != null)
		{
			base.StopCoroutine("OnNextScene");
			base.StartCoroutine("OnNextScene");
		}
		else
		{
			LoadingPanel.NextScene(nextScene);
		}
	}

	private IEnumerator OnNextScene()
	{
		yield return new WaitForSeconds(0.16f);
		this.PlaySwitchAnim(this.PANEL_OUT);
		yield return new WaitForEndOfFrame();
		yield return new WaitForSeconds(this.switchAnim.GetCurrentAnimatorStateInfo(0).length);
		LoadingPanel.NextScene(this.nextScene);
		yield break;
	}

	internal void NextFunction(BasePanel.Function nextFunction)
	{
		if (this.isEnd)
		{
			return;
		}
		this.isEnd = true;
		this.nextFunction = nextFunction;
		if (this.switchAnim != null)
		{
			base.StopCoroutine("OnNextFunction");
			base.StartCoroutine("OnNextFunction");
		}
		else
		{
			if (nextFunction != null)
			{
				nextFunction();
			}
			nextFunction = null;
		}
	}

	private IEnumerator OnNextFunction()
	{
		//yield return new WaitForSeconds(0.16f);
		//this.PlaySwitchAnim(this.PANEL_OUT);
		yield return new WaitForEndOfFrame();
		//yield return new WaitForSeconds(this.switchAnim.GetCurrentAnimatorStateInfo(0).length);
		if (this.nextFunction != null)
		{
			this.nextFunction();
		}
		this.nextFunction = null;
		yield break;
	}

	protected void OnDisable()
	{
		this.isEnd = false;
	}

	public void PlayAudio(string musicEvent)
	{
	}

	protected void Update()
	{
	}

	protected int PANEL_IN;

	protected int PANEL_OUT = 1;

	internal bool isEnd;

	public Animator switchAnim;

	private GameObject nextGO;

	private string nextScene;

	private BasePanel.Function nextFunction;

	private const float outBtnWaitTime = 0.16f;

	internal delegate void Function();
}