using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YouYouFramework;


/// <summary>
/// Asset��, ��������ʱ, �Զ������ü���-1
/// </summary>
public class AutoReleaseHandle : MonoBehaviour
{
    private List<AssetReferenceEntity> releaseList = new List<AssetReferenceEntity>();

    public static void Add(AssetReferenceEntity referenceEntity, GameObject target)
    {
        if (target == null)
        {
            GameObject SceneRoot = GameEntry.Pool.GameObjectPool.YouYouObjPool;
            target = SceneRoot;
            GameEntry.Log(LogCategory.Loader, string.Format("��Ϊ{0}û�пɰ󶨵�target�� ���԰󶨵���{1}�ϣ� �浱ǰ�������ٶ��������ü���", referenceEntity.AssetFullPath, SceneRoot));
        }

        if (target != null)
        {
            AutoReleaseHandle handle = target.GetOrCreatComponent<AutoReleaseHandle>();
            handle.releaseList.Add(referenceEntity);
            referenceEntity.ReferenceAdd();
        }
    }

    private void OnDestroy()
    {
        foreach (AssetReferenceEntity referenceEntity in releaseList)
        {
            referenceEntity.ReferenceRemove();
        }
    }
}
