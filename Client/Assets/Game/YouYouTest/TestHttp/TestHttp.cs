using YouYouMain;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YouYouFramework;


/// <summary>
/// ��ע: ��Unity�˵���/YouYouTool/YouYouEditor/ParamsSettings��,������Http������·��, ��:127.0.0.1:8083
/// ��ע2:���û�к�˸����ṩ�ӿ�, ����������޷����õ�, �������뷶���ͺ� 
/// </summary>
public class TestHttp : MonoBehaviour
{
    async void Update()
    {
        if (Input.GetKeyUp(KeyCode.A))
        {
            //Get����, �Զ��ж�HasError
            GameEntry.Http.Get("Test/AAA", callBack: (string json) =>
            {
                GameEntry.Log(LogCategory.Normal, json);
            });
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            //Get����
            GameEntry.Http.GetArgs("Test/AAA", callBack: (HttpCallBackArgs callBackArgs) =>
            {
                if (!callBackArgs.HasError)
                {
                    GameEntry.Log(LogCategory.Normal, callBackArgs.Value);
                }
            });
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            //Post����
            GameEntry.Http.Post("Test/AAA", callBack: (string json) =>
            {
                GameEntry.Log(LogCategory.Normal, json);
            });
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            //Post���� 
            string json = await GameEntry.Http.PostAsync("Test/AAA");
            GameEntry.Log(LogCategory.Normal, json);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            //Post����, �Զ�����תȦUI, ������ҵ��
            string json = await GameEntry.Http.PostAsync("Test/AAA", loadingCircle: true);
            GameEntry.Log(LogCategory.Normal, json);
        }
    }
}
