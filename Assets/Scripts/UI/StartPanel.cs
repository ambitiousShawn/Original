using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartPanel : MonoBehaviour
{
    public Button startGame; //开始游戏

    public Image tips; //提示面板

    void Start()
    {
        startGame.onClick.AddListener(() =>
        {
            //切换场景
            ScenesManager.Instance.LoadSceneAsync("1_Game",()=>
            {

            });
        });

        Invoke("DelayHide", 3f);
    }

    private void DelayHide()
    {
        tips.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();    
    }
}
