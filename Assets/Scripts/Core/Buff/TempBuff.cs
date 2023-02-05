using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempBuff : MonoBehaviour
{
    
    /// <summary>
    /// 动画事件
    /// 回到Buff卡槽逻辑：
    ///     1.将临时卡牌的parent改为frame
    /// </summary>
    public void BackFrame()
    {
        transform.parent = MainMgr.Instance.gamePanel.buffFrame;
        GetComponentInChildren<Text>().gameObject.SetActive(false);
        MainMgr.Instance.canMove = true;
    }
}
