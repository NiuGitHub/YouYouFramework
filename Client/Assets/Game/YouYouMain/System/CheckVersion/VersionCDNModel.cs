using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YouYouMain;


/// <summary>
/// AssetBundle�汾�ļ�, �ƶ���Դվ���VersionFile.bytes��Model
/// </summary>
public class VersionCDNModel
{
    public static VersionCDNModel Instance { get; private set; } = new VersionCDNModel();

    /// <summary>
    /// CDN��Դ�汾��
    /// </summary>
    public string Version;

    /// <summary>
    /// CDN��Դ����Ϣ
    /// </summary>
    public Dictionary<string, VersionFileEntity> VersionDic = new Dictionary<string, VersionFileEntity>();


    /// <summary>
    /// ��ȡCDN�ϵ���Դ���İ汾��Ϣ(�������һ��Ҫ�ܷ�����Ϣ)
    /// </summary>
    public VersionFileEntity GetVersionFileEntity(string assetbundlePath)
    {
        VersionFileEntity entity = null;
        VersionDic.TryGetValue(assetbundlePath, out entity);
        return entity;
    }

    /// <summary>
    /// �����ļ�������(True==����Ҫ����)
    /// </summary>
    public bool CheckVersionChangeSingle(string assetBundleName)
    {
        if (VersionDic.TryGetValue(assetBundleName, out VersionFileEntity cdnAssetBundleInfo))
        {
            if (VersionLocalModel.Instance.VersionDic.TryGetValue(cdnAssetBundleInfo.AssetBundleName, out VersionFileEntity LocalAssetsAssetBundleInfo))
            {
                //��д���� CDNҲ�� ��֤MD5
                return cdnAssetBundleInfo.MD5.Equals(LocalAssetsAssetBundleInfo.MD5, StringComparison.CurrentCultureIgnoreCase);
            }
        }
        return false;//CDN������
    }
}
