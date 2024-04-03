using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YouYouFramework;


/// <summary>
/// ĳ��״̬���Ĺ�����
/// </summary>
public class TestFsmMgr
{
    public enum TestFsmState
    {
        None,
        State1,
        State2,
    }

    /// <summary>
    /// ��ǰ״̬��
    /// </summary>
    public Fsm<TestFsmMgr> CurrFsm { get; private set; }

    /// <summary>
    /// ��ǰ״̬Type
    /// </summary>
    public TestFsmState CurrProcedureState
    {
        get
        {
            return (TestFsmState)CurrFsm.CurrStateType;
        }
    }

    internal void Init()
    {
        //�õ�ö�ٵĳ���
        int count = Enum.GetNames(typeof(TestFsmState)).Length;
        FsmState<TestFsmMgr>[] states = new FsmState<TestFsmMgr>[count];
        states[(byte)TestFsmState.None] = new TestFsmStateNone();
        states[(byte)TestFsmState.State1] = new TestFsmState1();
        states[(byte)TestFsmState.State1] = new TestFsmState2();

        CurrFsm = GameEntry.Fsm.Create(this, states);
    }
    internal void OnUpdate()
    {
        CurrFsm.OnUpdate();
    }

    /// <summary>
    /// �л�״̬
    /// </summary>
    public void ChangeState(TestFsmState state)
    {
        CurrFsm.ChangeState((sbyte)state);
    }

    public void SetData<TData>(string key, TData value)
    {
        CurrFsm.SetData(key, value);
    }
    public TData GetDada<TData>(string key)
    {
        return CurrFsm.GetDada<TData>(key);
    }
}

public class TestFsmStateNone : FsmState<TestFsmMgr>
{
}
public class TestFsmState1 : FsmState<TestFsmMgr>
{
    internal override void OnEnter()
    {
        base.OnEnter();
        GameEntry.Log(LogCategory.Normal, CurrFsm.GetState(CurrFsm.CurrStateType).ToString() + "==>> OnEnter()");
    }
    internal override void OnLeave()
    {
        base.OnLeave();
        GameEntry.Log(LogCategory.Normal, CurrFsm.GetState(CurrFsm.CurrStateType).ToString() + "==>> OnLeave()");
    }
    internal override void OnUpdate()
    {
        base.OnUpdate();
        GameEntry.Log(LogCategory.Normal, CurrFsm.GetState(CurrFsm.CurrStateType).ToString() + "==>OnUpdate()");
    }
    internal override void OnDestroy()
    {
        base.OnDestroy();
        GameEntry.Log(LogCategory.Normal, CurrFsm.GetState(CurrFsm.CurrStateType).ToString() + "==>OnDestroy()");
    }

}
public class TestFsmState2 : FsmState<TestFsmMgr>
{
    internal override void OnEnter()
    {
        base.OnEnter();
        GameEntry.Log(LogCategory.Normal, CurrFsm.GetState(CurrFsm.CurrStateType).ToString() + "==>> OnEnter()");
    }
    internal override void OnLeave()
    {
        base.OnLeave();
        GameEntry.Log(LogCategory.Normal, CurrFsm.GetState(CurrFsm.CurrStateType).ToString() + "==>> OnLeave()");
    }
}