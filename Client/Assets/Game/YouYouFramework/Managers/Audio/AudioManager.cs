using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using Object = UnityEngine.Object;

namespace YouYou
{
    /// <summary>
    /// 音频管理器
    /// </summary>
    public class AudioManager
    {
        public float PlayerBGMVolume { get; private set; }
        public float PlayerAudioVolume { get; private set; }


        public void Init()
        {
            AudioSourcePrefab = new GameObject("AudioItem", typeof(AudioSource), typeof(PoolObj)).GetComponent<AudioSource>();
            AudioSourcePrefab.transform.SetParent(GameEntry.Instance.AudioGroup);
            AudioSourcePrefab.playOnAwake = false;
            AudioSourcePrefab.maxDistance = 20;

            BGMSource = Object.Instantiate(AudioSourcePrefab, GameEntry.Instance.AudioGroup);
            BGMSource.loop = true;
            BGMSource.name = "BGMSource";

            GameEntry.Data.PlayerPrefsDataMgr.AddEventListener(PlayerPrefsDataMgr.EventName.PlayerBGMVolume, RefreshBGM);
            GameEntry.Data.PlayerPrefsDataMgr.AddEventListener(PlayerPrefsDataMgr.EventName.PlayerAudioVolume, RefreshAudio);
            GameEntry.Data.PlayerPrefsDataMgr.AddEventListener(PlayerPrefsDataMgr.EventName.GamePause, OnGamePause);

            RefreshBGM(null);
            RefreshAudio(null);
        }

        private void RefreshAudio(object userData)
        {
            PlayerAudioVolume = GameEntry.Data.PlayerPrefsDataMgr.GetFloat(PlayerPrefsDataMgr.EventName.PlayerAudioVolume);
        }
        private void RefreshBGM(object userData)
        {
            PlayerBGMVolume = GameEntry.Data.PlayerPrefsDataMgr.GetFloat(PlayerPrefsDataMgr.EventName.PlayerBGMVolume);
            if (CurrBGMEntity != null) BGMSource.volume = CurrBGMEntity.Volume * PlayerBGMVolume;
        }

        private void OnGamePause(object userData)
        {
            int GamePause = userData.ToInt();
            AudioSourceList.ForEach(x =>
            {
                if (x != null) x.mute = GamePause == 1;
            });
        }

        #region BGM
        public AudioSource BGMSource { get; private set; }
        public Sys_BGMEntity CurrBGMEntity;
        private TimeAction timeActionIn;
        private TimeAction timeActionOut;

        public void PlayBGM(BGMName audioName)
        {
            Sys_BGMEntity entity = GameEntry.DataTable.Sys_BGMDBModel.GetEntity(audioName.ToString());
            if (CurrBGMEntity != null && CurrBGMEntity.AssetPath == entity.AssetPath)
            {
                return;
            }

            CurrBGMEntity = entity;
            AudioClip audioClip = GameEntry.Resource.LoadMainAsset<AudioClip>(CurrBGMEntity.AssetPath);
            PlayBGM(audioClip, CurrBGMEntity.IsLoop == 1, CurrBGMEntity.IsFadeIn == 1, CurrBGMEntity.Volume);
            GameEntry.Log(LogCategory.Audio, "PlayBGM, Volume=={0}", CurrBGMEntity.Volume);
        }
        public void PlayBGM(AudioClip audioClip, bool isLoop, bool isFadeIn, float entityVolume)
        {
            if (audioClip == null)
            {
                Debug.LogError("audioClip==null");
                return;
            }
            StopBGM(() =>
            {
                BGMSource.clip = audioClip;
                BGMSource.Play();
                BGMSource.loop = isLoop;

                if (isFadeIn)
                {
                    //把音量逐渐变成Max
                    if (timeActionIn != null)
                    {
                        timeActionIn.Stop();
                        timeActionIn = null;
                    }
                    BGMSource.volume = 0;
                    timeActionIn = GameEntry.Time.Create(interval: 0.15f, loop: 10, unScaled: true, onUpdate: (int loop) =>
                    {
                        if (BGMSource == null) return;
                        float volume = entityVolume * PlayerBGMVolume;
                        BGMSource.volume = Mathf.Min(BGMSource.volume + volume / 10, volume);
                    }, onComplete: () =>
                    {
                        if (BGMSource == null) return;
                        timeActionIn = null;
                        BGMSource.volume = entityVolume * PlayerBGMVolume;
                    });
                }
                else
                {
                    BGMSource.volume = entityVolume * PlayerBGMVolume;
                }
            });
            GameEntry.Log(LogCategory.Audio, CurrBGMEntity.Volume + "PlayBGM");
        }

        internal void StopBGM(Action volumeOut = null)
        {
            if (CurrBGMEntity.IsFadeOut == 0)
            {
                BGMSource.Stop();
                volumeOut?.Invoke();
                return;
            }

            if (timeActionOut != null)
            {
                timeActionOut.Stop();
                timeActionOut = null;
            }
            //把音量逐渐变成0 再停止
            timeActionOut = GameEntry.Time.Create(interval: 0.15f, loop: 10, unScaled: true, onUpdate: (int loop) =>
            {
                if (BGMSource == null) return;
                float volume = CurrBGMEntity.Volume * PlayerBGMVolume;
                BGMSource.volume = Mathf.Max(BGMSource.volume - volume / 10, 0);
            }, onComplete: () =>
            {
                if (BGMSource == null) return;
                timeActionOut = null;
                BGMSource.Stop();
                volumeOut?.Invoke();
            });
            GameEntry.Log(LogCategory.Audio, CurrBGMEntity.Volume + "StopBGM");
        }

        public void PauseBGM(bool isPause)
        {
            if (BGMSource == null || BGMSource.clip == null) return;
            if (isPause)
            {
                BGMSource.Pause();
            }
            else
            {
                BGMSource.UnPause();
            }
            GameEntry.Log(LogCategory.Audio, CurrBGMEntity.Volume + "PauseBGM");

        }
        #endregion

        #region 音效
        private AudioSource AudioSourcePrefab;
        private List<AudioSource> AudioSourceList = new List<AudioSource>();

        public void PlayAudio(AudioClip audioClip, Vector3 point, float volume = 1, bool loop = false, int priority = 128)
        {
            AudioSource audioSource = PlayAudio2(audioClip, volume, loop, priority);
            if (audioSource == null)
            {
                Debug.LogError("audioSource==null");
                return;
            }
            audioSource.transform.position = point;
            audioSource.spatialBlend = 1;
        }
        public void PlayAudio(AudioClip audioClip, float volume = 1, bool loop = false, int priority = 128)
        {
            PlayAudio2(audioClip, volume, loop, priority);
        }
        public void PlayAudio(AudioName audioName, Vector3 point)
        {
            Sys_AudioEntity sys_Audio = GameEntry.DataTable.Sys_AudioDBModel.GetEntity(audioName.ToString());
            AudioClip audioClip = GameEntry.Resource.LoadMainAsset<AudioClip>(sys_Audio.AssetPath);
            AudioSource helper = PlayAudio2(audioClip, sys_Audio.Volume, sys_Audio.IsLoop == 1, sys_Audio.Priority);
            if (helper == null) return;
            helper.transform.position = point;
            helper.spatialBlend = 1;
        }
        public void PlayAudio(AudioName audioName)
        {
            Sys_AudioEntity sys_Audio = GameEntry.DataTable.Sys_AudioDBModel.GetEntity(audioName.ToString());
            AudioClip audioClip = GameEntry.Resource.LoadMainAsset<AudioClip>(sys_Audio.AssetPath);
            PlayAudio2(audioClip, sys_Audio.Volume, sys_Audio.IsLoop == 1, sys_Audio.Priority);
        }

        private AudioSource PlayAudio2(AudioClip audioClip, float volume = 1, bool loop = false, int priority = 128)
        {
            if (PlayerAudioVolume == 0f)
            {
                Debug.LogError("PlayerAudioVolume==0, 播放无效");
                return null;
            }

            AudioSource helper = GameEntry.Pool.GameObjectPool.Spawn(AudioSourcePrefab.transform, GameEntry.Instance.AudioGroup).GetComponent<AudioSource>();
            AudioSourceList.Add(helper);
            helper.clip = audioClip;
            helper.mute = false;
            helper.volume = volume * PlayerAudioVolume;
            helper.loop = loop;
            helper.priority = priority;
            helper.spatialBlend = 0;
            helper.Play();

            if (!helper.loop)
            {
                PoolObj poolObj = helper.GetComponent<PoolObj>();
                poolObj.SetDelayTimeDespawn(audioClip.length);
                poolObj.OnDespawn = () =>
                {
                    AudioSourceList.Remove(helper);
                };
            }
            return helper;
        }

        #endregion

    }
}