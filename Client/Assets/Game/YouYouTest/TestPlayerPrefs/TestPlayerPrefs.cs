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
            //���ܺ�, ���뱾�ش浵
            GameEntry.PlayerPrefs.SetFloat(PlayerPrefsConstKey.MasterVolume, 1);
            GameEntry.PlayerPrefs.SetFloat(PlayerPrefsConstKey.AudioVolume, 1);
            GameEntry.PlayerPrefs.SetFloat(PlayerPrefsConstKey.BGMVolume, 1);
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            //��ȡ���ش浵
            GameEntry.Log(LogCategory.ZhangSan, GameEntry.PlayerPrefs.GetFloat(PlayerPrefsConstKey.MasterVolume));
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            //���ܲ�
            PlayerPrefs.SetFloat("TestKey", 0.5f);
        }
    }
}
