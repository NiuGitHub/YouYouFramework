using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YouYou;

public class TestEvent : MonoBehaviour
{
    private void OnDestroy()
    {
        //�Ƴ�����ȫ���¼�
        GameEntry.Event.RemoveEventListener(CommonEventId.TestEvent, OnTestEvent);

        //�Ƴ�����ĳ��Model�ڵ�ĳ������ˢ�µ��¼�
        GameEntry.Model.GetModel<TestModel>().RemoveEventListener((int)TestModel.TestEvent.TestEvent1, OnTestEvent);
    }
    void Start()
    {
        //����ȫ���¼�
        GameEntry.Event.AddEventListener(CommonEventId.TestEvent, OnTestEvent);

        //����ĳ��Model�ڵ�ĳ������ˢ�µ��¼�
        GameEntry.Model.GetModel<TestModel>().AddEventListener((int)TestModel.TestEvent.TestEvent1, OnTestEvent);
    }
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.A))
        {
            //����ȫ���¼�
            GameEntry.Event.Dispatch(CommonEventId.TestEvent, 123);
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            //����ĳ��Model�ڵ�ĳ������ˢ�µ��¼�
            GameEntry.Model.GetModel<TestModel>().Dispatch((int)TestModel.TestEvent.TestEvent1);
        }
    }

    private void OnTestEvent(object userData)
    {
        Debug.Log(userData);
    }
}