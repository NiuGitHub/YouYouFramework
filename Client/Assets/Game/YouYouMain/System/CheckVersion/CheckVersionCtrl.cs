using Sirenix.Serialization;
using Sirenix.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using YouYouMain;

public class CheckVersionCtrl
{
    public static CheckVersionCtrl Instance { get; private set; } = new CheckVersionCtrl();

    public CheckVersionCtrl()
    {
        m_NeedDownloadList = new LinkedList<string>();
    }

    #region ȥ��Դվ������CDN�İ汾�ļ���Ϣ
    /// <summary>
    /// ȥ��Դվ������CDN�İ汾�ļ���Ϣ
    /// </summary>
    public void SendCDNVersionFile(Action onInitComplete)
    {
        StringBuilder sbr = StringHelper.PoolNew();
        string url = sbr.AppendFormatNoGC("{0}{1}", ChannelModel.Instance.CurrChannelConfig.RealSourceUrl, YFConstDefine.VersionFileName).ToString();
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
                VersionCDNModel.Instance.Version = ms.ReadUTF8String().Trim();
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
            string json = IOUtil.GetFileText(VersionLocalModel.Instance.VersionFilePath);
            VersionLocalModel.Instance.VersionDic = json.ToObject<Dictionary<string, VersionFileEntity>>();
            VersionLocalModel.Instance.AssetsVersion = PlayerPrefs.GetString(YFConstDefine.AssetVersion);
            MainEntry.Log("OnInitLocalVersionFile");
        }
    }
    #endregion

    #region ����������߼�
    /// <summary>
    /// ��Ҫ���ص���Դ���б�
    /// </summary>
    private LinkedList<string> m_NeedDownloadList;

    /// <summary>
    /// ���汾��������ʱ��Ĳ���
    /// </summary>
    private BaseParams m_DownloadingParams;

    public event Action CheckVersionBeginDownload;
    internal event Action<BaseParams> CheckVersionDownloadUpdate;
    public event Action CheckVersionDownloadComplete;

    private Action CheckVersionComplete;

    /// <summary>
    /// ������
    /// </summary>
    public void CheckVersionChange(Action onComplete)
    {
        CheckVersionComplete = onComplete;

        //ȥ��Դվ������CDN�İ汾�ļ���Ϣ
        SendCDNVersionFile(CheckVersionChange);
    }
    private void CheckVersionChange()
    {
        MainEntry.Log("������=>CheckVersionChange(), �汾��=>{0}", VersionLocalModel.Instance.AssetsVersion);

        if (VersionLocalModel.Instance.GetVersionFileExists())
        {
            if (!string.IsNullOrEmpty(VersionLocalModel.Instance.AssetsVersion) && VersionLocalModel.Instance.AssetsVersion.Equals(VersionCDNModel.Instance.Version))
            {
                MainEntry.Log("��д���汾�ź�CDN�汾��һ�� ����Ҫ������");
                CheckVersionComplete?.Invoke();
            }
            else
            {
                MainEntry.Log("��д���汾�ź�CDN�汾�Ų�һ�� ��ʼ������");
                BeginCheckVersionChange();
            }
        }
        else
        {
            //���س�ʼ��Դ
            DownloadInitResources();
        }
    }

    /// <summary>
    /// ���س�ʼ��Դ
    /// </summary>
    private void DownloadInitResources()
    {
        CheckVersionBeginDownload?.Invoke();
        m_DownloadingParams = BaseParams.Create();

        m_NeedDownloadList.Clear();

        var enumerator = VersionCDNModel.Instance.VersionDic.GetEnumerator();
        while (enumerator.MoveNext())
        {
            VersionFileEntity entity = enumerator.Current.Value;
            if (entity.IsFirstData)
            {
                m_NeedDownloadList.AddLast(entity.AssetBundleName);
            }
        }

        //���û�г�ʼ��Դ ֱ�Ӽ�����
        if (m_NeedDownloadList.Count == 0)
        {
            BeginCheckVersionChange();
        }
        else
        {
            MainEntry.Log("���س�ʼ��Դ,�ļ�����==>>" + m_NeedDownloadList.Count);
            MainEntry.Download.BeginDownloadMulit(m_NeedDownloadList, OnDownloadMulitUpdate, OnDownloadMulitComplete);
        }
    }

    /// <summary>
    /// ��ʼ������
    /// </summary>
    private void BeginCheckVersionChange()
    {
        m_DownloadingParams = BaseParams.Create();

        //��Ҫɾ�����ļ�
        LinkedList<string> deleteList = new LinkedList<string>();

        //��Ҫ���ص��ļ�
        LinkedList<string> needDownloadList = new LinkedList<string>();

        //�ҳ���Ҫɾ�����ļ�
        var enumerator = VersionLocalModel.Instance.VersionDic.GetEnumerator();
        while (enumerator.MoveNext())
        {
            string assetBundleName = enumerator.Current.Key;

            VersionFileEntity cdnAssetBundleInfo = null;
            if (VersionCDNModel.Instance.VersionDic.TryGetValue(assetBundleName, out cdnAssetBundleInfo))
            {
                //��д���� CDNҲ��
                if (!cdnAssetBundleInfo.MD5.Equals(enumerator.Current.Value.MD5, StringComparison.CurrentCultureIgnoreCase))
                {
                    //���MD5��һ�� ������������
                    needDownloadList.AddLast(assetBundleName);
                }
            }
            else
            {
                //��д���� CDN��û�� ����ɾ������
                deleteList.AddLast(assetBundleName);
            }
        }

        //ɾ����Ҫɾ����
        MainEntry.Log("ɾ������Դ=>{0}", deleteList.ToJson());
        LinkedListNode<string> currDel = deleteList.First;
        while (currDel != null)
        {
            StringBuilder sbr = StringHelper.PoolNew();
            string filePath = sbr.AppendFormatNoGC("{0}/{1}", Application.persistentDataPath, currDel.Value).ToString();
            StringHelper.PoolDel(ref sbr);

            if (File.Exists(filePath)) File.Delete(filePath);
            LinkedListNode<string> next = currDel.Next;
            deleteList.Remove(currDel);
            currDel = next;
        }

        //�����Ҫ���ص�
        enumerator = VersionCDNModel.Instance.VersionDic.GetEnumerator();
        while (enumerator.MoveNext())
        {
            VersionFileEntity cdnAssetBundleInfo = enumerator.Current.Value;
            if (cdnAssetBundleInfo.IsFirstData)//����ʼ��Դ
            {
                if (!VersionLocalModel.Instance.VersionDic.ContainsKey(cdnAssetBundleInfo.AssetBundleName))
                {
                    //�����д��û�� ������������
                    needDownloadList.AddLast(cdnAssetBundleInfo.AssetBundleName);
                }
            }
        }

        CheckVersionBeginDownload?.Invoke();

        //��������
        MainEntry.Log("���ظ�����Դ,�ļ�����==>" + needDownloadList.Count + "==>" + needDownloadList.ToJson());
        MainEntry.Download.BeginDownloadMulit(needDownloadList, OnDownloadMulitUpdate, OnDownloadMulitComplete);
    }
    /// <summary>
    /// ���ؽ�����
    /// </summary>
    private void OnDownloadMulitUpdate(int t1, int t2, ulong t3, ulong t4)
    {
        m_DownloadingParams.IntParam1 = t1;
        m_DownloadingParams.IntParam2 = t2;

        m_DownloadingParams.ULongParam1 = t3;
        m_DownloadingParams.ULongParam2 = t4;

        CheckVersionDownloadUpdate?.Invoke(m_DownloadingParams);
    }
    /// <summary>
    /// �������
    /// </summary>
    private void OnDownloadMulitComplete()
    {
        VersionLocalModel.Instance.SetAssetVersion(VersionCDNModel.Instance.Version);

        CheckVersionDownloadComplete?.Invoke();
        //MainEntry.ClassObjectPool.Enqueue(m_DownloadingParams);

        MainEntry.Log("�������������, ����Ԥ��������");
        CheckVersionComplete?.Invoke();
    }
    #endregion

}
