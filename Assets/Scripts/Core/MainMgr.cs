using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuffType
{
    
}

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
    public int pos;

    //一些全局变量
    public string speaker;  //当前说话人姓名
    public string dialogue; //谈话内容
    public string leftInfo; //左边内容
    public string rightInfo; //右边内容
    public static string RES = "Card/"; //卡牌的贴图路径保证为 “Resources/Card/” + pos 即可
    public int support, food, prestige, army, money, decay; //当前各项指标

    public List<string> buffs = new List<string>();

    private void Start()
    {
        pos = 1;
        support = food = prestige = army = money = 50;
        decay = 40;
        InitCurrCard();
    }

    /// <summary>
    /// 对当前卡牌进行初始化
    ///     1.对对应属性值进行增减
    ///     2.修改当前卡面人物与对话内容文本
    ///     3.在翻牌动画中间加入动画事件 切换贴图
    /// </summary>
    public void InitCurrCard()
    {
        CardInfo info = DataMgr.Instance.infos[pos]; //当前位置卡片数据
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
        //更新UI
        gamePanel.UpdateTxt();
        gamePanel.UpdateStatsAndBar();
    }
}
