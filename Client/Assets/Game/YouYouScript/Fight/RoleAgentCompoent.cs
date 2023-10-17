using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using YouYou;

public class RoleAgentCompoent : MonoBehaviour
{
    public NavMeshAgent Agent { get; private set; }

    /// <summary>
    /// ·����
    /// </summary>
    private Vector3[] m_VectorPath;

    /// <summary>
    /// ��ǰ·��������
    /// </summary>
    private int m_CurrPointIndex;

    /// <summary>
    /// ת�����
    /// </summary>
    private bool m_TurnComplete = false;

    private Vector3 endPos;
    private Vector3 beginPos;
    private Vector3 dir;
    private Vector3 rotation;
    [SerializeField] float runSpeed = 10; //�ٶ�
    private float modifyRunSpeed = 10;//�����ٶ�

    private NavMeshPath path;

    public Action MoveTargetEnd;

    void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
        path = new NavMeshPath();
    }
    void Update()
    {
        if (m_VectorPath == null)
        {
            return;
        }

        //�������·�������� �л�����
        if (m_CurrPointIndex >= m_VectorPath.Length)
        {
            m_VectorPath = null;
            MoveTargetEnd?.Invoke();
            return;
        }

        if (!m_TurnComplete)
        {
            endPos = m_VectorPath[m_CurrPointIndex];
            beginPos = m_VectorPath[m_CurrPointIndex - 1];

            dir = (endPos - beginPos).normalized;

            rotation = dir;
            //����ת��
            rotation.y = 0;
            transform.rotation = Quaternion.LookRotation(rotation);

            m_TurnComplete = true;
        }
        Agent.Move(dir * Time.deltaTime * modifyRunSpeed);

        //�ж��Ƿ�Ӧ������һ�����ƶ�
        float dis = Vector3.Distance(transform.position, beginPos);

        //��������ʱĿ�����
        if (dis >= Vector3.Distance(endPos, beginPos))
        {
            //λ������
            SetPostionAndRotation(endPos, transform.rotation);

            m_TurnComplete = false;
            m_CurrPointIndex++;
        }
    }

    public void SetPostionAndRotation(Vector3 pos, Quaternion rot)
    {
        Agent.enabled = false;
        transform.position = pos;
        transform.rotation = rot;
        Agent.enabled = true;
    }

    public NavMeshPathStatus ClickMove(Vector3 targetPos)
    {
        m_CurrPointIndex = 1;
        m_TurnComplete = false;
        //runSpeed = 10;
        modifyRunSpeed = runSpeed;

        //����·��
        Agent.CalculatePath(targetPos, path);

        if (path.status == NavMeshPathStatus.PathComplete)
        {
            m_VectorPath = path.corners;
        }
        return path.status;
    }
    public void ClickMoveStop()
    {
        m_VectorPath = null;
    }

    public void JoystickMove(Vector2 dir)
    {
        if (dir == Vector2.zero) return;
        //1.���ø�����λ��
        //GameEntry.Data.RoleDataMgr.CurrPlayerMoveHelper.transform.position = new Vector3(transform.position.x + dir.x, transform.position.y, transform.position.z + dir.y);

        ////2.�ø�����������ת
        //GameEntry.Data.RoleDataMgr.CurrPlayerMoveHelper.transform.RotateAround(transform.position, Vector3.up, CameraCtrl.Instance.transform.localEulerAngles.y);//- 90

        ////3.�õ�����Ҫ�ƶ��ķ���
        //Vector3 direction = GameEntry.Data.RoleDataMgr.CurrPlayerMoveHelper.transform.position - transform.position;

        //direction.Normalize();//��һ����ȷ������һ��
        //direction = direction * Time.deltaTime * modifyRunSpeed;
        //transform.rotation = Quaternion.LookRotation(direction);
        //Agent.Move(direction);

    }
}
