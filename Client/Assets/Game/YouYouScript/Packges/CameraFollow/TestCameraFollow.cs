using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YouYou;

public class TestCameraFollow : MonoBehaviour
{
    void Update()
    {
        //Camera����
        CameraFollowCtrl.Instance.transform.position = transform.position;

        //Camera��ת
        if (Input.GetKey(KeyCode.W))
        {
            //�����������������GameEntry,  ��ȻҲ����ʹ��GameEntry.Input.GetAxis(InputConst.MouseY)
            CameraFollowCtrl.Instance.SetCameraRotateY(Time.deltaTime * 1000);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            CameraFollowCtrl.Instance.SetCameraRotateY(-Time.deltaTime * 1000);
        }
        if (Input.GetKey(KeyCode.A))
        {
            CameraFollowCtrl.Instance.SetCameraRotateX(Time.deltaTime * 1000);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            CameraFollowCtrl.Instance.SetCameraRotateX(-Time.deltaTime * 1000);
        }

        //Camera����
        if (Input.GetKey(KeyCode.Q))
        {
            CameraFollowCtrl.Instance.SetCameraDistance(Time.deltaTime * 10);
        }
        else if (Input.GetKey(KeyCode.E))
        {
            CameraFollowCtrl.Instance.SetCameraDistance(-Time.deltaTime * 10);
        }
    }
}
