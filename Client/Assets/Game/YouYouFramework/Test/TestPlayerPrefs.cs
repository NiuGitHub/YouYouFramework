using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YouYou;

public class TestPlayerPrefs : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.A))
        {
            //���ܺ�, ��֧���¼�
            GameEntry.PlayerPrefs.SetFloat(PlayerPrefsDataMgr.EventName.MasterVolume, 1);
            GameEntry.PlayerPrefs.SetFloat(PlayerPrefsDataMgr.EventName.AudioVolume, 1);
            GameEntry.PlayerPrefs.SetFloat(PlayerPrefsDataMgr.EventName.BGMVolume, 1);
            GameEntry.PlayerPrefs.SetInt(PlayerPrefsDataMgr.EventName.FrameRate, 2);
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            //���ܲ�
            PlayerPrefs.SetFloat("TestKey", 0.5f);
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            //���ܺ�(ʹ��ԭ��)
            Dictionary<string, float> dic = new Dictionary<string, float>();
            dic["TestKey"] = 0.5f;
            PlayerPrefs.SetFloat("TestKey", dic["TestKey"]);
        }
    }
}
