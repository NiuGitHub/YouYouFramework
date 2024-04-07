using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YouYouFramework;

public class TestInput : MonoBehaviour
{
    [SerializeField] Button button;

    private void Awake()
    {
        button.onClick.AddListener(() =>
        {
            //�ֻ���, �����ť����Input
            GameEntry.Input.SetButtonUp(InputKey.BuyTower);
        });
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.A))
        {
            //PC��, ���̰���A, ����Input
            GameEntry.Input.SetButtonUp(InputKey.BuyTower);
        }

        if (GameEntry.Input.GetButtonUp(InputKey.BuyTower))
        {
            //����Input����, ��ӡ��־
            GameEntry.Log(LogCategory.Normal, InputKey.BuyTower.ToString());
        }
    }
}
