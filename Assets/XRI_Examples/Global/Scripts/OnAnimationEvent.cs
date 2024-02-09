using System;
using UnityEngine.Events;

namespace UnityEngine.XR.Content.Animation
{
    /// <summary>
    /// Will receive triggers from the <see cref="AnimationEventActionBegin"/> and <see cref="AnimationEventActionFinished"/> classes,
    /// and forward them to Unity Events.
    /// </summary>
    public class OnAnimationEvent : MonoBehaviour, IAnimationEventActionBegin, IAnimationEventActionFinished
    {
        [Serializable]
        struct ActionEvent
        {
            public string m_Label;
            public UnityEvent m_Action;
        }

        [SerializeField]
        ActionEvent[] m_ActionBeginEvents;

        [SerializeField]
        ActionEvent[] m_ActionEndEvents;

        /// <inheritdoc />
        public void ActionBegin(string label)
        {
            Debug.Log("ActionBegin");
            if (m_ActionBeginEvents == null)
                return;

            foreach (ActionEvent currentAction in m_ActionBeginEvents)
            {
                if (currentAction.m_Label == label)
                    currentAction.m_Action.Invoke();
            }
        }

        /// <inheritdoc />
        public void ActionFinished(string label)
        {
            Debug.Log("ActionFinished");
            if (m_ActionEndEvents == null)
                return;

            foreach (ActionEvent currentAction in m_ActionEndEvents)
            {
                if (currentAction.m_Label == label)
                    currentAction.m_Action.Invoke();
            }
        }
    }
}
