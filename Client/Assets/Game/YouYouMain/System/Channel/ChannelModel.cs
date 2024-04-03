using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChannelModel
{
    public static ChannelModel Instance { get; private set; } = new ChannelModel();

    /// <summary>
    /// ������������
    /// </summary>
    public ChannelConfigEntity CurrChannelConfig { get; private set; }

    /// <summary>
    /// ���ڼ���ʱ����ı��ط�����ʱ��
    /// </summary>
    public long CurrServerTime
    {
        get
        {
            if (CurrChannelConfig == null) return (long)Time.unscaledTime;
            return CurrChannelConfig.ServerTime + (long)Time.unscaledTime;
        }
    }

    public ChannelModel()
    {
        CurrChannelConfig = new ChannelConfigEntity();
    }
}