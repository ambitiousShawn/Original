using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartPanel : MonoBehaviour
{
    public Button startGame; //开始游戏

    void Start()
    {
        startGame.onClick.AddListener(() =>
        {
            //切换场景
            ScenesManager.Instance.LoadSceneAsync("1_Game",()=>
            {

            });
        });
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();    
    }
}
