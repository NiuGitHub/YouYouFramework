using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YouYou;

public class Reddot : MonoBehaviour
{
    public bool IsRoot;

    void Awake()
    {
        //��ȡ����·������̬��ӽڵ�
        string Path = GetPath(this);
        Debug.Log("��ʼ�����==" + Path);
        GameEntry.Reddot.GetTreeNode(Path);
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
