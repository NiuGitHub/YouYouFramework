using Sirenix.Serialization;
using Sirenix.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using YouYouMain;

public class VersionFileCtrl
{
    public static VersionFileCtrl Instance { get; private set; } = new VersionFileCtrl();


    /// <summary>
    /// ȥ��Դվ������CDN�İ汾�ļ���Ϣ
    /// </summary>
    public void SendCDNVersionFile(Action onInitComplete)
    {
        StringBuilder sbr = StringHelper.PoolNew();
        string url = sbr.AppendFormatNoGC("{0}{1}", SystemModel.Instance.CurrChannelConfig.RealSourceUrl, YFConstDefine.VersionFileName).ToString();
        StringHelper.PoolDel(ref sbr);

        IEnumerator UnityWebRequestGet(string url, Action<UnityWebRequest> onComplete)
        {
            using (UnityWebRequest request = UnityWebRequest.Get(url))
            {
                yield return request.SendWebRequest();
                onComplete?.Invoke(request);
            }
        }
        MainEntry.Instance.StartCoroutine(UnityWebRequestGet(url, (request) =>
        {
            //CDN�汾�ļ�����ɹ�
            if (request.result == UnityWebRequest.Result.Success)
            {
                //����CDN�汾�ļ���Ϣ
                LoadCDNVersionFile(request.downloadHandler.data);

                //���ؿ�д���汾�ļ���Ϣ
                LoadLocalVersionFile();

                onInitComplete?.Invoke();
            }
            else
            {
                MainEntry.Log("��ʼ��CDN��Դ����Ϣʧ�ܣ�url==" + url);
            }
        }));
    }
    private void LoadCDNVersionFile(byte[] buffer)
    {
        buffer = ZlibHelper.DeCompressBytes(buffer);

        Dictionary<string, VersionFileEntity> dic = new Dictionary<string, VersionFileEntity>();

        MMO_MemoryStream ms = new MMO_MemoryStream(buffer);

        int len = ms.ReadInt();

        for (int i = 0; i < len; i++)
        {
            if (i == 0)
            {
                VersionCDNModel.Instance.CDNVersion = ms.ReadUTF8String().Trim();
            }
            else
            {
                VersionFileEntity entity = new VersionFileEntity();
                entity.AssetBundleName = ms.ReadUTF8String();
                entity.MD5 = ms.ReadUTF8String();
                entity.Size = ms.ReadULong();
                entity.IsFirstData = ms.ReadByte() == 1;
                entity.IsEncrypt = ms.ReadByte() == 1;

                dic[entity.AssetBundleName] = entity;
            }
        }
        VersionCDNModel.Instance.VersionDic = dic;
        MainEntry.Log("OnInitCDNVersionFile");
    }
    private void LoadLocalVersionFile()
    {
        //�жϿ�д���汾�ļ��Ƿ����
        if (VersionLocalModel.Instance.GetVersionFileExists())
        {
            string json = IOUtil.GetFileText(VersionLocalModel.Instance.LocalVersionFilePath);
            VersionLocalModel.Instance.LocalAssetsVersionDic = json.ToObject<Dictionary<string, VersionFileEntity>>();
            VersionLocalModel.Instance.LocalAssetsVersion = PlayerPrefs.GetString(YFConstDefine.AssetVersion);
            MainEntry.Log("OnInitLocalVersionFile");
        }
    }
}
