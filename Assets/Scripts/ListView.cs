// dnSpy decompiler from Assembly-CSharp.dll class: ListView
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]
public class ListView : MonoBehaviour
{
	public void Init(int startIndex, int dataNum, ListView.OnItemShow onItemShow)
	{
		this.Clear();
		this.child.SetActive(false);
		this.sr = base.GetComponent<ScrollRect>();
		this.sr.onValueChanged.AddListener(new UnityAction<Vector2>(this.OnScrollValueChange));
		this.startIndex = startIndex;
		this.onItemShow = (ListView.OnItemShow)Delegate.Combine(this.onItemShow, onItemShow);
		if (this.sr.vertical)
		{
			this.childSize = this.child.GetComponent<RectTransform>().sizeDelta.y;
		}
		else
		{
			this.childSize = this.child.GetComponent<RectTransform>().sizeDelta.x;
		}
		if (this.sr.vertical)
		{
			this.viewSize = base.GetComponent<RectTransform>().rect.height;
		}
		else
		{
			this.viewSize = base.GetComponent<RectTransform>().rect.height;
		}
		int num = Mathf.FloorToInt(this.viewSize / this.childSize) + 2;
		for (int i = 0; i < num; i++)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.child);
			gameObject.SetActive(true);
			RectTransform component = gameObject.GetComponent<RectTransform>();
			RectTransform component2 = this.child.GetComponent<RectTransform>();
			component.SetParent(this.sr.content);
			component.offsetMin = component2.offsetMin;
			component.offsetMax = component2.offsetMax;
			component.anchoredPosition = component2.anchoredPosition;
			component.position = component2.position;
			component.localScale = component2.localScale;
			this.childs.Add(component);
		}
		this.ResetNum(dataNum);
		Vector2 anchoredPosition = this.sr.content.anchoredPosition;
		float num2 = (float)startIndex * this.childSize;
		num2 = ((num2 <= this.totalSize - this.viewSize + this.childSize) ? num2 : (this.totalSize - this.viewSize + this.childSize));
		if (this.sr.vertical)
		{
			anchoredPosition.y = num2;
		}
		else
		{
			anchoredPosition.x = num2;
		}
		this.sr.content.anchoredPosition = anchoredPosition;
	}

	public void OnScrollValueChange(Vector2 pos)
	{
		pos.x = 1f - pos.x;
		pos.y = 1f - pos.y;
		float num = pos.y * (this.totalSize - this.viewSize);
		int num2 = Mathf.FloorToInt(num / this.childSize);
		int num3 = Mathf.FloorToInt((num + this.viewSize) / this.childSize);
		float num4 = (num > 0f) ? num : 0f;
		num2 = ((num2 > 0) ? num2 : 0);
		this.minAllShowIndex = num2;
		this.maxAllShowIndex = num3;
		for (int i = 0; i < this.childs.Count; i++)
		{
			Vector2 anchoredPosition = this.childs[i].anchoredPosition;
			if (this.sr.vertical)
			{
				anchoredPosition.y = 1048575f;
			}
			else
			{
				anchoredPosition.x = 1048575f;
			}
			this.childs[i].anchoredPosition = anchoredPosition;
		}
		for (int j = num2; j <= num3; j++)
		{
			if (j >= 0 && j < this.maxNum)
			{
				int index = j % this.childs.Count;
				Vector2 anchoredPosition2 = this.childs[index].anchoredPosition;
				if (this.sr.vertical)
				{
					anchoredPosition2.y = (float)(-(float)j) * this.childSize;
				}
				else
				{
					anchoredPosition2.x = (float)(-(float)j) * this.childSize;
				}
				this.childs[index].anchoredPosition = anchoredPosition2;
				if (this.childs[index].gameObject.name != j.ToString())
				{
					this.childs[index].gameObject.name = j.ToString();
					if (this.onItemShow != null)
					{
						this.onItemShow(this.childs[index].gameObject, j);
					}
				}
			}
		}
	}

	public void ResetNum(int maxNum)
	{
		this.maxNum = maxNum;
		Vector2 sizeDelta = this.sr.content.sizeDelta;
		if (this.sr.vertical)
		{
			sizeDelta.y = this.childSize * (float)maxNum;
			this.totalSize = sizeDelta.y;
		}
		else
		{
			sizeDelta.x = this.childSize * (float)maxNum;
			this.totalSize = sizeDelta.x;
		}
		this.sr.content.sizeDelta = sizeDelta;
	}

	public void RefreshView(int index = -1)
	{
		for (int i = 0; i < this.childs.Count; i++)
		{
			this.childs[i].name = string.Empty;
		}
		if (index == -1)
		{
			this.OnScrollValueChange(this.sr.normalizedPosition);
		}
		else
		{
			Vector2 anchoredPosition = this.sr.content.anchoredPosition;
			float num = (float)index * this.childSize;
			num = ((num <= this.totalSize - this.viewSize + this.childSize) ? num : (this.totalSize - this.viewSize + this.childSize));
			if (this.sr.vertical)
			{
				anchoredPosition.y = num;
			}
			else
			{
				anchoredPosition.x = num;
			}
			this.sr.content.anchoredPosition = anchoredPosition;
			this.OnScrollValueChange(this.sr.normalizedPosition);
		}
	}

	public void Clear()
	{
		this.maxNum = 0;
		if (this.sr != null)
		{
			this.sr.onValueChanged.RemoveAllListeners();
		}
		if (this.childs.Count > 0)
		{
			for (int i = 0; i < this.childs.Count; i++)
			{
				Destroy(this.childs[i].gameObject);
			}
			this.childs.Clear();
		}
	}

	private void OnDestroy()
	{
		this.Clear();
	}

	public GameObject child;

	public ListView.OnItemShow onItemShow;

	public int maxNum;

	[HideInInspector]
	public int minAllShowIndex;

	[HideInInspector]
	public int maxAllShowIndex;

	private List<RectTransform> childs = new List<RectTransform>();

	private ScrollRect sr;

	private int startIndex;

	private float childSize;

	private float viewSize;

	private float totalSize;

	public delegate void OnItemShow(GameObject item, int index);
}
