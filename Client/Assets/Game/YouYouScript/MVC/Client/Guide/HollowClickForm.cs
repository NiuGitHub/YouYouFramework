using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YouYouFramework;

/// <summary>
/// �ο����֣�ֻ���ο�����ɵ����button����������棬����ʵ�ʵ�ҵ�񳡾����棩
/// </summary>
[RequireComponent(typeof(HollowOutMask))]
public class HollowClickForm : MonoBehaviour
{
    public static GameObject ShowDialog(string formName)
    {
        return GameUtil.LoadPrefabClone(formName);
    }
    private void Start()
    {
        GetComponent<HollowOutMask>().IsAcross = true;
    }
}
