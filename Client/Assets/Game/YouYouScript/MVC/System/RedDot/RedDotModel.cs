using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YouYouFramework;

/// <summary>
/// 
/// </summary>
public class RedDotModel : Observable
{
    public enum EventName
    {
        //����ȡ�ص�
        E_SVR_MSG_ID_GET_GLOBAL_RED,
        //�������ص�
        E_SVR_MSG_ID_CLEAR_GLOBAL_RED,
    }

    /// <summary>
    /// ��̨�·��ĺ������
    /// </summary>
    private Dictionary<int, int> mMapGlobalRedInfo = new Dictionary<int, int>();

    public bool IsGlobalRedInfoExist()
    {
        return mMapGlobalRedInfo.Count != 0;
    }

    public int GetTGlobalRedInfo(int redDotType)
    {
        int TGlobalRedInfo = 0;
        if (mMapGlobalRedInfo.ContainsKey(redDotType))
        {
            TGlobalRedInfo = mMapGlobalRedInfo[redDotType];
        }
        return TGlobalRedInfo;
    }

    public void SetTGetGlobalRedRsp(TGetGlobalRedRsp rsp)
    {
        foreach (int key in rsp.mapRedModelResults.Keys)
        {
            mMapGlobalRedInfo[key] = rsp.mapRedModelResults[key];
            ReddotManager.Instance.ChangeValue(ReddotManager.Instance.GetServerIdOfPath(key), rsp.mapRedModelResults[key]);
        }
        Dispatch((int)EventName.E_SVR_MSG_ID_GET_GLOBAL_RED, rsp.mapRedModelResults);
    }

    public void SetTClearGlobalRedRsp(TClearGlobalRedRsp rsp)
    {
        for (int j = 0; j < rsp.vecRedModel.Count; j++)
        {
            int tGlobalRedInfo = rsp.vecRedModel[j];
            if (mMapGlobalRedInfo.ContainsKey(tGlobalRedInfo))
            {
                mMapGlobalRedInfo[tGlobalRedInfo] = 0;
                ReddotManager.Instance.ChangeValue(ReddotManager.Instance.GetServerIdOfPath(tGlobalRedInfo), 0);
            }
        }
        Dispatch((int)EventName.E_SVR_MSG_ID_CLEAR_GLOBAL_RED, rsp.vecRedModel);
    }
}
