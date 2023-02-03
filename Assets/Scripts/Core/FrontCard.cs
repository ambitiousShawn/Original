using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrontCard : MonoBehaviour
{
    //组件
    private Animator anim; //动画组件

    //鼠标位置
    private Vector3 mousePos;
    //上一帧鼠标位置
    private Vector3 preMousePos;

    void Start()
    {
        //组件初始化
        anim = GetComponent<Animator>();

        for (int i = 1; i < DataMgr.Instance.infos.Count; i++) Debug.Log(DataMgr.Instance.infos[i].id + DataMgr.Instance.infos[i].name);
    }

    void Update()
    {
        CardSwing();
        CardClick();
    }

    private void CardSwing()
    {
        mousePos = Input.mousePosition;
        //print(mousePos);
        if (mousePos.x <= 850) 
        {
            anim.SetBool("isLeft", true);
            MainMgr.Instance.gamePanel.ShowLorR(false);
        }
        
        else if (mousePos.x >= 1070)
        {
            anim.SetBool("isRight", true);
            MainMgr.Instance.gamePanel.ShowLorR(true);
        } 
        else
        {
            MainMgr.Instance.gamePanel.HideLandR();
            if (preMousePos.x <= 850) anim.SetBool("isLeft", false);
            else if (preMousePos.x >= 1070) anim.SetBool("isRight", false);
        }

        preMousePos = mousePos;
    }

    private void CardClick()
    {
        //按键后，选择选项并播放落下和出生动画
        if ((anim.GetBool("isLeft") || anim.GetBool("isRight")) &&
            Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("Fall");
            //决策树遍历
            if (anim.GetBool("isLeft")) MainMgr.Instance.pos *= 2;
            else MainMgr.Instance.pos = MainMgr.Instance.pos * 2 + 1;
            print("当前位置编号为" + MainMgr.Instance.pos);
            MainMgr.Instance.InitCurrCard(); //初始化当前卡牌
        }
    }

    //动画事件
    public void ChangeTexture()
    {
        GetComponent<Image>().sprite = Resources.Load<Sprite>("Card/" + MainMgr.Instance.pos);
    }
}
