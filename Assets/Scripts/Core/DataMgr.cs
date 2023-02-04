using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataMgr : Singleton<DataMgr>
{
    //数组模拟的二叉树存储所有卡牌
    public List<CardInfo> infos = new List<CardInfo>();
    //突发事件数据
    public List<EventsInfo> eventsInfos = new List<EventsInfo>();
    //Buff数据
    public List<BuffInfo> buffInfos = new List<BuffInfo>();
    
    public DataMgr()
    {
        infos = JsonMgr.Instance.LoadData<List<CardInfo>>("CardInfo");
        eventsInfos = JsonMgr.Instance.LoadData<List<EventsInfo>>("EventsInfo");
        buffInfos = JsonMgr.Instance.LoadData<List<BuffInfo>>("BuffInfo");
    }
}
