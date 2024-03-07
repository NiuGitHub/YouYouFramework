using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YouYou;

/// <summary>
/// ��Ҫ���ڳ�ʼ�������
/// </summary>
public class Reddot : MonoBehaviour
{
    public bool IsRoot;

    [Header("��ѡ�������ID����ӳ��·��")]
    public int serverId;

    void Awake()
    {
        //��ȡ����·������̬��ӽڵ�
        string Path = GetPath(this);
        if (serverId > 0)
        {
            ReddotManager.Instance.SetServerIdOfPath(serverId, Path);
        }
        else
        {
            ReddotManager.Instance.GetTreeNode(Path);
        }
        Debug.Log("��ʼ�����==" + Path);
    }

    private static string GetPath(Reddot reddot)
    {
        if (reddot.transform.parent == null)
        {
            return reddot.name;
        }

        Reddot parentReddot = reddot.transform.parent.GetComponent<Reddot>();
        if (parentReddot == null)
        {
            return reddot.name;
        }

        if (parentReddot.IsRoot)
        {
            return parentReddot.name + "/" + reddot.name;
        }

        return GetPath(parentReddot) + "/" + reddot.name;
    }
}
