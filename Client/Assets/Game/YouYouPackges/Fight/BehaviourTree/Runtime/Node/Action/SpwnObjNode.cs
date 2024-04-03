using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using YouYouFramework;

/// <summary>
/// ȡ����
/// </summary>
[NodeInfo(Name = "����/ȡ����")]
public class SpwnObjNode : BaseActionNode
{
    private static FieldInfo[] fieldInfos = typeof(SpwnObjNode).GetFields(BindingFlags.Public | BindingFlags.Instance);

    /// <inheritdoc />
    public override FieldInfo[] FieldInfos => fieldInfos;

    /// <summary>
    /// Ҫȡ�Ķ�������
    /// </summary>
    [BBParamInfo(Name = "Ҫȡ�Ķ�������")]
    public BBParamString SpwnObjName;

    protected override void OnStart()
    {
        TimelineCtrl timelineCtrl = Owner.RoleCtrl.CreateSkillTimeLine(SpwnObjName.Value);
        GameEntry.Log(LogCategory.Skill, "��ʼ�ͷż��� Skill1");
        timelineCtrl.OnStopped = () =>
        {
            Finish(true);
            GameEntry.Log(LogCategory.Skill, "�����ͷ���� Skill1");

            Owner.Restart();
            GameEntry.Log(LogCategory.Skill, "������Ϊ��");
        };
    }

    protected override void OnCancel()
    {

    }

}
