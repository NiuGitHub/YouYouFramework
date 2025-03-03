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
                if (item is AnimationClip)
                {
                    clips.Add(item as AnimationClip);
                }
            }
            clipArray = clips.ToArray();
#endif
            return clipArray;
        }
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
            AssetReleaseHandle.Add(referenceEntity, obj);
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
            AssetReleaseHandle.Add(referenceEntity, obj);
            return obj;
        }
        return null;
    }
}