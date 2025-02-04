// dnSpy decompiler from Assembly-CSharp.dll class: TouchBase
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchBase : MonoBehaviour
{
	private void Start()
	{
	}

	public void RegistTouchFunc(TouchBase.OnTouched callFunc)
	{
		this.onTouched = callFunc;
	}

	public void RegistLongTapFunc(TouchBase.OnLongTap callFunc)
	{
		this.onLongTap = callFunc;
	}

	public void RegistArrowTapFunc(TouchBase.OnGesture callFunc)
	{
		this.onGesture = callFunc;
	}

	protected void ActiveOnTouched(int index, TouchBase.TouchBaseType touchType, Vector3 point)
	{
		if (this.onTouched != null)
		{
			this.onTouched(index, touchType, point);
		}
	}

	protected void ActiveOnLongTap(TouchBase.TouchBaseType touchType, Vector3 point)
	{
		if (this.onLongTap != null)
		{
			this.onLongTap(touchType, point);
		}
	}

	protected void ActiveOnGesture(TouchBase.TouchBaseType touchType, TouchBase.ArrowType arrowType, Vector3 touchPos)
	{
		if (this.onGesture != null)
		{
			this.onGesture(touchType, arrowType, touchPos);
		}
	}

	private void AddTouchPoint(Vector3 pos)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.touchPoint);
		pos.z = -9f;
		gameObject.transform.localPosition = pos;
	}

	private void TouchEvent(TouchBase.TouchBaseType type, Vector3 point)
	{
		this.ActiveOnTouched(0, type, point);
	}

	public void InvokeUpdate(float timDelta)
	{
		if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsEditor)
		{
			if (!EventSystem.current.IsPointerOverGameObject())
			{
				if (Input.GetMouseButton(0))
				{
					this.touchStarted = true;
					Vector3 mousePosition = UnityEngine.Input.mousePosition;
					if (Input.GetMouseButtonDown(0))
					{
						this.TouchEvent(TouchBase.TouchBaseType.TouchBegan, mousePosition);
					}
					else
					{
						this.TouchEvent(TouchBase.TouchBaseType.TouchMove, mousePosition);
					}
				}
				else if (this.touchStarted)
				{
					this.touchStarted = false;
					Vector3 mousePosition2 = UnityEngine.Input.mousePosition;
					this.TouchEvent(TouchBase.TouchBaseType.TouchFinished, mousePosition2);
				}
			}
		}
		else if (0 < UnityEngine.Input.touchCount)
		{
			Vector3 point = UnityEngine.Input.GetTouch(0).position;
			if (UnityEngine.Input.GetTouch(0).phase == TouchPhase.Began)
			{
				this.TouchEvent(TouchBase.TouchBaseType.TouchBegan, point);
			}
			else if (UnityEngine.Input.GetTouch(0).phase == TouchPhase.Moved)
			{
				this.TouchEvent(TouchBase.TouchBaseType.TouchMove, point);
			}
			else if (UnityEngine.Input.GetTouch(0).phase == TouchPhase.Ended || UnityEngine.Input.GetTouch(0).phase == TouchPhase.Canceled)
			{
				this.TouchEvent(TouchBase.TouchBaseType.TouchFinished, point);
			}
		}
	}

	public GameObject touchPoint;

	protected TouchBase.OnTouched onTouched;

	protected TouchBase.OnLongTap onLongTap;

	protected TouchBase.OnGesture onGesture;

	protected bool touchStarted;

	protected Vector3 cachePoint = Vector3.zero;

	protected Vector3 additionLen = Vector3.zero;

	protected float additionTime;

	public enum TouchBaseType
	{
		TouchBegan,
		TouchMove,
		TouchFinished
	}

	public enum ArrowType
	{
		Null,
		Up,
		Right,
		Down,
		Left
	}

	public delegate void OnTouched(int index, TouchBase.TouchBaseType touchType, Vector3 touchPos);

	public delegate void OnLongTap(TouchBase.TouchBaseType touchType, Vector3 touchPos);

	public delegate void OnGesture(TouchBase.TouchBaseType touchType, TouchBase.ArrowType arrowType, Vector3 touchPos);
}
