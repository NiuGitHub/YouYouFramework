using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using YouYouMain;


/// <summary>
/// AssetBundle�汾�ļ�, ���ؿ�д����VersionFile.json��Model
/// </summary>
public class VersionLocalModel
{
    public static VersionLocalModel Instance { get; private set; } = new VersionLocalModel();


    /// <summary>
    /// ��д�� �汾�ļ�·��
    /// </summary>
    public string LocalVersionFilePath
    {
        get
        {
            return string.Format("{0}/{1}", Application.persistentDataPath, YFConstDefine.VersionFileName);
        }
    }

    /// <summary>
    /// ��д����Դ�汾��
    /// </summary>
    public string LocalAssetsVersion;

    /// <summary>
    /// ��д����Դ����Ϣ
    /// </summary>
    public Dictionary<string, VersionFileEntity> LocalAssetsVersionDic = new Dictionary<string, VersionFileEntity>();

    /// <summary>
    /// �����д���汾��Ϣ
    /// </summary>
    /// <param name="entity"></param>
    public void SaveVersion(VersionFileEntity entity)
    {
        LocalAssetsVersionDic[entity.AssetBundleName] = entity;

        //����汾�ļ�
        string json = LocalAssetsVersionDic.ToJson();
        IOUtil.CreateTextFile(LocalVersionFilePath, json);
    }

    /// <summary>
    /// �����д����Դ�汾��
    /// </summary>
    public void SetAssetVersion(string version)
    {
        LocalAssetsVersion = version;
        PlayerPrefs.SetString(YFConstDefine.AssetVersion, version);
    }

    /// <summary>
    /// ��ȡ��д���汾�ļ��Ƿ����
    /// </summary>
    /// <returns></returns>
    public bool GetVersionFileExists()
    {
        return File.Exists(LocalVersionFilePath);
    }
}
