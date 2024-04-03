using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YouYouFramework;

/// <summary>
/// ���������,  Ҳ����������ȡ�ı�������, ֻ��������һ��
/// </summary>
public class VarObjectPool
{
    /// <summary>
    /// �����������
    /// </summary>
    private object m_VarObjectLock = new object();

#if UNITY_EDITOR
    /// <summary>
    /// �ڼ��������ʾ����Ϣ
    /// </summary>
    public Dictionary<Type, int> VarObjectInspectorDic = new Dictionary<Type, int>();
#endif

    /// <summary>
    /// ȡ��һ����������
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T DequeueVarObject<T>() where T : VariableBase, new()
    {
        lock (m_VarObjectLock)
        {
            T item = GameEntry.Pool.ClassObjectPool.Dequeue<T>();
#if UNITY_EDITOR
            Type t = item.GetType();
            if (VarObjectInspectorDic.ContainsKey(t))
            {
                VarObjectInspectorDic[t]++;
            }
            else
            {
                VarObjectInspectorDic[t] = 1;
            }
#endif
            return item;
        }
    }

    /// <summary>
    /// ��������س�
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="item"></param>
    public void EnqueueVarObject<T>(T item) where T : VariableBase
    {
        lock (m_VarObjectLock)
        {
            GameEntry.Pool.ClassObjectPool.Enqueue(item);
#if UNITY_EDITOR
            Type t = item.GetType();
            if (VarObjectInspectorDic.ContainsKey(t))
            {
                VarObjectInspectorDic[t]--;
                if (VarObjectInspectorDic[t] == 0)
                {
                    VarObjectInspectorDic.Remove(t);
                }
            }
#endif
        }
    }

}
