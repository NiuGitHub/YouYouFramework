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
    public string CDNVersion;

    /// <summary>
    /// CDN��Դ����Ϣ
    /// </summary>
    public Dictionary<string, VersionFileEntity> VersionDic = new Dictionary<string, VersionFileEntity>();


    /// <summary>
    /// ��ȡCDN�ϵ���Դ���İ汾��Ϣ(�������һ��Ҫ�ܷ�����Ϣ)
    /// </summary>
    /// <param name="assetbundlePath"></param>
    public VersionFileEntity GetVersionFileEntity(string assetbundlePath)
    {
        VersionFileEntity entity = null;
        VersionDic.TryGetValue(assetbundlePath, out entity);
        return entity;
    }
}
