using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace YouYouFramework
{
    //һ����������Ϸ����(���硣һ������GUI��ť)Ӧ�õ���������'pressed'������Ȼ������������Զ�ȡ
    //�ð�ť��Get/Down/Up״̬
    public class VirtualButton
    {
        public InputKeyCode Name { get; private set; }
        public bool MatchWithInputManager { get; private set; }

        private int m_LastPressedFrame = -5;
        private int m_ReleasedFrame = -5;
        private bool m_Pressed;

        public VirtualButton(InputKeyCode name) : this(name, true)
        {
        }
        public VirtualButton(InputKeyCode name, bool matchToInputSettings)
        {
            this.Name = name;
            MatchWithInputManager = matchToInputSettings;
        }

        /// <summary>
        /// ����
        /// </summary>
        public void Pressed()
        {
            if (m_Pressed)
            {
                return;
            }
            m_Pressed = true;
            m_LastPressedFrame = Time.frameCount;
        }

        /// <summary>
        /// �ɿ�
        /// </summary>
        public void Released()
        {
            m_Pressed = false;
            m_ReleasedFrame = Time.frameCount;
        }

        // ��Щ�ǰ�ť��״̬������ͨ����ƽ̨����ϵͳ��ȡ
        public bool GetButton
        {
            get { return m_Pressed; }
        }

        public bool GetButtonDown
        {
            get
            {
                return m_LastPressedFrame - Time.frameCount == -1;
            }
        }

        public bool GetButtonUp
        {
            get
            {
                return (m_ReleasedFrame == Time.frameCount - 1);
            }
        }
    }
}