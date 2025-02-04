// dnSpy decompiler from Assembly-CSharp.dll class: BkgPanel
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BkgPanel : BasePanel
{
	protected new void Start()
	{
		base.Start();
        StartCoroutine(CheckVideoRoutine());
	}

	protected new void OnEnable()
	{
		base.OnEnable();
	}

	protected new void OnDisable()
	{
		base.OnDisable();
	}

	public void SelectTheme(int index)
	{
		//for (int i = 0; i < this.bkgImgs.Length; i++)
		//{
		//	this.bkgImgs[i].sprite = this.themeSprite[index];
		//	//this.bkgImgs[i].color = this.themeColor[index];
		//}
		//if (index == 1)
		//{
		//	for (int j = 0; j < this.lightImgs.Length; j++)
		//	{
		//		this.lightImgs[j].color = this.lightDark[1];
		//	}
		//	for (int k = 0; k < this.lightLabs.Length; k++)
		//	{
		//		this.lightLabs[k].color = this.lightDark[1];
		//	}
		//}
		//else
		//{
		//	for (int l = 0; l < this.lightImgs.Length; l++)
		//	{
		//		this.lightImgs[l].color = this.lightDark[0];
		//	}
		//	for (int m = 0; m < this.lightLabs.Length; m++)
		//	{
		//		this.lightLabs[m].color = this.lightDark[0];
		//	}
		//}
  //      MenuPanel.instance.soundOn.GetComponent<Image>().sprite = soundOnSpr[index];
  //      MenuPanel.instance.soundOff.GetComponent<Image>().sprite = soundOffSpr[index];
  //      themeBase[0].sprite = themeBaseSpr[index];
  //      themeBase[1].sprite = themeBaseSpr[index];
  //      MenuPanel.instance.rateGo.GetComponent<Image>().sprite = rateBtnSpr[index];
  //      MenuPanel.instance.enNoAds.GetComponent<Image>().sprite = noAdsBtnSpr[index];
  //      themeIcon.sprite = themeIconSpr[index];
  //      videoIcon.sprite = videoIconSpr[index];
  //      topImg.sprite = topImgSpr[index];
    }

	IEnumerator CheckVideoRoutine()
    {
        //if (Base._instance.IsRewardReady())
        //{
        //    notiGo.gameObject.SetActive(true);
        //}
        //else
        //{
        //    notiGo.gameObject.SetActive(false);
        //}
        yield return new WaitForSeconds(10);
        StartCoroutine(CheckVideoRoutine());
    }

	public Image[] bkgImgs;

    public Sprite[] soundOnSpr;

    public Sprite[] soundOffSpr;

    public Image[] themeBase;

    public Sprite[] themeBaseSpr;

    public Sprite[] rateBtnSpr;

    public Sprite[] noAdsBtnSpr;

    public Image themeIcon;

    public Sprite[] themeIconSpr;

    public Image videoIcon;

    public Sprite[] videoIconSpr;

    public Image topImg;

    public Sprite[] topImgSpr;

	public Color[] lightDark;

	public Image[] lightImgs;

	public Text[] lightLabs;

	public int themeCount;

	public Sprite[] themeSprite;

	//public Color[] themeColor;

	private float targetPec;

    public GameObject notiGo;
}
