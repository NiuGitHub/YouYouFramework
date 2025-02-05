using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YouYouFramework;


public class TipForm : UIFormBase
{
    [SerializeField] Text textContent;

    public static void ShowFormByKey(string key)
    {
        if (GameEntry.DataTable.Sys_TipDBModel.keyDic.TryGetValue(key, out var entity))
        {
            ShowForm(entity.Content, entity.Duration);
        }
        else
        {
            GameEntry.LogError("��ǰKey�Ҳ�����Ӧ�ı������, Key==" + key);
        }
    }
    public static void ShowForm(string contenct = "", float duration = 3f)
    {
        TipForm form = GameEntry.UI.OpenUIForm<TipForm>();
        form.SetUI(contenct, duration);
    }

    private void SetUI(string contenct = "", float duration = 3f)
    {
        //��������
        textContent.text = contenct;

        GameEntry.Time.CreateTimer(this, duration, () =>
        {
            Close();
        },true);
    }

}
