using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMgr : MonoBehaviour
{
    #region 单例模式
    private static MainMgr instance;
    public static MainMgr Instance { get => instance; }

    private void Awake()
    {
        instance = this;
    }
    #endregion
    //主面板
    public GamePanel gamePanel;

    //当前玩家所在的位置pos
    public int pos ;

    //一些全局变量
    public string speaker;  //当前说话人姓名
    public string dialogue; //谈话内容
    public string leftInfo; //左边内容
    public string rightInfo; //右边内容
    public static string RES = "Card/"; //卡牌的贴图路径保证为 “Resources/Card/” + pos 即可
    public int support, food, prestige, army, money, decay; //当前各项指标

    public int currleftJump, currRightJump;

    public string eventInfo; //当前事件内容

    public List<BuffInfo> buffs = new List<BuffInfo>();
    int[] buffTime = new int[4];
       
    private void Start()
    {
        pos = 1;
        support = food = prestige = army = money = 20;
        decay = 20;
        InitCurrCard();

    }

    #region Buff模块
    /// <summary>
    /// 给玩家添加一个buff：
    ///     1.获取到对应类型buff数据
    ///     2.将其加入buffs集合中
    ///     3.更新显示UI
    /// </summary>
    /// <param name="type"></param>
    public void AddBuff(int type)
    {
        BuffInfo buffInfo = DataMgr.Instance.buffInfos[type];
        
        bool isExist = false;
        //如果buffs中数值存在
        for (int i = 0;i < buffs.Count; i++)
        {
            if (buffs[i].id == buffInfo.id)
            {
                isExist = true;
                buffTime[i] = buffInfo.interval;
                break;
            }   
        }
        //如果不存在
        if (!isExist)
        {
            buffs.Add(buffInfo);
            buffTime[buffs.Count] = buffInfo.interval;
        }
        
        gamePanel.UpdateBuff();
    }

    //在卡牌初始化结算Buff
    public void BuffTrigger()
    {
        if (buffs.Count == 0) return;

        //遍历buff栏中所有buff
        for (int i = 0;i < buffs.Count; i++)
        {
            if (buffTime[i] > 0)
            {
                support += buffs[i].support;
                food += buffs[i].food;
                prestige += buffs[i].prestige;
                army += buffs[i].army;
                money += buffs[i].money;
                decay += buffs[i].decay;
                buffTime[i]--;
            }
            if (buffTime[i] == 0)
            {
                buffs.Remove(buffs[i]);
            }
        }
        foreach (var v in buffs) print(v);
        gamePanel.UpdateBuff();
        
    }
    #endregion

    /// <summary>
    /// 对当前卡牌进行初始化
    ///     1.对对应属性值进行增减
    ///     2.修改当前卡面人物与对话内容文本
    ///     3.在翻牌动画中间加入动画事件 切换贴图
    /// </summary>
    public void InitCurrCard()
    {
        CardInfo info = DataMgr.Instance.infos[pos]; //当前位置卡片数据
        print(info.leftJump + " " + info.rightJump);
        //更新数值
        speaker = info.name;
        dialogue = info.dialogue;
        leftInfo = info.leftInfo;
        rightInfo = info.rightInfo;
        support += info.support;
        food += info.food;
        prestige += info.prestige;
        army += info.army;
        money += info.money;
        decay += info.decay;
        currleftJump = info.leftJump;
        currRightJump = info.rightJump;
        //更新UI
        gamePanel.UpdateTxt();
        gamePanel.UpdateStatsAndBar();
    }
}

