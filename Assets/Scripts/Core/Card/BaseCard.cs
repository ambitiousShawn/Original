using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCard : MonoBehaviour
{
    //组件
    protected Animator anim; //动画组件

    //鼠标位置
    private Vector3 mousePos;
    //上一帧鼠标位置
    private Vector3 preMousePos;

    protected virtual void Start()
    {
        //组件初始化
        anim = GetComponent<Animator>();
        MainMgr.Instance.AddBuff(1);
        MainMgr.Instance.AddBuff(1);
    }

    protected virtual void Update()
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
            //if (MainMgr.Instance.pos == 22) print(MainMgr.Instance.currleftJump + " " + MainMgr.Instance.currRightJump);
            //决策树遍历
            if (anim.GetBool("isLeft")) MainMgr.Instance.pos = MainMgr.Instance.currleftJump;
            else MainMgr.Instance.pos = MainMgr.Instance.pos = MainMgr.Instance.currRightJump ;
            print(MainMgr.Instance.pos);

        }
    }
}
