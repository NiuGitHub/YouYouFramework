using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YouYou;

public class BtnBack : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            GameEntry.Log(LogCategory.ZhangSan, "�ص���һ������");
            GameEntry.Procedure.ChangeState(ProcedureState.Main);
            GameEntry.Scene.UnLoadCurrScene();
        });
    }
}
