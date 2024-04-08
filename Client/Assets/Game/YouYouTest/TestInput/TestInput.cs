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
            GameEntry.Input.SetButtonUp(InputKeyCode.BuyTower);
        });
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.A))
        {
            //PC��, ���̰���A, ����Input
            GameEntry.Input.SetButtonUp(InputKeyCode.BuyTower);
        }

        if (GameEntry.Input.GetButtonUp(InputKeyCode.BuyTower))
        {
            //����Input����, ��ӡ��־
            GameEntry.Log(LogCategory.Normal, InputKeyCode.BuyTower.ToString());
        }
    }
}
