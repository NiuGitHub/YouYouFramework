using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YouYou;

/// <summary>
/// �ο����֣�ȫ���ɵ��
/// </summary>
[RequireComponent(typeof(Button))]
[RequireComponent(typeof(HollowOutMask))]
public class FormHollow2 : UIFormBase
{
    [Header("ǿ�ƹۿ�ʱ��")]
    [SerializeField] float DelayTime;

    private Button button;

    public static void ShowDialog(string formName)
    {
        GameEntry.UI.OpenUIForm<FormHollow2>(formName);
    }

    protected override void Start()
    {
        base.Start();
        GetComponent<HollowOutMask>().IsAcross = false;

        button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            //�ر��Լ�
            Close();

            //������һ������
            GuideCtrl.Instance.NextGroup(GuideCtrl.Instance.CurrentState);
        });
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        //ǿ����ҿ�һ���
        if (DelayTime > 0)
        {
            button.enabled = false;
            GameEntry.Time.CreateTimer(this, DelayTime, () =>
            {
                button.enabled = true;
            });
        }
    }
}
