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
    public string VersionFilePath
    {
        get
        {
            return string.Format("{0}/{1}", Application.persistentDataPath, YFConstDefine.VersionFileName);
        }
    }

    /// <summary>
    /// ��д����Դ�汾��
    /// </summary>
    public string AssetsVersion;

    /// <summary>
    /// ��д����Դ����Ϣ
    /// </summary>
    public Dictionary<string, VersionFileEntity> VersionDic = new Dictionary<string, VersionFileEntity>();

    /// <summary>
    /// �����д���汾��Ϣ
    /// </summary>
    public void SaveVersion(VersionFileEntity entity)
    {
        VersionDic[entity.AssetBundleName] = entity;

        //����汾�ļ�
        string json = VersionDic.ToJson();
        IOUtil.CreateTextFile(VersionFilePath, json);
    }

    /// <summary>
    /// �����д����Դ�汾��
    /// </summary>
    public void SetAssetVersion(string version)
    {
        AssetsVersion = version;
        PlayerPrefs.SetString(YFConstDefine.AssetVersion, version);
    }

    /// <summary>
    /// ��ȡ��д���汾�ļ��Ƿ����
    /// </summary>
    public bool GetVersionFileExists()
    {
        return File.Exists(VersionFilePath);
    }
}
