using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using YouYou;

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
        GameEntry.Log(LogCategory.ZhangSan, "��ʼ�ͷż��� Skill1");
        timelineCtrl.OnStopped = () =>
        {
            Finish(true);
            GameEntry.Log(LogCategory.ZhangSan, "�����ͷ���� Skill1");

            Owner.Restart();
            GameEntry.Log(LogCategory.ZhangSan, "������Ϊ��");
        };
    }

    protected override void OnCancel()
    {

    }

}
