using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YouYouFramework;

public class BtnBack : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            GameEntry.Log(LogCategory.Normal, "�ص���һ������");
            GameEntry.UI.OpenUIForm<LoadingForm>();
            GameEntry.Scene.LoadSceneAction(SceneGroupName.Main);
        });
    }
}
