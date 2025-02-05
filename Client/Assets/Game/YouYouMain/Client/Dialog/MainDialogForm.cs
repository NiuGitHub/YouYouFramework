using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//ΪʲôҪ��2��DialogForm?  ��Ϊ1��Ҫ�ȸ�(���Զ���, ���ܳ����ڼ����½���) 1����Ҫ�ȸ�(���ܶ���, ���Գ����ڼ����½���)
public class MainDialogForm : MonoBehaviour
{
    private static MainDialogForm Instance;

    /// <summary>
    /// ��ʾ����,��ť��ʾ��ʽ
    /// </summary>
    public enum DialogFormType
    {
        /// <summary>
        /// ȷ����ť
        /// </summary>
        Affirm,
        /// <summary>
        /// ȷ��,ȡ����ť
        /// </summary>
        AffirmAndCancel
    }

    [SerializeField] Text textTitle;
    [SerializeField] Text textContent;
    [SerializeField] Button btnOK;
    [SerializeField] Button btnCancel;
    [SerializeField] Text textOK;
    [SerializeField] Text textCancel;

    private Action actionOK;

    private Action actionCancel;


    protected void Awake()
    {
        Instance = this;
        gameObject.SetActive(false);

        btnOK.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
            actionOK?.Invoke();
        });
        btnCancel.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
            actionCancel?.Invoke();
        });
    }

    public static void ShowForm(string contenct = "", string title = "��ʾ", string textBtn1 = "ȷ��", string textBtn2 = "ȡ��", DialogFormType type = DialogFormType.AffirmAndCancel, Action okAction = null, Action cancelAction = null)
    {
        Instance.gameObject.SetActive(true);
        Instance.SetUI(contenct, title, textBtn1, textBtn2, type, okAction, cancelAction);
    }

    private void SetUI(string contenct = "", string title = "��ʾ", string textBtn1 = "ȷ��", string textBtn2 = "ȡ��", DialogFormType type = DialogFormType.AffirmAndCancel, Action okAction = null, Action cancelAction = null)
    {
        gameObject.SetActive(true);

        //��������
        textTitle.text = title;
        textContent.text = contenct;
        textOK.text = textBtn1;
        textCancel.text = textBtn2;

        //�����ť������
        switch (type)
        {
            case DialogFormType.Affirm:
                btnOK.gameObject.SetActive(true);
                btnCancel.gameObject.SetActive(false);
                break;
            case DialogFormType.AffirmAndCancel:
                btnCancel.gameObject.SetActive(true);
                btnOK.gameObject.SetActive(true);
                break;
        }

        actionOK = okAction;
        actionCancel = cancelAction;
    }
}
