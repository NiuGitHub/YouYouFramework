using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YouYou;

/// <summary>
/// �ο����֣�ֻ���ο�����ɵ����button����������棬����ʵ�ʵ�ҵ�񳡾����棩
/// </summary>
[RequireComponent(typeof(HollowOutMask))]
public class FormHollow : UIFormBase
{
    public static void ShowDialog(string formName)
    {
        GameEntry.UI.OpenUIForm<FormHollow>(formName);
    }

    protected override void Start()
    {
        base.Start();
        GetComponent<HollowOutMask>().IsAcross = true;
    }
}
