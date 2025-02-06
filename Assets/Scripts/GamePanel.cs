// dnSpy decompiler from Assembly-CSharp.dll class: GamePanel
using System;
using System.Collections;
using System.IO;
using com.vector;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : BasePanel
{
    public static GamePanel instance;
    protected new void Start()
	{
		instance = this;
		base.Start();
		bestScore= PlayerPrefs.GetInt("BestScore");

	}

	protected new void OnEnable()
	{
		this.txtGuide.gameObject.SetActive(true);
		this.cancelGo.SetActive(false);
		base.OnEnable();
		this.gameGo.SetActive(true);
		this.UpdateLevel();
		Vector2 sizeDelta = Game.main.GetComponent<RectTransform>().sizeDelta;
		Vector2 anchoredPosition = this.topRect.anchoredPosition;
		anchoredPosition.y = sizeDelta.y / 2f + this.topRect.sizeDelta.y / 2f;
		this.topRect.anchoredPosition = anchoredPosition;
		//anchoredPosition = this.bottomRect.anchoredPosition; // noman commented
		//anchoredPosition.y = -sizeDelta.y / 2f - this.bottomRect.sizeDelta.y / 2f; // noman commented
		//this.bottomRect.anchoredPosition = anchoredPosition;
		float num = UIManager.selfInstance.canvas.GetComponent<RectTransform>().sizeDelta.y * 0.5f;
		float y = num + anchoredPosition.y - 20f;
		UIManager.selfInstance.topPanel.UpdateThemeBtn(y);
		UIManager.selfInstance.topPanel.SwitchThemeBtn(true);
        this.UpdateTipStatus1();
        KeyBoardListener kbl = UIManager.selfInstance.kbl;
		kbl.onBackKeyEvent = (KeyBoardListener.OnBackKeyEvent)Delegate.Combine(kbl.onBackKeyEvent, new KeyBoardListener.OnBackKeyEvent(this.OnBackKeyEvent));
	}

	//public void UpdateTipCounts()
	//{
	//	GameUser instance = GameUser.Instance;
	//	string text = DateTime.UtcNow.ToString("D");
	//	if (instance.dateStr != text)
	//	{
	//		instance.dateStr = text;
	//		if (UIManager.selfInstance.VAinstance.adData.refreshDaily && instance.lastTipTimes >= 1)
	//		{
	//			instance.lastTipTimes = 0;
	//			instance.lastTipTimeCount = 0L;
	//		}
	//	}
	//	int num = instance.lastTipTimes * instance.lastTipTimes * UIManager.selfInstance.VAinstance.adData.gameAddTipDeltaTime + UIManager.selfInstance.VAinstance.adData.gameStartTipDeltaTime;
	//	num = ((num <= UIManager.selfInstance.VAinstance.adData.gameMaxTipDeltaTime) ? num : UIManager.selfInstance.VAinstance.adData.gameMaxTipDeltaTime);
	//	instance.lastTipTimeCount = (long)(utils.CurrentSeconds() + num);
	//	instance.lastTipTimes++;
	//	instance.SaveLastGameData();
	//}
	public void SetButtonsInteractive()
	{
		lightGo.GetComponent<Button>().interactable = true;
		
	}
    public void UpdateTipStatus0()
    {
    //    //this.tipImg.gameObject.SetActive(true);
    //    //this.lightGo.SetActive(false);
    //    //this.removeGo.SetActive(false);
    //    //this.undoGo.SetActive(false);
    //    //this.cancelGo.SetActive(false);
    //    //if (Base._instance.IsRewardReady())
    //    //{
    //    //    this.isVideoCache = true;
    //    //    this.tipImg.color = this.pinkActiveColor;
    //    //}
    //    //if (GameUser.Instance.lastTipTimeCount == 0L || (long)utils.CurrentSeconds() > GameUser.Instance.lastTipTimeCount)
    //    //{
    //    //    this.emptyTouchTime = 0f;
    //    //    this.labTipCount.text = string.Empty;
    //    //    this.tipStatus0.gameObject.SetActive(true);
    //    //    this.tipStatus1.gameObject.SetActive(false);
    //    //}
    //    //else
    //    //{
    //    //    long num = GameUser.Instance.lastTipTimeCount - (long)utils.CurrentSeconds();
    //    //    this.labTipCount.text = ((int)num / 60).ToString((num / 60L < 10L) ? "D2" : string.Empty) + ":" + ((int)num % 60).ToString("D2");
    //    //    this.tipStatus0.gameObject.SetActive(false);
    //    //    this.tipStatus1.gameObject.SetActive(false);
    //    //}
       UpdateTipStatus1();
    }

	private void UpdateTipStatus1()
	{
        Base._instance.removeCount.text = RewardScriptableObject.instance.tipRemoveCount.ToString();
        Base._instance.ligtCount.text = RewardScriptableObject.instance.tipLightCount.ToString();
        Base._instance.undoCount.text = RewardScriptableObject.instance.tipUndoCount.ToString();
        Base._instance.removeCount.color = Color.black;
        Base._instance.ligtCount.color = Color.black;
        Base._instance.undoCount.color = Color.black;
        this.tipImg.gameObject.SetActive(false);
        this.lightGo.SetActive(true);
        this.removeGo.SetActive(true);
        this.undoGo.SetActive(true);
        this.cancelGo.SetActive(false);
        if (Game.main.undoDotList == null)
        {
                 //this.undoImg.color = new Color(0.5f, 0.5f, 0.5f, 1); // this.deactiveColor;
                 undoImg.sprite = undoDisable;
        }
        else
        {
                 //this.undoImg.color = Color.white; // this.yellowActiveColor;
                 undoImg.sprite = undoEnable;
        }
    }

	private void UpdateTipStatus2()
	{
		this.tipImg.gameObject.SetActive(false);
		this.lightGo.SetActive(true);
		this.removeGo.SetActive(false);
		this.undoGo.SetActive(true);
		this.cancelGo.SetActive(true);
    }

	protected new void OnDisable()
	{
		base.OnDisable();
		this.gameGo.SetActive(false);
		UIManager.selfInstance.topPanel.UpdateThemeBtn(82f);
		KeyBoardListener kbl = UIManager.selfInstance.kbl;
		kbl.onBackKeyEvent = (KeyBoardListener.OnBackKeyEvent)Delegate.Remove(kbl.onBackKeyEvent, new KeyBoardListener.OnBackKeyEvent(this.OnBackKeyEvent));
	}

	private void OnBackKeyEvent()
	{
		this.OnPause();
	}
	private void UpdateLevel()
	{
		this.guideHand.gameObject.SetActive(false);
		this.txtGuide.gameObject.SetActive(false);
		this.labNow.gameObject.SetActive(true);
		GameUser instance = GameUser.Instance;
		this.costTimeGo.SetActive(true);
		this.nowScore = instance.lastRecord;
		this.targetScore = this.nowScore;
		this.labNow.text = this.targetScore.ToString("N0");
		this.costTim = instance.lastTime;
		this.labCostTim.text = ((int)this.costTim / 60).ToString((this.costTim / 60f < 10f) ? "D2" : string.Empty) + ":" + ((int)this.costTim % 60).ToString("D2");
		Game.main.GameInit();
	}

	internal void AddScore(long addNum)
	{
		this.targetScore += addNum;
		Debug.Log("Addinggggggg scoreeeeee");
	}

	internal void DelayNgs(float delayTim)
	{
		base.CancelInvoke("InvokeAds");
		base.Invoke("InvokeAds", delayTim);
	}

	private void InvokeAds()
	{
		UIManager.selfInstance.VAinstance.ShowNGS(false);
	}

	internal void UpdateGuideHand(bool isShow, Vector2 newPos = default(Vector2))
	{
		this.guideHand.gameObject.SetActive(isShow);
		if (isShow)
		{
			this.txtGuide.gameObject.SetActive(true);
			this.labNow.gameObject.SetActive(false);
		}
		else
		{
			this.txtGuide.gameObject.SetActive(false);
			this.labNow.gameObject.SetActive(true);
		}
		this.guideHand.anchoredPosition = newPos;
	}

	// internal void GameFinish()
	// {
	// 	//this.isEnd = true;
	// 	//base.StartCoroutine("GoToPassPanel");
	// }

	private IEnumerator GoToPassPanel()
	{
		yield return new WaitForSeconds(0.1f);
		string filePath = utils.GetWritablePath(string.Empty) + "/share.png";
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android)
		{
			if (File.Exists(filePath))
			{
				File.Delete(filePath);
			}
			ScreenCapture.CaptureScreenshot("share.png");
		}
		else
		{
			if (!Directory.Exists(utils.GetWritablePath(string.Empty)))
			{
				Directory.CreateDirectory(utils.GetWritablePath(string.Empty));
			}
			if (File.Exists(filePath))
			{
				File.Delete(filePath);
			}
			ScreenCapture.CaptureScreenshot(filePath);
		}
		yield return new WaitForSeconds(0.5f);
		if (GameUser.Instance.isNewGame )
		{
			GameUser.Instance.isNewGame = false;
			GameUser.Save();
			//UIManager.selfInstance.revivePanel.gameObject.SetActive(true);
		}
		else
		{
			this.ToPassPanel();
		}
		yield break;
	}

	public void ToPassPanel()
	{
       // AdsInitScript._instance.gameStatus = GameState.GameOver;
		this.isEnd = false;
		//base.NextPanel(UIManager.selfInstance.passPanel.gameObject);
	}

	public void GameRevive()
	{
		this.isEnd = false;
		Game.main.Revive();
	}

	public void OnClose()
	{
		this.isEnd = true;
		base.gameObject.SetActive(false);
	}

	public void OnPause()
	{
		if (this.isEnd)
		{
			return;
		}
		this.isEnd = true;
		AudioSystem.PlayOneShotEffect("btn");
		GameUser.Instance.SaveLastGameData();
		UIManager.selfInstance.pausePanel.gameObject.SetActive(true);
		Game.main.status = Game.PLAYSTATUS.STOP; 
	}

	public void OnResume()
	{
		this.isEnd = false;
		UIManager.selfInstance.topPanel.SwitchThemeBtn(true);
		Game.main.status = Game.PLAYSTATUS.RUN;
	}

    public void OnTips()
    {
        if (this.isEnd || Game.main.isGuide || (long)utils.CurrentSeconds() < GameUser.Instance.lastTipTimeCount)
        {
            return;
        }
      
        //AdsInitScript._instance.ShowRewardByType(delegate
        //{
        //    this.UpdateTipStatus1();
        //});
        
    }

	public void InvokeAdsCallback(int type, string msg)
	{
		if (type != 1)
		{
			if (type == 0)
			{
				this.UpdateTipStatus1();
				VectorAdsInstance vainstance = UIManager.selfInstance.VAinstance;
				vainstance.mCallBack = (VectorAdsInstance.InvokeAdsCallback)Delegate.Remove(vainstance.mCallBack, new VectorAdsInstance.InvokeAdsCallback(this.InvokeAdsCallback));
			}
		}
		else
		{
			if (msg == "true")
			{
				this.UpdateTipStatus1();
			}
			VectorAdsInstance vainstance2 = UIManager.selfInstance.VAinstance;
			vainstance2.mCallBack = (VectorAdsInstance.InvokeAdsCallback)Delegate.Remove(vainstance2.mCallBack, new VectorAdsInstance.InvokeAdsCallback(this.InvokeAdsCallback));
		}
	}
	
	public void OnLight()
	{
        if (this.isEnd)
		{
			return;
		}
        if(RewardScriptableObject.instance.tipLightCount > 0 && !Game.main.onlighton)
        {
            Game.main.onlighton = true;
            AudioSystem.PlayOneShotEffect("btn");
            Game.main.TipLight();
            RewardScriptableObject.instance.tipLightCount--;
            this.UpdateTipStatus1();
			lightGo.GetComponent<Button>().interactable = false;
        }
		else if(RewardScriptableObject.instance.tipLightCount == 0)
		{
			ToastManager.instance.ShowToast("Please Purchase it or Watch video to get");
		}
    }

	public void OnRemove()
	{
		if (this.isEnd)
		{
			return;
		}
        if (RewardScriptableObject.instance.tipRemoveCount > 0)
        {
            AudioSystem.PlayOneShotEffect("btn");
            Game.main.TipRemove();
            this.UpdateTipStatus2();
        }
		else
		{
			ToastManager.instance.ShowToast("Please Purchase it or Watch video to get");
		}
	}

    public void OnUndo()
	{
		if (this.isEnd || Game.main.undoDotList == null)
		{
			return;
		}
        if (RewardScriptableObject.instance.tipUndoCount > 0)
        {
            AudioSystem.PlayOneShotEffect("btn");
            Game.main.TipUndo();
			this.UpdateTipStatus0();
        }
		else
		{
			ToastManager.instance.ShowToast("Please Purchase it or Watch video to get");
		}
	}

	public void FinishRemove()
	{
        //this.UpdateTipCounts();
        UpdateTipStatus1();
    }

	

	public void OnCancel()
	{
		if (this.isEnd)
		{
			return;
		}
		AudioSystem.PlayOneShotEffect("btn");
		Game.main.ClearTipStatus();
        //this.UpdateTipCounts();
        UpdateTipStatus1();
	}

	public void OnRestart()
	{
		this.isEnd = false;
		GameUser.Instance.ClearLastGameData();
		GameUser.Save();
		this.UpdateLevel();
	}

	private void OnApplicationPause(bool pauseStatus)
	{
		if (pauseStatus && Game.main.status != Game.PLAYSTATUS.STOP)
		{
			GameUser.Instance.SaveLastGameData();
		}
	}

	private new void Update()
	{
		if (Game.main.status == Game.PLAYSTATUS.RUN)
		{
			this.costTim += Time.deltaTime;
			this.labCostTim.text = ((int)this.costTim / 60).ToString((this.costTim / 60f < 10f) ? "D2" : string.Empty) + ":" + ((int)this.costTim % 60).ToString("D2");
			this.videoCheckTime += Time.deltaTime;
			if (this.videoCheckTime >= 5f && !this.isVideoCache)
			{
				this.videoCheckTime = 0f;
                
			}
			if (GameUser.Instance.lastTipTimeCount == 0L || (long)utils.CurrentSeconds() > GameUser.Instance.lastTipTimeCount)
			{
				if (this.isVideoCache)
				{
					this.emptyTouchTime += Time.deltaTime;
				}
				this.labTipCount.text = string.Empty;
				this.tipStatus0.gameObject.SetActive(this.emptyTouchTime < 15f);
				this.tipStatus1.gameObject.SetActive(this.emptyTouchTime >= 15f);
			}
			else
			{
				long num = GameUser.Instance.lastTipTimeCount - (long)utils.CurrentSeconds();
				this.labTipCount.text = ((int)num / 60).ToString((num / 60L < 10L) ? "D2" : string.Empty) + ":" + ((int)num % 60).ToString("D2");
				this.tipStatus0.gameObject.SetActive(false);
				this.tipStatus1.gameObject.SetActive(false);
			}
			if (Game.main.undoDotList == null)
			{
                //this.undoImg.color = new Color(0.5f, 0.5f, 0.5f, 1); //this.deactiveColor;
                undoImg.sprite = undoDisable;
            }
			else
			{
                //this.undoImg.color = Color.white; //this.yellowActiveColor;'
                undoImg.sprite = undoEnable;
			}
		}
		if (this.nowScore != this.targetScore)
		{
			long num2 = (long)((double)(this.targetScore - this.nowScore) * 0.2);
			if (Mathf.Abs((float)num2) <= 1f)
			{
				this.nowScore = this.targetScore;
			}
			else
			{
				this.nowScore += num2;
			}
			this.labNow.text = this.nowScore.ToString("N0");
			bestScore = (int)nowScore;
			
			

		}

        if (bestScore > currentScore)
        {
			
            currentScore = bestScore;
			
            //currentScore = PlayerPrefs.GetInt("BestScore", bestScore);
            PlayerPrefs.SetInt("BestScore", currentScore);

            bestScoretxt.text = currentScore.ToString();
        }
       

		
    }

	public Color pinkActiveColor;

	public Color yellowActiveColor;

	public Color deactiveColor;

	public Text labTipCount;

	public Image tipStatus0;

	public Image tipStatus1;

	public Image tipImg;

	public GameObject lightGo;

	public GameObject removeGo;

	public GameObject undoGo;

	public Image undoImg;

    public Sprite undoEnable;

    public Sprite undoDisable;

	public GameObject cancelGo;

	public RectTransform guideHand;

	public RectTransform topRect;

	public RectTransform bottomRect;

	public GameObject gameGo;

	public GameObject costTimeGo;

	public Text labNow;

	public Text labCostTim;

	public Text txtGuide;

	internal float costTim;

	internal long nowScore;

	internal long targetScore;

	internal float emptyTouchTime;

	private bool isVideoCache;

	private float videoCheckTime;
    public Text bestScoretxt;
    public int bestScore;
	public int currentScore;
}