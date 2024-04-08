using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Playables;

namespace YouYouFramework
{
    public class PlayResourcePlayable : BasePlayableAsset<PlayResourcePlayableBehaviour, PlayResourceEventArgs>
    {
    }
    public class PlayResourcePlayableBehaviour : BasePlayableBehaviour<PlayResourceEventArgs>
    {
        protected override void OnYouYouBehaviourPlay(Playable playable, FrameData info)
        {
            CurrTimelineCtrl.PlayResource?.Invoke(CurrArgs, (float)End);
        }

        protected override void OnYouYouBehaviourStop(Playable playable, FrameData info)
        {

        }
    }
    [System.Serializable]
    public class PlayResourceEventArgs
    {
#if UNITY_EDITOR
        [OnValueChanged("OnCurrResourceChanged")]
        public GameObject CurrResource;
        private void OnCurrResourceChanged()
        {
            string path = UnityEditor.AssetDatabase.GetAssetPath(CurrResource);
            PrefabFullPath = path;
        }
#endif

        [Header("Ŀ���")]
        public DynamicTarget Target;

        [Header("Ԥ��·��")]
        public string PrefabFullPath;

        [Header("ƫ��")]
        public Vector3 Offset;

        [Header("��ת")]
        public Vector3 Rotation;

        [Header("����")]
        public Vector3 Scale = Vector3.one;

    }
}