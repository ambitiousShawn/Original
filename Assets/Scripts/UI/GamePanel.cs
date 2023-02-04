using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : MonoBehaviour
{
    public Text dialogueTxt; //对话文本
    public Text leftInfoTxt; //左边对话文本
    public Text rightInfoTxt; //右边对话文本
    public Text midInfoTxt; //中间对话文本
    public Image TxtMask; //对话遮罩
    
    //Stats
    public Image supportMask; //民心滑动条
    public Image foodMask; //粮草滑动条
    public Image prestigeMask; //威望滑动条
    public Image armyMask; //军队滑动条
    public Image moneyMask; //经济滑动条
    public Image decayMask; //腐朽度滑动条

    public Text supportTxt;
    public Text foodTxt;
    public Text prestigeTxt;
    public Text armyTxt;
    public Text moneyTxt;
    public Text decayTxt;

    //Buff栏
    public Image buff1;
    public Image buff2;
    public Image buff3;
    public Image buff4;

    //渐变是否完成
    bool[] isFinish = new bool[6];

    //显示对话框和某侧文字(true为右侧)
    public void ShowLorR(bool isRight)
    {
        TxtMask.gameObject.SetActive(true);
        /*if (isRight) rightInfoTxt.gameObject.SetActive(true);
        else leftInfoTxt.gameObject.SetActive(true);*/
        midInfoTxt.gameObject.SetActive(true);
        if (isRight) midInfoTxt.text = rightInfoTxt.text;
        else midInfoTxt.text = leftInfoTxt.text;
        if (midInfoTxt.text == "") TxtMask.gameObject.SetActive(false);
    }

    //隐藏对话框和两侧文字
    public void HideLandR()
    {
        TxtMask.gameObject.SetActive(false);
        /*leftInfoTxt.gameObject.SetActive(false);
        rightInfoTxt.gameObject.SetActive(false);*/
        midInfoTxt.gameObject.SetActive(false);
    }

    //更新人物对话
    public void UpdateTxt()
    {
        dialogueTxt.text = MainMgr.Instance.dialogue;
        leftInfoTxt.text = MainMgr.Instance.leftInfo;
        rightInfoTxt.text = MainMgr.Instance.rightInfo;
    }

    //更新上方Buff栏
    public void UpdateBuff()
    {
        buff1.sprite = null;
        string res = "Buff/";
        for (int i = 1;i <= MainMgr.Instance.buffs.Count; i++)
        {
            switch (i) 
            {
                case 1: buff1.sprite = Resources.Load<Sprite>(res + i); break;
                case 2: buff2.sprite = Resources.Load<Sprite>(res + i); break;
                case 3: buff3.sprite = Resources.Load<Sprite>(res + i); break;
                case 4: buff4.sprite = Resources.Load<Sprite>(res + i); break;
            }
            
        }
        for (int j = MainMgr.Instance.buffs.Count; j <= 4; j++)
        {
            if (j <= 3) buff4.sprite = null;
            if (j <= 2) buff3.sprite = null;
            if (j <= 1) buff2.sprite = null;
            if (j <= 0) buff1.sprite = null;
        }
    }


    //更新左侧数据栏
    public void UpdateStatsAndBar()
    {
        UpdateStats();
        UpdateBar();
    }


    //更新各项数据数值
    private void UpdateStats()
    {
        supportTxt.text = MainMgr.Instance.support.ToString() ;
        foodTxt.text = MainMgr.Instance.food.ToString();
        prestigeTxt.text = MainMgr.Instance.prestige.ToString();
        armyTxt.text = MainMgr.Instance.army.ToString();
        moneyTxt.text = MainMgr.Instance.money.ToString();
        decayTxt.text = MainMgr.Instance.decay.ToString();
    }

    //更新各项指标进度条
    private void UpdateBar()
    {
        StartCoroutine(IE_UpdateData());
    }

    IEnumerator IE_UpdateData()
    {
        float preSupport = supportMask.fillAmount * 100;
        float preFood = foodMask.fillAmount * 100;
        float prePrestige = prestigeMask.fillAmount * 100;
        float preArmy = armyMask.fillAmount * 100;
        float preMoney = moneyMask.fillAmount * 100;
        float preDecay = decayMask.fillAmount * 100;
        float currSupport = MainMgr.Instance.support;
        float currFood = MainMgr.Instance.food;
        float currPrestige = MainMgr.Instance.prestige;
        float currArmy = MainMgr.Instance.army;
        float currMoney = MainMgr.Instance.money;
        float currDecay = MainMgr.Instance.decay;


        for (int i = 0; i < isFinish.Length; i++) isFinish[i] = false;

        //渐变值
        float deltaVal = 0.2f;

        while (!IsFinish())
        {
            StatsBarChange(0, supportMask, ref preSupport, currSupport,deltaVal);
            StatsBarChange(1, foodMask, ref preFood, currFood, deltaVal);
            StatsBarChange(2, prestigeMask, ref prePrestige, currPrestige, deltaVal);
            StatsBarChange(3, armyMask, ref preArmy, currArmy, deltaVal);
            StatsBarChange(4, moneyMask, ref preMoney, currMoney, deltaVal);
            StatsBarChange(5, decayMask, ref preDecay, currDecay, deltaVal);

            yield return new WaitForSeconds(0.01f);
        }
    }

    private bool IsFinish()
    {
        foreach (bool v in isFinish)
            if (!v) return false;
        return true;
    }

    /// <summary>
    /// 每一次更新进度条
    /// </summary>
    /// <param name="bar">改变哪个进度条</param>
    /// <param name="initVal">进度条当前值</param>
    /// <param name="endVal">进度条目标值</param>
    private void StatsBarChange(int i ,Image bar, ref float initVal, float endVal,float deltaVal)
    {
        bar.fillAmount = initVal / 100;
        if (initVal  < endVal - 1)
        {
            initVal += deltaVal;
            bar.color = new Color(0,1,1);
        }else if (initVal  > endVal + 1)
        {
            initVal -= deltaVal;
            bar.color = new Color(0.35f,0.47f,0.47f);
        }
        else
        {
            bar.color = new Color(0.32f,0.72f,0.71f);
            isFinish[i] = true;
        }
    }
}
