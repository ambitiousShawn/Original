using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataMgr : Singleton<DataMgr>
{
    //数组模拟的二叉树存储所有卡牌
    public List<CardInfo> infos = new List<CardInfo>();
    
    public DataMgr()
    {
        infos = JsonMgr.Instance.LoadData<List<CardInfo>>("CardInfo");
        
    }
}
