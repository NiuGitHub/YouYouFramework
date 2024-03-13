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
        GameEntry.Model.GetModel<GuideModel>().RemoveEventListener((int)GuideModel.GUIDE_ID.EventName, OnTestEvent);
    }
    void Start()
    {
        //����ȫ���¼�
        GameEntry.Event.AddEventListener(CommonEventId.TestEvent, OnTestEvent);

        //����ĳ��Model�ڵ�ĳ������ˢ�µ��¼�
        GameEntry.Model.GetModel<GuideModel>().AddEventListener((int)GuideModel.GUIDE_ID.EventName, OnTestEvent);
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
            GameEntry.Model.GetModel<GuideModel>().Dispatch((int)GuideModel.GUIDE_ID.EventName);
        }
    }

    private void OnTestEvent(object userData)
    {
        Debug.Log(userData);
    }
}