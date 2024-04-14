using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ComponentAutoBindTool))]//�ű�����
public class ComponentAutoBindBase : MonoBehaviour
{
    protected virtual void Awake()
    {
        GetBindComponents(gameObject);
    }
    protected virtual void GetBindComponents(GameObject go)
    {
    }
}
