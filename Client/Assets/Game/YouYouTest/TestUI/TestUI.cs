using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YouYou;

public class TestUI : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.A))
        {
            //��ʽ1, ������ʼ��
            GameEntry.UI.OpenUIForm<DialogForm>();
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            //��ʽ2, ����ʼ��
            DialogForm.ShowForm("����ڲ�����ȫ���������, �Ѿ������¼����", "��¼����");
        }
    }
}