using YouYouMain;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YouYouFramework;


/// <summary>
/// 预加载流程
/// </summary>
public class ProcedurePreload : ProcedureBase
{
    /// <summary>
    /// 目标进度(实际进度)
    /// </summary>
    private float m_TargetProgress;
    /// <summary>
    /// 当前进度(模拟进度)
    /// </summary>
    private float m_CurrProgress;

    public override void OnEnter(int lastState)
    {
        base.OnEnter(lastState);
        MainEntry.Instance.ActionPreloadBegin?.Invoke();

        m_CurrProgress = 0;

        BeginTask();
    }
    public override void OnUpdate(float elapseSeconds)
    {
        base.OnUpdate(elapseSeconds);
        //模拟加载进度条
        if (m_CurrProgress < m_TargetProgress)
        {
            //根据实际情况调节速度, 加载已完成和未完成, 模拟进度增值速度分开计算!
            if (m_TargetProgress < 1)
            {
                m_CurrProgress += Time.deltaTime * 0.5f;
            }
            else
            {
                m_CurrProgress += Time.deltaTime * 0.8f;
            }
            m_CurrProgress = Mathf.Min(m_CurrProgress, m_TargetProgress);//这里是为了防止进度超过100%， 比如完成了显示102%
            MainEntry.Instance.ActionPreloadUpdate?.Invoke(m_CurrProgress);
        }

        if (m_CurrProgress == 1)
        {
            MainEntry.Instance.ActionPreloadComplete?.Invoke();

            //进入到业务流程
            GameEntry.Procedure.ChangeState(ProcedureManager.EState.Login);
        }
    }

    /// <summary>
    /// 开始任务
    /// </summary>
    private void BeginTask()
    {
        TaskGroup taskGroup = GameEntry.Task.CreateTaskGroup();

        if (MainEntry.IsAssetBundleMode)
        {
            //初始化资源信息
            taskGroup.AddTask(async (taskRoutine) =>
            {
                bool isSuccess = await GameEntry.Loader.AssetInfo.InitAssetInfo();
                if (isSuccess)
                {
                    taskRoutine.TaskComplete();
                }
            });

            //加载自定义Shader
            taskGroup.AddTask(async (taskRoutine) =>
            {
                AssetBundle bundle = await GameEntry.Loader.LoadAssetBundleAsync(YFConstDefine.CusShadersAssetBundlePath);
                bundle.LoadAllAssets();
                //Shader.WarmupAllShaders();
                taskRoutine.TaskComplete();
            });
        }

        //加载Excel
        taskGroup.AddTask((taskRoutine) =>
        {
            GameEntry.DataTable.LoadDataAllTable();
            taskRoutine.TaskComplete();
        });

        //设置多语言(因为多语言是读表的，所以得先加载Excel再设置多语言)
        taskGroup.AddTask((taskRoutine) =>
        {
            GameEntry.Localization.ChangeLanguageToSystem();
            taskRoutine.TaskComplete();
        });

        taskGroup.OnCompleteOne = () =>
        {
            m_TargetProgress = taskGroup.CurrCount / (float)taskGroup.TotalCount;
        };
        taskGroup.Run();
    }

}