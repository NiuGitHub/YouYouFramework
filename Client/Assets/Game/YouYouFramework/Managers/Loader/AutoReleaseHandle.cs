using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YouYou;

public class AutoReleaseHandle : MonoBehaviour
{
    private List<AssetReferenceEntity> releaseList = new List<AssetReferenceEntity>();

    public static void Add(AssetReferenceEntity referenceEntity, GameObject target)
    {
        if (target == null)
        {
            GameObject SceneRoot = null;//DOTO��������ÿ�������Ķ���ظ��ڵ㣬�����ٲ�
            target = SceneRoot;
        }

        if (target != null)
        {
            AutoReleaseHandle handle = target.GetComponent<AutoReleaseHandle>();
            if (handle == null)
            {
                handle = target.AddComponent<AutoReleaseHandle>();
            }
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
