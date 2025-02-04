// dnSpy decompiler from Assembly-CSharp.dll class: Line
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Line : Widget
{
	protected new void OnEnable()
	{
		Game main = Game.main;
		main.onInvokeScaleUpdate = (Game.OnInvokeUpdate)Delegate.Combine(main.onInvokeScaleUpdate, new Game.OnInvokeUpdate(this.InvokeUpdate));
	}

	protected new void OnDisable()
	{
		Game main = Game.main;
		main.onInvokeScaleUpdate = (Game.OnInvokeUpdate)Delegate.Remove(main.onInvokeScaleUpdate, new Game.OnInvokeUpdate(this.InvokeUpdate));
	}

	public void UpdateNumLevel(int newNumLevel)
	{
		this.numberLevel = newNumLevel;
		this.baseLineImg.color = Res.LevelColor[newNumLevel % Res.LevelColor.Length];
	}

	public override void Clear(bool isRemove = false)
	{
		this.linkDots.Clear();
		base.gameObject.SetActive(false);
		if (isRemove)
		{
			Destroy(base.gameObject);
		}
		else
		{
			Game.main.RecycleLine(this);
		}
	}

	public override void InvokeUpdate(float tim)
	{
		if (this.linkDots.Count < 1)
		{
			return;
		}
		if (this.linkDots.Count == 2)
		{
			this.targetPos = this.linkDots[1].anchoredPosition;
		}
		(base.transform as RectTransform).anchoredPosition = this.linkDots[0].anchoredPosition;
		float x = Vector2.Distance(this.linkDots[0].anchoredPosition, this.targetPos);
		Vector2 sizeDelta = this.baseLineTran.sizeDelta;
		sizeDelta.x = x;
		this.baseLineTran.sizeDelta = sizeDelta;
		Vector2 vector = this.targetPos - this.linkDots[0].anchoredPosition;
		float z = Mathf.Atan2(vector.y, vector.x) * 57.29578f;
		this.baseLineTran.localEulerAngles = new Vector3(0f, 0f, z);
	}

	public RectTransform baseLineTran;

	public Image baseLineImg;

	internal List<RectTransform> linkDots = new List<RectTransform>();

	internal Vector2 targetPos = Vector2.zero;

	internal int numberLevel;
}
