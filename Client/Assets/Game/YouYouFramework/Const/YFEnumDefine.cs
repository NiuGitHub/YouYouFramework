using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YouYou
{
    /// <summary>
    /// 日志分类
    /// </summary>
    public enum LogCategory
    {
        /// <summary>
        /// 框架日志
        /// </summary>
        Framework,
        /// <summary>
        /// 流程
        /// </summary>
        Procedure,
        /// <summary>
        /// 资源管理
        /// </summary>
        Loader,
        /// <summary>
        /// 网络消息
        /// </summary>
        NetWork,
        /// <summary>
        /// 新手引导
        /// </summary>
        Guide,
        UI,
        Audio,
        Scene,
        Pool,

        /// <summary>
        /// 张三(程序员)
        /// </summary>
        ZhangSan,
        /// <summary>
        /// 李四(程序员)
        /// </summary>
        LiSi
    }

    /// <summary>
    /// 提示窗口,按钮显示方式
    /// </summary>
    public enum DialogFormType
    {
        /// <summary>
        /// 确定按钮
        /// </summary>
        Affirm,
        /// <summary>
        /// 确定,取消按钮
        /// </summary>
        AffirmAndCancel
    }

    /// <summary>
    /// UI窗口的显示类型
    /// </summary>
    public enum UIFormShowMode
    {
        Normal = 0,
        /// <summary>
        /// 反切
        /// </summary>
        ReverseChange = 1,
    }

}