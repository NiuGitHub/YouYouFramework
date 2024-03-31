using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YouYouFramework;


public class TestAudio : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.A))
        {
            //���ű�������
            GameEntry.Audio.PlayBGM(BGMName.maintheme1);
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            //����UI��Ч
            GameEntry.Audio.PlayAudio(AudioName.button_sound);
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            //����3D��Ч
            GameEntry.Audio.PlayAudio(AudioName.button_sound, transform.position);
        }
    }
}