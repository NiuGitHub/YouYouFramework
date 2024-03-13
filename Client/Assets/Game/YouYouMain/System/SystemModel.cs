using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Main
{
    public class SystemModel
    {
        private static SystemModel instance;
        public static SystemModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SystemModel();
                }
                return instance;
            }
        }

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

        public SystemModel()
        {
            CurrChannelConfig = new ChannelConfigEntity();
        }
    }
}