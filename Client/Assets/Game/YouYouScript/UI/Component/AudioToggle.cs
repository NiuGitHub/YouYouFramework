using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace YouYou
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Toggle))]//脚本依赖
    public class AudioToggle : MonoBehaviour
    {
        [SerializeField] private bool IsOffPlay;

        [SerializeField] private AudioName[] AudioNames = new AudioName[] { };
        private AudioName audioName;

        private Toggle m_Toggle;

        void Start()
        {
            if (AudioNames.Length == 0)
            {
                audioName = AudioName.UIClick;
            }
            else
            {
                audioName = AudioNames[Random.Range(0, AudioNames.Length)];
            }

            m_Toggle = GetComponent<Toggle>();
            m_Toggle.onValueChanged.AddListener((isOn) =>
            {
                if (IsOffPlay || isOn) GameEntry.Audio.PlayAudio(audioName);
            });
        }
    }
}