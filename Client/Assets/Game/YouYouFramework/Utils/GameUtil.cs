using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using YouYouFramework;
using Cysharp.Threading.Tasks;
using YouYouMain;


public class GameUtil
{
    /// <summary>
    /// 加载FBX嵌入的所有动画
    /// </summary>
    public static AnimationClip[] LoadInitRoleAnimationsByFBX(string assetFullPath)
    {
        if (MainEntry.IsAssetBundleMode)
        {
            AssetInfoEntity m_CurrAssetEnity = GameEntry.Loader.AssetInfo.GetAssetEntity(assetFullPath);
            AssetBundle bundle = GameEntry.Loader.LoadAssetBundle(m_CurrAssetEnity.AssetBundleFullPath);
            return bundle.LoadAllAssets<AnimationClip>();
        }
        else
        {
            AnimationClip[] clipArray = null;
#if UNITY_EDITOR
            UnityEngine.Object[] objs = UnityEditor.AssetDatabase.LoadAllAssetsAtPath(assetFullPath);
            List<AnimationClip> clips = new List<AnimationClip>();
            foreach (var item in objs)
            {
                if (item is AnimationClip) clips.Add(item as T);
            }
            clipArray = clips.ToArray();
#endif
            return clipArray;
        }
    }

    /// <summary>
    /// 获取路径的最后名称
    /// </summary>
    public static string GetLastPathName(string path)
    {
        if (path.IndexOf('/') == -1)
        {
            return path;
        }
        return path.Substring(path.LastIndexOf('/') + 1);
    }

    /// <summary>
    /// 加载Prefab并克隆
    /// </summary>
    public static GameObject LoadPrefabClone(string prefabFullPath, Transform parent = null)
    {
        AssetReferenceEntity referenceEntity = GameEntry.Loader.LoadMainAsset(prefabFullPath);
        if (referenceEntity != null)
        {
            GameObject obj = UnityEngine.Object.Instantiate(referenceEntity.Target as GameObject, parent);
            AutoReleaseHandle.Add(referenceEntity, obj);
            return obj;
        }
        return null;
    }
    public static async UniTask<GameObject> LoadPrefabCloneAsync(string prefabFullPath, Transform parent = null)
    {
        AssetReferenceEntity referenceEntity = await GameEntry.Loader.LoadMainAssetAsync(prefabFullPath);
        if (referenceEntity != null)
        {
            GameObject obj = UnityEngine.Object.Instantiate(referenceEntity.Target as GameObject, parent);
            AutoReleaseHandle.Add(referenceEntity, obj);
            return obj;
        }
        return null;
    }
}