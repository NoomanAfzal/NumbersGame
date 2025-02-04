// dnSpy decompiler from Assembly-CSharp.dll class: Game
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Game : MonoBehaviour
{
    public static Game instane;
    private void Awake()
    {
        instane = this;
        Game.main = this;
        this.touchBase = base.GetComponent<TouchBase>();
        this.touchBase.RegistTouchFunc(new TouchBase.OnTouched(this.OnTouched));
    }

    private void Start()
    {
       
    }

    public void GameInit()
    {
        this.GameClear(false);
        this.nowMaxLv = 3;
        this.holdTouchTim = 0f;
        this.touchTime = 0f;
        this.status = Game.PLAYSTATUS.RUN;
        Vector2 sizeDelta = UIManager.selfInstance.canvas.GetComponent<RectTransform>().sizeDelta;
        float num = sizeDelta.y - 360f - this.offset.y * 2f;
        this.cellSizeY = Mathf.FloorToInt(num / this.cellHeight);
        this.cellSizeY = Mathf.Clamp(this.cellSizeY, 6, 8); // Keep the number of rows between 6 and 8
        this.cellSizeX = 5; // Set the number of columns to 5 for a 5x8 grid

        // Calculate cell width based on the desired aspect ratio (adjust as needed)
        this.cellWidth = (sizeDelta.x * 0.96f - this.offset.x * 2f) / this.cellSizeX;

        Vector2 sizeDelta2 = new Vector2((float)this.cellSizeX * this.cellWidth + this.offset.x * 2f, ((float)this.cellSizeY + 0.5f) * this.cellHeight + this.offset.y * 2f);
        base.GetComponent<RectTransform>().sizeDelta = sizeDelta2;

        // Adjust the starting positions accordingly (adjust as needed)
        this.zeroOddPos = new Vector2(-sizeDelta2.x / 2f + this.cellWidth / 2f + this.offset.x, -sizeDelta2.y / 2f + this.cellHeight / 2f + this.offset.y);
        this.zeroEvenPos = new Vector2(-sizeDelta2.x / 2f + this.cellWidth / 2f + this.offset.x, -sizeDelta2.y / 2f + this.cellHeight / 2f + this.offset.y);

        if (this.dots == null)
        {
            this.dots = new Dot[this.cellSizeX][];
            for (int i = 0; i < this.cellSizeX; i++)
            {
                this.dots[i] = new Dot[this.cellSizeY];
                for (int j = 0; j < this.cellSizeY; j++)
                {
                    this.dots[i][j] = null;
                }
            }
        }

        this.tipStatus = Game.TIPSTATUS.NONE;
        this.ClearTipStatus();
        this.ClearUndo();

        GameUser instance = GameUser.Instance;
        this.isGuide = (instance.guideStep < 4);

        if (this.isGuide)
        {
            this.GuideFill();
        }
        else
        {
            AudioSystem.playEffect("pageflip_more");
            this.FillDots(true);
        }

        this.Update();
    }





    public void GameFinish()
    {
        //	this.status = Game.PLAYSTATUS.STOP;
        //	UIManager.selfInstance.gamePanel.GameFinish();
    }

    private void FillDots(bool firstFill = false)
    {
        int num = 0;
        GameUser instance = GameUser.Instance;
        for (int i = 0; i < this.cellSizeX; i++)
        {
            int num2 = 1;
            int num3 = 0;
            for (int j = 0; j < this.cellSizeY; j++)
            {
                Dot x = this.dots[i][j];
                if (x == null)
                {
                    int num4 = -1;
                    for (int k = j + 1; k < this.cellSizeY; k++)
                    {
                        if (this.dots[i][k] != null)
                        {
                            num4 = k;
                            break;
                        }
                    }
                    if (num4 != -1)
                    {
                        this.dots[i][j] = this.dots[i][num4];
                        this.dots[i][num4] = null;
                        this.dots[i][j].mPos.y = (float)j;
                    }
                    else
                    {
                        int num5 = this.cellSizeY - 1 + (this.cellSizeX - 5) + UIManager.selfInstance.VAinstance.adData.gameExtraLevel;
                        num5 = ((num5 > 3) ? num5 : 3);
                        int num6 = UnityEngine.Random.Range(0, (this.nowMaxLv <= num5) ? this.nowMaxLv : num5);
                        if (firstFill && instance.lastGames.Count > i * this.cellSizeY + j)
                        {
                            num6 = instance.lastGames[i * this.cellSizeY + j];
                            this.nowMaxLv = ((this.nowMaxLv >= num6) ? this.nowMaxLv : num6);
                        }
                        this.dots[i][j] = this.NewDot(num6, i, j, ((i % 2 != 0) ? this.zeroEvenPos : this.zeroOddPos) + new Vector2((float)i * this.cellWidth, this.cellHeight * (float)(this.cellSizeY + num2)));
                        num2++;
                    }
                    this.dots[i][j].UpdatePos((float)num3 * 0.1f, null, false);
                    num3++;
                }
            }
            if (num3 > num)
            {
                num = num3;
            }
        }
        this.holdTouchTim += (float)num * 0.1f + 0.12f;
    }

    private void GuideDotClear()
    {
        while (this.guideDot.Count > 0)
        {
            this.dots[(int)this.guideDot[0].mPos.x][(int)this.guideDot[0].mPos.y] = null;
            this.guideDot[0].Clear(false);
            this.guideDot.RemoveAt(0);
        }
    }

    private void GuideFill()
    {
        GameUser instance = GameUser.Instance;
        this.needGuideHand = true;
        this.guideMvTime = 0f;
        bool flag = this.cellSizeX % 2 == 0;
        int num = this.cellSizeX / 2;
        int num2 = this.cellSizeY / 2;
        if (instance.guideStep == 0)
        {
            if (flag)
            {
                num--;
            }
            num2++;
            for (int i = num2; i > num2 - 2; i--)
            {
                this.dots[num][i] = this.NewDot(0, num, i, ((num % 2 != 0) ? this.zeroEvenPos : this.zeroOddPos) + new Vector2((float)num * this.cellWidth, (float)i * this.cellHeight));
                this.guideDot.Add(this.dots[num][i]);
            }
        }
        else if (instance.guideStep == 1)
        {
            this.GuideDotClear();
            num--;
            int j = num;
            int num3 = 0;
            while (j < num + 3)
            {
                int num4 = num3 - 1;
                num4 = ((num4 > 0) ? num4 : 0);
                int num5 = num2;
                this.dots[j][num5] = this.NewDot(num4, j, num5, ((j % 2 != 0) ? this.zeroEvenPos : this.zeroOddPos) + new Vector2((float)j * this.cellWidth, (float)num5 * this.cellHeight));
                this.guideDot.Add(this.dots[j][num5]);
                j++;
                num3++;
            }
        }
        else if (instance.guideStep == 2)
        {
            this.GuideDotClear();
            num = ((this.cellSizeX >= 6) ? 1 : 0);
            for (int k = num; k < num + 4; k++)
            {
                this.dots[k][num2] = this.NewDot(0, k, num2, ((k % 2 != 0) ? this.zeroEvenPos : this.zeroOddPos) + new Vector2((float)k * this.cellWidth, (float)num2 * this.cellHeight));
                this.guideDot.Add(this.dots[k][num2]);
            }
        }
        else if (instance.guideStep == 3)
        {
            this.GuideDotClear();
            num = this.cellSizeX - 1;
            int l = num;
            int num6 = 4;
            while (l > num - 5)
            {
                int num7 = num6 - 2;
                num7 = ((num7 > 0) ? num7 : 0);
                this.dots[l][num2] = this.NewDot(num7, l, num2, ((l % 2 != 0) ? this.zeroEvenPos : this.zeroOddPos) + new Vector2((float)l * this.cellWidth, (float)num2 * this.cellHeight));
                this.guideDot.Insert(0, this.dots[l][num2]);
                l--;
                num6--;
            }
        }
    }

    private Dot NewDot(int numberLevel, int x, int y, Vector2 startPos)
    {
        Dot dot;
        if (this.cacheDots.Count <= 0)
        {
            GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("prefabs/dot")) as GameObject;
            dot = gameObject.GetComponent<Dot>();
        }
        else
        {
            dot = this.cacheDots.Dequeue();
        }
        dot.UpdateNumLevel(numberLevel);
        dot.transform.SetParent(this.dotsParent);
        (dot.transform as RectTransform).anchoredPosition3D = Vector3.zero;
        dot.transform.localScale = Vector3.one;
        dot.mPos.x = (float)x;
        dot.mPos.y = (float)y;
        (dot.transform as RectTransform).anchoredPosition = startPos;
        dot.UpdatePos(0f, null, false);
        dot.gameObject.SetActive(true);
        return dot;
    }

    private Line NewLine(Dot lDot)
    {
        Line line;
        if (this.cacheLines.Count <= 0)
        {
            GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("prefabs/line")) as GameObject;
            line = gameObject.GetComponent<Line>();
           
        }
        else
        {
            line = this.cacheLines.Dequeue();
        }
        line.UpdateNumLevel(lDot.numberLevel);
        line.transform.SetParent(this.linesParent);
        (line.transform as RectTransform).anchoredPosition3D = Vector3.zero;
        line.transform.localScale = Vector3.one;
        line.linkDots.Add(lDot.transform as RectTransform);
        line.gameObject.SetActive(true);
        GamePanel.instance.SetButtonsInteractive();
        return line;
    }

    public void RecycleDot(Dot dot)
    {
        if (!this.cacheDots.Contains(dot))
        {
            this.cacheDots.Enqueue(dot);
        }
    }

    public void RecycleLine(Line line)
    {
        if (!this.cacheLines.Contains(line))
        {
            this.cacheLines.Enqueue(line);
        }
    }

    public void GameClear(bool isRemove)
    {
        this.guideDot.Clear();
        if (this.dots != null)
        {
            for (int i = 0; i < this.dots.Length; i++)
            {
                for (int j = 0; j < this.dots[i].Length; j++)
                {
                    if (this.dots[i][j] != null)
                    {
                        this.dots[i][j].Clear(isRemove);
                        this.dots[i][j] = null;
                    }
                }
            }
        }
        this.ClearLine(isRemove, -1);
    }

    private void ClearLine(bool isRemove, int maxNum = -1)
    {
        onlighton = false;
        while (this.linkLines.Count > 0 && (maxNum <= -1 || maxNum > 0))
        {
            this.linkLines[this.linkLines.Count - 1].Clear(isRemove);
            this.linkLines.RemoveAt(this.linkLines.Count - 1);
            maxNum--;
        }
    }

    private void StartTouch(Vector2 pos)
    {
        this.touchTime = Time.unscaledTime;
        UIManager.selfInstance.gamePanel.emptyTouchTime = 0f;
        if (this.tipStatus != Game.TIPSTATUS.REMOVE)
        {
            this.ClearLine(false, -1);
            int num = Mathf.RoundToInt((pos.x - this.zeroOddPos.x) / this.cellWidth);
            int num2 = Mathf.RoundToInt((pos.y - this.zeroOddPos.y) / this.cellHeight);

            if (num >= 0 && num < this.cellSizeX && num2 >= 0 && num2 < this.cellSizeY && this.dots[num][num2] != null)
            {
                this.needGuideHand = false;
                this.dots[num][num2].PlayWave(0f);
                Line line = this.NewLine(this.dots[num][num2]);
                line.targetPos = pos;
                this.linkLines.Add(line);
                this.nowLinkLv = this.dots[num][num2].numberLevel;
                this.maxLinkLv = this.dots[num][num2].numberLevel;
                AudioSystem.playEffect("p1");
            }
        }
    }

private void HoldTouch(Vector2 pos)
{
    if (this.tipStatus != Game.TIPSTATUS.REMOVE)
    {
        if (this.linkLines.Count > 0)
        {
            int num = Mathf.RoundToInt((pos.x - this.zeroOddPos.x) / this.cellWidth);
            int num2 = Mathf.RoundToInt((pos.y - this.zeroOddPos.y) / this.cellHeight);

            if (num >= 0 && num < this.cellSizeX && num2 >= 0 && num2 < this.cellSizeY && this.dots[num][num2] != null)
            {
                // Check if the dot is already in ANY part of the chain
                bool alreadyInChain = false;
                foreach (Line line in this.linkLines)
                {
                    foreach (RectTransform existingDot in line.linkDots)
                    {
                        if (this.dots[num][num2].transform == existingDot)
                        {
                            alreadyInChain = true;
                            break;
                        }
                    }
                    if (alreadyInChain) break;
                }

                // Only add the dot if it's not already in the chain
                if (!alreadyInChain && 
                    this.dots[num][num2].transform != this.linkLines[this.linkLines.Count - 1].linkDots[0] && 
                    (this.dots[num][num2].numberLevel == this.nowLinkLv || 
                     this.dots[num][num2].numberLevel == this.maxLinkLv))
                {
                    Dot component = this.linkLines[this.linkLines.Count - 1].linkDots[0].GetComponent<Dot>();
                    bool flag = false;
                    for (int j = -1; j <= 1; j++)
                    {
                        for (int k = -1; k <= 1; k++)
                        {
                            int num3 = (int)component.mPos.x + j;
                            int num4 = (int)component.mPos.y + k;

                            if (num == num3 && num2 == num4)
                            {
                                flag = true;
                                break;
                            }
                        }
                    }
                    if (flag)
                    {
                        this.dots[num][num2].PlayWave(0f);
                        this.linkLines[this.linkLines.Count - 1].linkDots.Add(this.dots[num][num2].transform as RectTransform);
                        Line line = this.NewLine(this.dots[num][num2]);
                        this.linkLines.Add(line);
                        this.nowLinkLv = line.numberLevel;
                        this.maxLinkLv = this.nowLinkLv + 1;
                        AudioSystem.playEffect("p" + (this.linkLines.Count % 14 + 1).ToString());
                    }
                }
            }
            this.linkLines[this.linkLines.Count - 1].targetPos = pos;
        }
    }
}
    public bool onlighton = false;
    private void EndTouch(Vector2 pos)
    {
        if (this.tipStatus == Game.TIPSTATUS.REMOVE)
        {
            if (Time.unscaledTime - this.touchTime <= 0.2f)
            {
                int num = Mathf.RoundToInt((pos.x - this.zeroOddPos.x) / this.cellWidth);
                int num2 = Mathf.RoundToInt((pos.y - this.zeroOddPos.y) / this.cellHeight);

                if (num >= 0 && num < this.cellSizeX && num2 >= 0 && num2 < this.cellSizeY && this.dots[num][num2] != null)
                {
                    AudioSystem.PlayOneShotEffect("btn");
                    this.undoDotTarget = null;
                    this.undoDotList = new List<Game.SimpleDot>();
                    this.undoDotList.Add(new Game.SimpleDot(this.dots[num][num2].numberLevel, this.dots[num][num2].mPos));
                    this.undoScore = 0L;
                    this.holdTouchTim = 0.2f;
                    this.ClearTipStatus();
                    onlighton = false;
                    this.dots[num][num2].PlayGoneScale(0f);
                    this.dots[num][num2] = null;

                    this.FillDots(false);
                    RewardScriptableObject.instance.tipRemoveCount--;
                    UIManager.selfInstance.gamePanel.FinishRemove();
                }
            }
        }
        else
        {
            onlighton = false;
            if (this.linkLines.Count > 1)
            {
                long num3 = 0L;
                this.holdTouchTim += 0.1f;
                Dot component = this.linkLines[this.linkLines.Count - 1].linkDots[0].GetComponent<Dot>();
                if (!this.isGuide)
                {
                    this.undoDotTarget = new Game.SimpleDot(component.numberLevel, component.mPos);
                    this.undoDotList = new List<Game.SimpleDot>();
                }
                num3 += component.number;
                AudioSystem.playEffect("pageflip");
                for (int i = 0; i < this.linkLines.Count - 1; i++)
                {
                    Dot component2 = this.linkLines[i].linkDots[0].GetComponent<Dot>();
                    if (!this.isGuide)
                    {
                        this.undoDotList.Add(new Game.SimpleDot(component2.numberLevel, component2.mPos));
                    }
                    this.dots[(int)component2.mPos.x][(int)component2.mPos.y] = null;
                    component2.mPos = component.mPos;
                    component2.UpdatePos(0f, component.transform as RectTransform, false);
                    num3 += component2.number;
                }
                long num4 = 1L;
                int num5 = 0;
                for (; ; )
                {
                    num4 *= 2L;
                    if (num4 > num3)
                    {
                        break;
                    }
                    num5++;
                }
                num5--;
                component.UpdateNumLevel(num5);
                if (this.nowMaxLv < component.numberLevel)
                {
                    this.nowMaxLv = component.numberLevel;
                    component.PlayTipScale(0f, false, 1.5f);
                    AudioSystem.playEffect("newlevel");
                    if (this.nowMaxLv >= UIManager.selfInstance.VAinstance.adData.gameAdLevel)
                    {
                        UIManager.selfInstance.gamePanel.DelayNgs(0.36f);
                    }
                }
                if (!this.isGuide)
                {
                    UIManager.selfInstance.gamePanel.AddScore(num3);
                }
                if (this.isGuide)
                {
                    GameUser.Instance.guideStep++;
                    if (GameUser.Instance.guideStep >= 4)
                    {
                        this.needGuideHand = false;
                        this.isGuide = false;
                        UIManager.selfInstance.gamePanel.UpdateGuideHand(false, default(Vector2));
                        GameUser.Save();
                        AudioSystem.playEffect("pageflip_more");
                    }
                    else
                    {
                        base.Invoke("GuideFill", 1f);
                    }
                }
                if (!this.isGuide)
                {
                   // UIManager.selfInstance.gamePanel.AddScore(num3); noman commented this line and added at 474 to 477
                    this.undoScore = -num3;
                    this.FillDots(false);
                    if (this.tipStatus == Game.TIPSTATUS.LIGHT)
                    {
                        this.ClearTipStatus();
                    }
                }
                else
                {
                    // Explicitly ensure no score is added during the guide
                    Debug.Log("Guide mode active: Score not added.");
                }
            }
            else
            {
                onlighton = false;
                this.needGuideHand = true;
            }
            this.ClearLine(false, -1);
        }
    }

    public void ClearTipStatus()
    {
        this.tipStatus = Game.TIPSTATUS.NONE;
        for (int i = 0; i < this.cellSizeX; i++)
        {
            for (int j = 0; j < this.cellSizeY; j++)
            {
                if (this.dots[i][j] != null)
                {
                    this.dots[i][j].StopTip();
                }
            }
        }
    }

    public void TipLight()
    {
        this.tipStatus = Game.TIPSTATUS.LIGHT;
        List<Dot> list = new List<Dot>();
        for (int i = 0; i < this.cellSizeX; i++)
        {
            for (int j = 0; j < this.cellSizeY; j++)
            {
                if (!list.Contains(this.dots[i][j]))
                {
                    for (int k = 0; k < this.CheckArrow.GetLength(0); k++)
                    {
                        int num = (int)this.dots[i][j].mPos.x + this.CheckArrow[k, 0];
                        int num2;
                        if ((int)this.dots[i][j].mPos.x % 2 == 0 || this.CheckArrow[k, 0] == 0)
                        {
                            num2 = (int)this.dots[i][j].mPos.y + this.CheckArrow[k, 1];
                        }
                        else
                        {
                            num2 = (int)this.dots[i][j].mPos.y + this.CheckArrow[k, 1] + 1;
                        }
                        if (num >= 0 && num < this.cellSizeX && num2 >= 0 && num2 < this.cellSizeY && this.dots[i][j].numberLevel == this.dots[num][num2].numberLevel)
                        {
                            if (!list.Contains(this.dots[i][j]))
                            {
                                list.Add(this.dots[i][j]);
                                this.dots[i][j].PlayTipScale(0f, true, 1.12f);
                            }
                            if (!list.Contains(this.dots[num][num2]))
                            {
                                list.Add(this.dots[num][num2]);
                                this.dots[num][num2].PlayTipScale(0f, true, 1.12f);
                            }
                        }
                    }
                }
            }
        }
    }

    public void TipRemove()
    {
        this.tipStatus = Game.TIPSTATUS.REMOVE;
        for (int i = 0; i < this.cellSizeX; i++)
        {
            for (int j = 0; j < this.cellSizeY; j++)
            {
                this.dots[i][j].PlayWarningShake();
            }
        }
    }

    private void ClearUndo()
    {
        this.undoDotList = null;
        this.undoDotTarget = null;
        this.undoScore = 0L;
    }

    public void TipUndo()
    {
        float num = 0f;
        if (this.undoDotList.Count != 0)
        {
            for (int i = 0; i < this.dots.Length; i++)
            {
                bool flag = false;
                for (int j = 0; j < this.dots[i].Length; j++)
                {
                    if (this.undoDotTarget != null && this.dots[i][j].mPos.Equals(this.undoDotTarget.mPos))
                    {
                        this.dots[i][j].UpdateNumLevel(this.undoDotTarget.numberLevel);
                        flag = true;
                    }
                    else
                    {
                        Game.SimpleDot simpleDot = null;
                        for (int k = 0; k < this.undoDotList.Count; k++)
                        {
                            if (this.dots[i][j].mPos.Equals(this.undoDotList[k].mPos))
                            {
                                simpleDot = this.undoDotList[k];
                                break;
                            }
                        }
                        if (simpleDot != null)
                        {
                            flag = true;
                            for (int l = this.dots[i].Length - 1; l >= j; l--)
                            {
                                if (l >= this.cellSizeY - 1)
                                {
                                    this.dots[i][l].mPos = new Vector2((float)i, (float)(l + 2));
                                }
                                else
                                {
                                    this.dots[i][l].mPos = new Vector2((float)i, (float)(l + 1));
                                }
                                Dot dot = this.dots[i][l];
                                float waitTim = num;
                                bool isMvToDead = l + 1 >= this.dots[i].Length;
                                dot.UpdatePos(waitTim, null, isMvToDead);
                                if (l < this.dots[i].Length - 1)
                                {
                                    this.dots[i][l + 1] = this.dots[i][l];
                                }
                            }
                            if (this.undoDotTarget != null)
                            {
                                this.dots[i][j] = this.NewDot(simpleDot.numberLevel, i, j, ((i % 2 != 0) ? this.zeroEvenPos : this.zeroOddPos) + this.undoDotTarget.mPos * this.cellWidth);
                                this.dots[i][j].LowSiblingIndex();
                                this.dots[i][j].UpdatePos(num, null, false);
                            }
                            else
                            {
                                this.dots[i][j] = this.NewDot(simpleDot.numberLevel, i, j, ((i % 2 != 0) ? this.zeroEvenPos : this.zeroOddPos) + simpleDot.mPos * this.cellWidth);
                                this.dots[i][j].PlayBornScale(num);
                            }
                        }
                    }
                }
                if (flag)
                {
                    num += 0.1f;
                }
            }
        }
        RewardScriptableObject.instance.tipUndoCount--;
        UIManager.selfInstance.gamePanel.AddScore(this.undoScore);
        this.ClearUndo();
    }

    public void Revive()
    {
        this.ClearUndo();
        List<Dot> list = new List<Dot>();
        for (int i = 0; i < this.dots.Length; i++)
        {
            for (int j = 0; j < this.dots[i].Length; j++)
            {
                list.Add(this.dots[i][j]);
            }
        }
        list.Sort((Dot x, Dot y) => -x.numberLevel + y.numberLevel);
        for (int k = 0; k < this.cellSizeY; k++)
        {
            for (int l = 0; l <= this.cellSizeX - 1; l++)
            {
                this.dots[l][k] = list[(k % 2 != 1) ? (l + k * this.cellSizeX) : (this.cellSizeX - 1 - l + k * this.cellSizeX)];
                this.dots[l][k].mPos = new Vector2((float)l, (float)k);
                this.dots[l][k].UpdatePos(0f, null, false);
            }
        }
        this.holdTouchTim = 0.2f;
        this.status = Game.PLAYSTATUS.RUN;
    }

    public void CheckEnd(bool isTip = false)
    {
        for (int i = 0; i < this.cellSizeX; i++)
        {
            for (int j = 0; j < this.cellSizeY; j++)
            {
                for (int k = 0; k < this.CheckArrow.GetLength(0); k++)
                {
                    int num = (int)this.dots[i][j].mPos.x + this.CheckArrow[k, 0];
                    int num2;
                    if ((int)this.dots[i][j].mPos.x % 2 == 0 || this.CheckArrow[k, 0] == 0)
                    {
                        num2 = (int)this.dots[i][j].mPos.y + this.CheckArrow[k, 1];
                    }
                    else
                    {
                        num2 = (int)this.dots[i][j].mPos.y + this.CheckArrow[k, 1] + 1;
                    }
                    if (num >= 0 && num < this.cellSizeX && num2 >= 0 && num2 < this.cellSizeY && this.dots[i][j].numberLevel == this.dots[num][num2].numberLevel)
                    {
                        return;
                    }
                }
            }
        }
        int num3 = 0;
        int num4 = 1;
        int num5 = 0;
        for (int l = 0; l < this.cellSizeY; l++)
        {
            while (num5 < this.cellSizeX && num5 >= 0)
            {
                this.dots[num5][l].PlayTipScale((float)num3 * 0.02f, false, 1.5f);
                num3++;
                num5 += num4;
            }
            num4 = -num4;
            num5 = ((num4 <= 0) ? (this.cellSizeX - 1) : 0);
        }
        base.Invoke("GameFinish", (float)num3 * 0.02f + 0.36f);
    }

    public void OnTouched(int index, TouchBase.TouchBaseType touchType, Vector3 touchPos)
    {
        if (this.holdTouchTim > 0f || this.status != Game.PLAYSTATUS.RUN)
        {
            return;
        }
        Vector2 zero = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(base.transform as RectTransform, touchPos, UIManager.selfInstance.canvas.worldCamera, out zero);
        Game.PLAYSTATUS playstatus = this.status;
        if (playstatus != Game.PLAYSTATUS.NONE)
        {
            if (playstatus != Game.PLAYSTATUS.RUN)
            {
                if (playstatus != Game.PLAYSTATUS.STOP)
                {
                }
            }
            else if (touchType == TouchBase.TouchBaseType.TouchBegan)
            {
                this.StartTouch(zero);
            }
            else if (touchType == TouchBase.TouchBaseType.TouchMove)
            {
                this.HoldTouch(zero);
            }
            else
            {
                this.EndTouch(zero);
            }
        }
    }

    private void Update()
    {

         
        float deltaTime = Time.deltaTime;
        float unscaledDeltaTime = Time.unscaledDeltaTime;
        if (this.holdTouchTim > 0f)
        {
            this.holdTouchTim -= deltaTime;
            if (this.holdTouchTim <= 0f)
            {
                this.holdTouchTim = 0f;
                if (!this.isGuide)
                {
                    this.CheckEnd(false);
                }
            }
        }
        this.touchBase.InvokeUpdate(deltaTime);
        if (this.onInvokeScaleUpdate != null)
        {
            this.onInvokeScaleUpdate(deltaTime);
        }
        if (this.onInvokeUnscaleUpdate != null)
        {
            this.onInvokeUnscaleUpdate(unscaledDeltaTime);
        }
        if (this.isGuide)
        {
            if (this.needGuideHand)
            {
                this.guideMvTime = ((this.guideMvTime < 1.6f) ? this.guideMvTime : 1.6f);
                float num = 1.6f / (float)this.guideDot.Count;
                int num2 = Mathf.CeilToInt(this.guideMvTime / num);
                num2 = ((num2 > 0) ? num2 : 1);
                if (num2 < this.guideDot.Count)
                {
                    Vector2 anchoredPosition = (this.guideDot[num2 - 1].transform as RectTransform).anchoredPosition;
                    Vector2 anchoredPosition2 = (this.guideDot[num2].transform as RectTransform).anchoredPosition;
                    UIManager.selfInstance.gamePanel.UpdateGuideHand(true, Vector2.Lerp(anchoredPosition, anchoredPosition2, 1f - ((float)num2 - this.guideMvTime / num)));
                }
                else
                {
                    this.guideMvTime = 0f;
                }
                this.guideMvTime += deltaTime * 1f;
            }
            else
            {
                UIManager.selfInstance.gamePanel.UpdateGuideHand(false, default(Vector2));
            }
        }
    }

    public static Game main;

    public Game.OnInvokeUpdate onInvokeUnscaleUpdate;

    public Game.OnInvokeUpdate onInvokeScaleUpdate;

    public Transform dotsParent;

    public Transform linesParent;

    public Vector2 offset;

    internal int cellSizeX;

    internal int cellSizeY;

    internal Game.PLAYSTATUS status;

    public TouchBase touchBase;

    private float holdTouchTim;

    private float touchTime;

    internal float cellWidth;

    public float cellHeight;

    internal Vector2 zeroOddPos;

    internal Vector2 zeroEvenPos;

    internal int nowMaxLv;

    private int nowLinkLv = -1;

    private int maxLinkLv = -1;

    internal Dot[][] dots;

    private List<Line> linkLines = new List<Line>();

    private Queue<Dot> cacheDots = new Queue<Dot>();

    private Queue<Line> cacheLines = new Queue<Line>();

    private List<Dot> guideDot = new List<Dot>();

    private float guideMvTime;

    private bool needGuideHand = true;

    internal bool isGuide = true;

    private Game.TIPSTATUS tipStatus;

    internal List<Game.SimpleDot> undoDotList;

    private Game.SimpleDot undoDotTarget;

    private long undoScore;

    private int[,] CheckArrow = new int[,]
    {
        {
            0,
            1
        },
        {
            1,
            0
        },
        {
            1,
            -1
        },
        {
            0,
            -1
        },
        {
            -1,
            -1
        },
        {
            -1,
            0
        }
    };

    public enum PLAYSTATUS
    {
        NONE,
        RUN,
        STOP
    }

    public delegate void OnInvokeUpdate(float timDelta);

    public enum TIPSTATUS
    {
        NONE,
        LIGHT,
        REMOVE
    }

    internal class SimpleDot
    {
        public SimpleDot(int newNumberLevel, Vector2 newPos)
        {
            this.numberLevel = newNumberLevel;
            this.mPos = newPos;
        }

        internal int numberLevel;

        internal Vector2 mPos;
    }
}