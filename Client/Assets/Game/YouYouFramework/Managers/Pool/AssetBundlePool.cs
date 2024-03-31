using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace YouYouFramework
{
    public class AssetBundlePool
    {
#if UNITY_EDITOR
        /// <summary>
        /// �ڼ��������ʾ����Ϣ
        /// </summary>
        public Dictionary<string, AssetBundleReferenceEntity> InspectorDic = new Dictionary<string, AssetBundleReferenceEntity>();
#endif

        /// <summary>
        /// ��Դ������
        /// </summary>
        public string PoolName { get; private set; }

        /// <summary>
        /// ��Դ���ֵ�
        /// </summary>
        private Dictionary<string, AssetBundleReferenceEntity> m_AssetBundleDic;

        /// <summary>
        /// ��Ҫ�Ƴ���Key����
        /// </summary>
        private LinkedList<string> m_NeedRemoveKeyList;

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="poolName">��Դ������</param>
        public AssetBundlePool(string poolName)
        {
            PoolName = poolName;
            m_AssetBundleDic = new Dictionary<string, AssetBundleReferenceEntity>();
            m_NeedRemoveKeyList = new LinkedList<string>();
        }

        /// <summary>
        /// ע�ᵽ��Դ��
        /// </summary>
        public void Register(AssetBundleReferenceEntity entity)
        {
#if UNITY_EDITOR
            InspectorDic.Add(entity.AssetBundlePath, entity);
#endif
            m_AssetBundleDic.Add(entity.AssetBundlePath, entity);
        }

        /// <summary>
        /// ��Դȡ��
        /// </summary>
        public AssetBundleReferenceEntity Spawn(string resourceName)
        {
            if (m_AssetBundleDic.TryGetValue(resourceName, out AssetBundleReferenceEntity abReferenceEntity))
            {
                return abReferenceEntity;
            }
            return null;
        }

        /// <summary>
        /// �ͷ���Դ���п��ͷ���Դ
        /// </summary>
        public void Release()
        {
            var enumerator = m_AssetBundleDic.GetEnumerator();
            while (enumerator.MoveNext())
            {
                AssetBundleReferenceEntity abReferenceEntity = enumerator.Current.Value;
                if (abReferenceEntity.GetCanRelease())
                {
#if UNITY_EDITOR
                    if (InspectorDic.ContainsKey(abReferenceEntity.AssetBundlePath))
                    {
                        InspectorDic.Remove(abReferenceEntity.AssetBundlePath);
                    }
#endif
                    m_NeedRemoveKeyList.AddFirst(abReferenceEntity.AssetBundlePath);
                    abReferenceEntity.Release();
                }
            }

            //ѭ������ ���ֵ����Ƴ��ƶ���Key
            LinkedListNode<string> curr = m_NeedRemoveKeyList.First;
            while (curr != null)
            {
                string key = curr.Value;
                m_AssetBundleDic.Remove(key);

                LinkedListNode<string> next = curr.Next;
                m_NeedRemoveKeyList.Remove(curr);
                curr = next;
            }
        }

    }
}