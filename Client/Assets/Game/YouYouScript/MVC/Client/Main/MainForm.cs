using YouYouMain;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YouYouFramework;

public partial class MainForm : UIFormBase
{
    [SerializeField] Transform BtnGroup;

    protected override void Awake()
    {
        base.Awake();
        foreach (Transform child in BtnGroup)
        {
            Button button = child.GetComponent<Button>();
            if (button != null)
            {
                button.onClick.AddListener(() =>
                {
                    if (MainEntry.IsAssetBundleMode)
                    {
                        GameEntry.LogError(LogCategory.Framework, "��ǰ��AssetBundle����ģʽ����֧�ּ���YouYouTest�ļ����ڵĲ��Գ����� ��Ϊ����û�з���Download�ļ����ڴ�������Բ�֧���ȸ��º�AssetBundle����");
                        return;
                    }
                    GameEntry.UI.CloseAllDefaultUIForm();
                    GameEntry.UI.OpenUIForm<LoadingForm>();
                    GameEntry.Scene.LoadSceneAction(button.GetComponentInChildren<Text>().text);
                });
            }
        }
    }
}
