using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YouYouFramework;

public class TestPool : MonoBehaviour
{
    List<GameObject> objList = new List<GameObject>();

    async void Update()
    {
        if (Input.GetKeyUp(KeyCode.A))
        {
            //�Ӷ������ȡ����
            GameObject obj = await GameEntry.Pool.GameObjectPool.SpawnAsync(PrefabName.Skill1);
            objList.Add(obj);
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            //�Ӷ������ȡ����
            GameObject obj = GameEntry.Pool.GameObjectPool.Spawn(PrefabName.Skill1);
            objList.Add(obj);

            //3����Զ��س�
            AutoDespawnHandle autoDespawnHandle = obj.gameObject.GetOrCreatComponent<AutoDespawnHandle>();
            autoDespawnHandle.SetDelayTimeDespawn(3);
            autoDespawnHandle.OnDespawn = () =>
            {
                objList.Remove(obj);
            };
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            //�سض���
            if (objList.Count > 0) GameEntry.Pool.GameObjectPool.Despawn(objList[0]);
        }
    }
}