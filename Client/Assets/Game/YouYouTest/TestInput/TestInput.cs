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
            GameEntry.Input.SetButtonUp(InputName.BuyTower);
        });
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.A))
        {
            //PC��, ���̰���A, ����Input
            GameEntry.Input.SetButtonUp(InputName.BuyTower);
        }

        if (GameEntry.Input.GetButtonUp(InputName.BuyTower))
        {
            //����Input����, ��ӡ��־
            GameEntry.Log(LogCategory.Normal, InputName.BuyTower.ToString());
        }
    }
}
