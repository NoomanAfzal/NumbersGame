// dnSpy decompiler from Assembly-CSharp.dll class: PausePanel
using System;
using com.vector;
using UnityEngine;
using UnityEngine.UI;

public class PausePanel : BasePanel
{
	protected new void Start()
	{
		base.Start();
	}

	protected new void OnEnable()
	{
		base.OnEnable();
        //if (InitScript.isLoginGameCenter)
        //{
        //	string text = "CgkI_PC18bcVEAIQAA";
        //	if (Application.platform == RuntimePlatform.IPhonePlayer)
        //	{
        //		text = "nlhexa_leaderboard";
        //	}
        //	long num = GameUser.Instance.bestRecord;
        //	if (UIManager.selfInstance.gamePanel.targetScore > num)
        //	{
        //		num = UIManager.selfInstance.gamePanel.targetScore;
        //	}
        //	VectorNative.invokeNative(102, string.Concat(new object[]
        //	{
        //		"{\"category\":\"",
        //		text,
        //		"\",\"score\":",
        //		num,
        //		"}"
        //	}));
        //}
        soundOn.SetActive(AudioSystem.isSound);
        soundOff.SetActive(!AudioSystem.isSound);
        if (Localization.getCurFlag() == "China")
		{
			this.imgNoAds.sprite = UIManager.selfInstance.settingPanel.cnNoAds;
		}
		else
		{
			this.imgNoAds.sprite = UIManager.selfInstance.settingPanel.enNoAds;
		}
		UIManager.selfInstance.topPanel.SwitchThemeBtn(true);
		KeyBoardListener kbl = UIManager.selfInstance.kbl;
		kbl.onBackKeyEvent = (KeyBoardListener.OnBackKeyEvent)Delegate.Combine(kbl.onBackKeyEvent, new KeyBoardListener.OnBackKeyEvent(this.OnBackKeyEvent));
	}

	protected new void OnDisable()
	{
		base.OnDisable();
		KeyBoardListener kbl = UIManager.selfInstance.kbl;
		kbl.onBackKeyEvent = (KeyBoardListener.OnBackKeyEvent)Delegate.Remove(kbl.onBackKeyEvent, new KeyBoardListener.OnBackKeyEvent(this.OnBackKeyEvent));
	}

	private void OnBackKeyEvent()
	{
		this.OnResume();
	}

	public void AfterAnimIn()
	{
		UIManager.selfInstance.VAinstance.ShowNGS(false);
	}

	public void OnBack()
	{
      
		AudioSystem.PlayOneShotEffect("btn");
		this.PANEL_OUT = 3;
		this.PANEL_IN = 0;
		UIManager.selfInstance.gamePanel.OnClose();
		base.NextPanel(UIManager.selfInstance.menuPanel.gameObject);
	}

	public void OnResume()
	{
		if (this.isEnd)
		{
			return;
		}
		AudioSystem.PlayOneShotEffect("btn");
		this.PANEL_OUT = 1;
		this.PANEL_IN = 0;
		base.NextFunction(delegate
		{
			base.gameObject.SetActive(false);
			UIManager.selfInstance.gamePanel.OnResume();
		});
	}

	public void OnRestart()
	{
		if (this.isEnd)
		{
			return;
		}
		AudioSystem.PlayOneShotEffect("btn");
		this.PANEL_OUT = 1;
		this.PANEL_IN = 0;
		base.NextFunction(delegate
		{
			base.gameObject.SetActive(false);
			UIManager.selfInstance.gamePanel.OnRestart();
		});
	}

	public void OnSetting()
	{
		if (this.isEnd)
		{
			return;
		}
		AudioSystem.PlayOneShotEffect("btn");
		this.PANEL_OUT = 3;
		this.PANEL_IN = 2;
		base.NextPanel(UIManager.selfInstance.settingPanel.gameObject);
	}

	public void OnOtherGame()
	{
		//if (this.isEnd)
		//{
		//	return;
		//}
		//AudioSystem.PlayOneShotEffect("btn");
		//int num = this.useSpIndex;
		//if (num != 0)
		//{
		//	if (num == 1)
		//	{
		//		if (Application.platform == RuntimePlatform.IPhonePlayer)
		//		{
		//			VectorNative.invokeNative(129, "1297180034");
		//		}
		//		else
		//		{
		//			VectorNative.invokeNative(129, "game.color.ballz.block.brick.puzzle.free.physics.balls");
		//		}
		//		VectorAds.invokeAds(109, "otherGame1");
		//	}
		//}
		//else
		//{
		//	if (Application.platform == RuntimePlatform.IPhonePlayer)
		//	{
		//		VectorNative.invokeNative(129, "1289907271");
		//	}
		//	else
		//	{
		//		VectorNative.invokeNative(129, "com.vector.game.puzzle.numberlink");
		//	}
		//	VectorAds.invokeAds(109, "otherGame0");
		//}
	}

	public void OnRemoveAds()
	{
		if (this.isEnd)
		{
			return;
		}
		AudioSystem.PlayOneShotEffect("btn");
		UIManager.selfInstance.noAdsThisTime = true;
		VectorAds.invokeAds(109, "pause_noads");
		VectorNative.invokeNative(112, "nlhexa_noads");
	}

    public void OnSoundSwitch(bool isOpen)
    {
        if (this.isEnd)
        {
            return;
        }
        AudioSystem.SwitchSound(isOpen);
        if (AudioSystem.isSound)
        {
            this.soundOn.SetActive(true);
            this.soundOff.SetActive(false);
        }
        else
        {
            this.soundOn.SetActive(false);
            this.soundOff.SetActive(true);
        }
        AudioSystem.PlayOneShotEffect("btn");
    }

	public Image imgNoAds;

	private int useSpIndex;

    public GameObject soundOn;

    public GameObject soundOff;
}