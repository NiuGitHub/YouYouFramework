using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YouYou;
using static QualityCtrl;

public class QualityModel
{
    public enum Quality
    {
        Low,
        Medium,
        High
    }
    public enum FrameRate
    {
        Low,
        Medium,
        High,
    }
    public enum ScreenLevel
    {
        Low,
        Medium,
        High
    }

    /// <summary>
    /// ����Ʒ��
    /// </summary>
    public Quality CurrQuality { get; private set; }
    /// <summary>
    /// �ֱ���
    /// </summary>
    public ScreenLevel CurrScreen { get; private set; }
    /// <summary>
    /// ������Ϸ֡��,FPS
    /// </summary>
    public FrameRate CurrFrameRate { get; private set; }


    public void SetQuality(Quality quality)
    {
        CurrQuality = quality;
        QualitySettings.SetQualityLevel(CurrQuality.ToInt(), true);
        GameEntry.PlayerPrefs.SetInt(PlayerPrefsDataMgr.EventName.QualityLevel, (int)quality);
    }
    public void SetScreen(ScreenLevel value)
    {
        CurrScreen = value;
        GameEntry.PlayerPrefs.SetInt(PlayerPrefsDataMgr.EventName.Screen, (int)CurrScreen);
        RefreshScreen();
    }

    public void RefreshScreen()
    {
        //�ֱ���
        bool isGame = false;
        int screen = 0;
        switch (CurrScreen)
        {
            case ScreenLevel.Low:
                screen = isGame ? 540 : 640;
                break;
            case ScreenLevel.Medium:
                screen = isGame ? 640 : 720;
                break;
            case ScreenLevel.High:
                screen = isGame ? 720 : 1080;
                break;
        }
        Screen.SetResolution((int)(screen * Screen.width / (float)Screen.height), screen, Screen.fullScreen);
    }

    public void SetFrameRate(FrameRate frameRate)
    {
        CurrFrameRate = frameRate;
        switch (CurrFrameRate)
        {
            case FrameRate.Low:
                Application.targetFrameRate = 30; //60;
                break;
            case FrameRate.Medium:
                Application.targetFrameRate = 40; //60;
                break;
            case FrameRate.High:
                Application.targetFrameRate = 60; // 120;
                break;
        }
        GameEntry.PlayerPrefs.SetInt(PlayerPrefsDataMgr.EventName.FrameRate, (int)CurrFrameRate);
        //#if UNITY_EDITOR
        //            Application.targetFrameRate = -1;
        //#endif
    }
}
