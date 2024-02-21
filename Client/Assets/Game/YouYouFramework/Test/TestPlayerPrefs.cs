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
            //���ܺ�
            GameEntry.PlayerPrefs.SetFloat(PlayerPrefsConstKey.MasterVolume, 1);
            GameEntry.PlayerPrefs.SetFloat(PlayerPrefsConstKey.AudioVolume, 1);
            GameEntry.PlayerPrefs.SetFloat(PlayerPrefsConstKey.BGMVolume, 1);
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
