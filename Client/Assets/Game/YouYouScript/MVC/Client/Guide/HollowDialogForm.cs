using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YouYouFramework;

/// <summary>
/// �ο����֣�ȫ���ɵ��
/// </summary>
[RequireComponent(typeof(Button))]
[RequireComponent(typeof(HollowOutMask))]
public class HollowDialogForm : MonoBehaviour
{
    [Header("ǿ�ƹۿ�ʱ��")]
    [SerializeField] float DelayTime;

    private Button button;

    public static void ShowDialog(string formName)
    {
        GameUtil.LoadPrefabClone(formName);
    }
    private void Start()
    {
        GetComponent<HollowOutMask>().IsAcross = false;

        button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            //�ر��Լ�
            Destroy(gameObject);

            //������һ������
            GuideCtrl.Instance.NextGroup(GuideCtrl.Instance.CurrentState);
        });
    }
    private void OnEnable()
    {
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
