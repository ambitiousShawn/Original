using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CG : MonoBehaviour
{
    //动画事件 :当cgmask完全被盖住时，修改贴图，修改文字
    public void UpdateCG()
    {
        MainMgr.Instance.gamePanel.UpdateCGInfo();
    }
}
