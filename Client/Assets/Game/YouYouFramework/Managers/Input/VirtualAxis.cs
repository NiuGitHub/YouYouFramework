using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YouYouFramework
{
    //������Ͱ�ť��-�������ƶ�����
    //����ӳ�䵽�������ݸˣ���б�������ǵȣ�ȡ���������ʵ�֡�
    //Ҳ���������������豸ʵ�֣���kinect�����Ӵ�������
    public class VirtualAxis
    {
        public string Name { get; private set; }
        private float m_Value;
        public bool MatchWithInputManager { get; private set; }


        public VirtualAxis(string name) : this(name, true)
        {
        }
        public VirtualAxis(string name, bool matchToInputSettings)
        {
            Name = name;
            MatchWithInputManager = matchToInputSettings;
        }

        public void Update(float value)
        {
            m_Value = value;
        }

        public float GetValue
        {
            get { return m_Value; }
        }

        public float GetValueRaw
        {
            get { return m_Value; }
        }
    }
}