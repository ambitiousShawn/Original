using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataMgr : Singleton<DataMgr>
{
    //数组模拟的二叉树存储所有卡牌
    public List<CardInfo> infos = new List<CardInfo>();
    //Buff数据
    public List<BuffInfo> buffInfos = new List<BuffInfo>();
    //CG数据
    public List<CGInfo> cgInfos = new List<CGInfo>();
    
    public DataMgr()
    {
        infos = JsonMgr.Instance.LoadData<List<CardInfo>>("CardInfo");
        buffInfos = JsonMgr.Instance.LoadData<List<BuffInfo>>("BuffInfo");
        cgInfos = JsonMgr.Instance.LoadData<List<CGInfo>>("CGInfo");
    }
}
