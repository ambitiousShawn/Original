using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrontCard : BaseCard
{
    protected override void Start()
    {
        base.Start();
    }

    //动画事件(在Born动画开始时初始化新的卡牌贴图以及数据)
    public void ChangeTextureAndInit()
    {
        MainMgr.Instance.BuffTrigger(); //Buff触发
        int currPos = MainMgr.Instance.pos;
        GetComponent<Image>().sprite = Resources.Load<Sprite>("Card/" + DataMgr.Instance.infos[currPos].name);
        if (GetComponent<Image>().sprite == null)
            GetComponent<Image>().sprite = Resources.Load<Sprite>("Card/空木签");
        AudioManager.Instance.PlaySound("木签");
        MainMgr.Instance.InitCurrCard(); //初始化当前卡牌
    }
}
