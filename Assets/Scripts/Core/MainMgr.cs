using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public List<BuffInfo> buffs = new List<BuffInfo>();
    int[] buffTime = new int[4];

    //禁止玩家操作
    public bool canMove;

    //结局CG数据
    public int cgId;
    public string cgDia;

    private void Start()
    {
        AudioManager.Instance.PlayBGM("BGM1");
        pos = 1;
        support = food = prestige = army = money = 30;
        decay = 20;
        canMove = true;
        InitCurrCard();

    }

    /// <summary>
    /// 游戏结束条件以及特殊事件触发方式
    /// 1.游戏结束条件：
    ///     ① 威望低于20
    ///     ② 任意值低于0（除腐败）
    /// 2.特殊事件
    ///     ① pos = 35 || 36，龙Buff
    ///     ② pos = 52 || 53，干旱Buff
    ///     ③ pos = 86 ，战乱Buff
    ///     ④ pos = 103 ， 风调雨顺Buff
    ///     ⑤ pos = 126 , 战乱Buff
    ///     ⑥ 腐朽 > 50 ，腐朽Buff
    /// </summary>
    private void Update()
    {
        GameOver();
        GeneratorBuff();

        InputKey();
        PlaySoundOnGame();
    }

    //播放音效
    private void PlaySoundOnGame()
    {
            if (pos == 136) AudioManager.Instance.PlaySound("BGM3");
    }

    // 输入按键
    private void InputKey()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ScenesManager.Instance.LoadSceneAsync("0_Start", () => { });
        }
    }

    //结束条件
    private void GameOver()
    {
        if (support <= 0)
        {
            CGInfo info = DataMgr.Instance.cgInfos[1];
            cgId = info.id;
            cgDia = info.dialogue;
            gamePanel.cgmask.SetActive(true);
        }
        if (food <= 0)
        {
            CGInfo info = DataMgr.Instance.cgInfos[2];
            cgId = info.id;
            cgDia = info.dialogue;
            gamePanel.cgmask.SetActive(true);
        }
        if (prestige < 20)
        {
            CGInfo info = DataMgr.Instance.cgInfos[3];
            cgId = info.id;
            cgDia = info.dialogue;
            gamePanel.cgmask.SetActive(true);
        }
        if (army <= 0)
        {
            CGInfo info = DataMgr.Instance.cgInfos[4];
            cgId = info.id;
            cgDia = info.dialogue;
            gamePanel.cgmask.SetActive(true);
        }
        if (money <= 0)
        {
            CGInfo info = DataMgr.Instance.cgInfos[5];
            cgId = info.id;
            cgDia = info.dialogue;
            gamePanel.cgmask.SetActive(true);
        }
    }

    /// <summary>
    /// 生成Buff：
    ///  1.将贴图赋值给TempBuff
    ///  2.播放生成动画
    ///  3.在生成动画结尾调用动画事件
    /// </summary>
    private void GeneratorBuff()
    {
        if (pos == 37) AddBuff(1); //龙

        else if (pos == 52 || pos == 53) AddBuff(2); //干旱
        else if (pos == 86 || pos == 124 || pos == 123) AddBuff(4); //战乱
        else if (pos == 103) AddBuff(3); // 风调雨顺

        if (decay >= 50) AddBuff(5); //腐朽
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
        BuffInfo buffInfo = DataMgr.Instance.buffInfos[type]; //读取指定类型buff的数据
        
        bool isExist = false;
        //如果buffs中数值存在
        for (int i = 0;i < buffs.Count; i++)
        {
            if (buffs[i].id == buffInfo.id)
            {
                isExist = true;
                buffTime[i] = buffInfo.times;
                break;
            }   
        }
        //如果不存在
        if (!isExist)
        {
            buffs.Add(buffInfo);
            buffTime[buffs.Count] = buffInfo.times;
            
            //激活临时卡牌，并修改信息
            canMove = false;
            GameObject tempBuff = ResourcesManager.Instance.Load<GameObject>("Buff/TempBuff");
            tempBuff.transform.SetParent(gamePanel.transform);
            tempBuff.GetComponent<Image>().sprite = Resources.Load<Sprite>("Buff/" + buffInfo.id);
            tempBuff.GetComponentInChildren<Text>().text = buffInfo.name;
            tempBuff.GetComponent<Animator>().Play("Gen");
            tempBuff.name = "tempBuff" + buffs.Count;
        }
        
        //gamePanel.UpdateBuff1();
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
                if (support > 100) support = 100;
                food += buffs[i].food;
                if (food > 100) food = 100;
                prestige += buffs[i].prestige;
                if (prestige > 100) prestige = 100;
                army += buffs[i].army;
                if (army > 100) army= 100;
                money += buffs[i].money;
                if (money> 100) money= 100;
                decay += buffs[i].decay;
                if (decay> 100) decay= 100;
                buffTime[i]--;
            }
            if (buffTime[i] == 0)
            {
                buffs.Remove(buffs[i]);
                Destroy(gamePanel.buffFrame.Find("tempBuff" + (i + 1)).gameObject);
            }
        }
        //gamePanel.UpdateBuff1();
       
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
        if (support > 100) support = 100;
        food += info.food;
        if (food > 100) food = 100;
        prestige += info.prestige;
        if (prestige > 100) prestige = 100;
        army += info.army;
        if (army > 100) army = 100;
        money += info.money;
        if (money > 100) money = 100;
        decay += info.decay;
        if (decay > 100) decay = 100;
        currleftJump = info.leftJump;
        currRightJump = info.rightJump;
        //更新UI
        gamePanel.UpdateTxt();
        gamePanel.UpdateStatsAndBar();
    }
}

