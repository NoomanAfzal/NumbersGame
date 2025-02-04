// dnSpy decompiler from Assembly-CSharp.dll class: Dot
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Dot : Widget
{
	protected new void OnEnable()
	{
		iTween.Stop(base.gameObject);
		iTween.Stop(this.waveDot.gameObject);
		this.mvToDead = false;
		this.mvToTarget = null;
		base.transform.localEulerAngles = Vector3.zero;
		(base.transform as RectTransform).sizeDelta = new Vector2(Game.main.cellWidth, Game.main.cellWidth);
		base.transform.localScale = Vector3.one;
		(base.transform as RectTransform).SetSiblingIndex(1);
		Game main = Game.main;
		main.onInvokeScaleUpdate = (Game.OnInvokeUpdate)Delegate.Combine(main.onInvokeScaleUpdate, new Game.OnInvokeUpdate(this.InvokeUpdate));
		this.waveDot.gameObject.SetActive(false);
		this.warnDot.gameObject.SetActive(false);
		this.shakeTargertZ = 0f;
	}

	protected new void OnDisable()
	{
		Game main = Game.main;
		main.onInvokeScaleUpdate = (Game.OnInvokeUpdate)Delegate.Remove(main.onInvokeScaleUpdate, new Game.OnInvokeUpdate(this.InvokeUpdate));
	}

	public void PlayBornScale(float delayTim = 0f)
	{
		iTween.Stop(base.gameObject);
		base.transform.localScale = Vector3.zero;
		iTween.ScaleTo(base.gameObject, iTween.Hash(new object[]
		{
			"scale",
			Vector3.one,
			"time",
			0.24f,
			"delay",
			delayTim,
			"looptype",
			iTween.LoopType.none,
			"easetype",
			iTween.EaseType.easeOutSine
		}));
	}

	public void PlayGoneScale(float delayTim = 0f)
	{
		iTween.Stop(base.gameObject);
		base.transform.localScale = Vector3.one;
		iTween.ScaleTo(base.gameObject, iTween.Hash(new object[]
		{
			"scale",
			Vector3.zero,
			"time",
			0.18f,
			"delay",
			delayTim,
			"looptype",
			iTween.LoopType.none,
			"easetype",
			iTween.EaseType.easeOutSine,
			"oncomplete",
			"GoneScaleEnd",
			"oncompletetarget",
			base.gameObject
		}));
	}

	private void GoneScaleEnd()
	{
		this.Clear(false);
	}

	public void PlayTipScale(float delayTim, bool needLoop = false, float scaleSize = 1.5f)
	{
		iTween.Stop(base.gameObject);
		base.transform.localScale = Vector3.one;
		iTween.ScaleTo(base.gameObject, iTween.Hash(new object[]
		{
			"scale",
			Vector3.one * scaleSize,
			"time",
			0.24f,
			"delay",
			delayTim,
			"looptype",
			(!needLoop) ? iTween.LoopType.none : iTween.LoopType.pingPong,
			"easetype",
			iTween.EaseType.easeOutSine
		}));
		if (!needLoop)
		{
			iTween.ScaleTo(base.gameObject, iTween.Hash(new object[]
			{
				"scale",
				Vector3.one,
				"time",
				0.24f,
				"delay",
				delayTim + 0.24f,
				"looptype",
				iTween.LoopType.none,
				"easetype",
				iTween.EaseType.easeOutSine
			}));
		}
	}

	public void PlayWarningShake()
	{
		this.warnDot.gameObject.SetActive(true);
		iTween.Stop(base.gameObject);
		if (this.shakeTargertZ == 0f)
		{
			this.shakeTargertZ = ((UnityEngine.Random.Range(0, 2) != 0) ? UnityEngine.Random.Range(4f, 6f) : UnityEngine.Random.Range(-6f, -4f));
		}
		else if (this.shakeTargertZ > 0f)
		{
			this.shakeTargertZ = UnityEngine.Random.Range(-6f, -4f);
		}
		else
		{
			this.shakeTargertZ = UnityEngine.Random.Range(4f, 6f);
		}
		iTween.RotateTo(base.gameObject, iTween.Hash(new object[]
		{
			"rotation",
			new Vector3(0f, 0f, this.shakeTargertZ),
			"time",
			UnityEngine.Random.Range(0.08f, 0.12f),
			"delay",
			0f,
			"looptype",
			iTween.LoopType.none,
			"easetype",
			iTween.EaseType.linear,
			"oncomplete",
			"PlayWarningShake",
			"oncompletetarget",
			base.gameObject
		}));
	}

	public void StopTip()
	{
		iTween.Stop(base.gameObject);
		this.warnDot.gameObject.SetActive(false);
		base.transform.localScale = Vector3.one;
		base.transform.localEulerAngles = Vector3.zero;
	}

	public void PlayWave(float delayTim)
	{
		iTween.Stop(this.waveDot.gameObject);
		this.waveDot.gameObject.SetActive(true);
		this.waveDot.color = new Color(this.imgDot.color.r, this.imgDot.color.g, this.imgDot.color.b, 0f);
		(this.waveDot.transform as RectTransform).sizeDelta = (this.imgDot.transform as RectTransform).sizeDelta;
		iTween.ValueTo(this.waveDot.gameObject, iTween.Hash(new object[]
		{
			"from",
			0,
			"to",
			1f,
			"time",
			0.24f,
			"delay",
			delayTim,
			"looptype",
			iTween.LoopType.none,
			"easetype",
			iTween.EaseType.easeOutSine,
			"onupdatetarget",
			base.gameObject,
			"onupdate",
			"WaveUpdate",
			"oncomplete",
			"WaveEnd",
			"oncompletetarget",
			base.gameObject
		}));
	}

	private void WaveUpdate(float v)
	{
		Color color = this.waveDot.color;
		color.a = 1f - v;
		this.waveDot.color = color;
		(this.waveDot.transform as RectTransform).sizeDelta = (this.imgDot.transform as RectTransform).sizeDelta * (0.5f * v + 1f);
	}

	private void WaveEnd()
	{
		this.waveDot.gameObject.SetActive(false);
	}

	public void UpdateNumLevel(int newNumLevel)
	{
		this.numberLevel = newNumLevel;
		this.number = 1L;
		for (int i = 0; i <= newNumLevel; i++)
		{
			this.number *= 2L;
		}
		int num = (newNumLevel < Res.LevelColor.Length) ? newNumLevel : (Res.LevelColor.Length - 1);
		Color color = Res.LevelColor[newNumLevel % Res.LevelColor.Length];
		this.imgDot.color = color;
		if (this.number < 1000L)
		{
			this.labDot.text = this.number.ToString();
		}
		else if (this.number < 1000000L)
		{
			this.labDot.text = (this.number / 1000L).ToString() + "K";
		}
		else if (this.number < 1000000000L)
		{
			this.labDot.text = (this.number / 1000000L).ToString() + "M";
		}
		else if (this.number < 1000000000000L)
		{
			this.labDot.text = (this.number / 1000000000L).ToString() + "B";
		}
		else
        {
            labDot.text = "∞";
            
        }
	}

	public void LowSiblingIndex()
	{
		(base.transform as RectTransform).SetSiblingIndex(0);
	}

	public void UpdatePos(float waitTim = 0f, RectTransform mvToTarget = null, bool isMvToDead = false)
	{
		this.waitTim = waitTim;
		this.mvToTarget = mvToTarget;
		if (mvToTarget != null)
		{
			this.LowSiblingIndex();
		}
		if ((int)this.mPos.x % 2 == 0)
		{
			this.targetPos = new Vector2(this.mPos.x * Game.main.cellWidth, this.mPos.y * Game.main.cellHeight) + Game.main.zeroOddPos;
		}
		else
		{
			this.targetPos = new Vector2(this.mPos.x * Game.main.cellWidth, this.mPos.y * Game.main.cellHeight) + Game.main.zeroEvenPos;
		}
		this.velocity = Vector2.zero;
		this.mvToDead = isMvToDead;
	}

	public override void Clear(bool isRemove = false)
	{
		this.mvToTarget = null;
		base.gameObject.SetActive(false);
		if (isRemove)
		{
			Destroy(base.gameObject);
            
		}
		else
		{
			Game.main.RecycleDot(this);
		}
	}

	public override void InvokeUpdate(float tim)
	{
		if (this.waitTim <= 0f)
		{
			RectTransform rectTransform = base.transform as RectTransform;
			Vector2 anchoredPosition = rectTransform.anchoredPosition;
			if (this.mvToTarget != null)
			{
				this.targetPos = this.mvToTarget.anchoredPosition;
			}
			if (anchoredPosition.x != this.targetPos.x || anchoredPosition.y != this.targetPos.y)
			{
				rectTransform.anchoredPosition = Vector2.SmoothDamp(anchoredPosition, this.targetPos, ref this.velocity, this.smoothTime, 2400f, tim);
				if ((anchoredPosition - this.targetPos).sqrMagnitude <= 1f)
				{
					rectTransform.anchoredPosition = this.targetPos;
					if (this.mvToTarget != null || this.mvToDead)
					{
						this.Clear(false);
					}
				}
			}
		}
		else
		{
			this.waitTim -= tim;
		}
	}

	public RectTransform baseDotTran;

	public Image waveDot;

	public Image imgDot;

	public TextMeshProUGUI labDot;

	public Image warnDot;

	internal int numberLevel;

	internal long number;

	internal RectTransform mvToTarget;

	private float waitTim;

	private Vector2 targetPos;

	private float smoothTime = 0.16f;

	private Vector2 velocity = Vector2.zero;

	private float shakeTargertZ;

	private bool mvToDead;
}
