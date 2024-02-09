    using UnityEngine.Events;

namespace UnityEngine.XR.Content.Interaction
{
    /// <summary>
    /// Calls events for when the velocity of this objects breaks the begin and end threshold.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class OnVelocity : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("The speed that will trigger the begin event.")]
        float m_BeginThreshold = 1.25f;

        [SerializeField]
        [Tooltip("The speed that will trigger the end event.")]
        float m_EndThreshold = 0.25f;

        [SerializeField]
        [Tooltip("Event that triggers when speed meets the begin threshold.")]
        UnityEvent m_OnBegin = new UnityEvent();

        [SerializeField]
        [Tooltip("Event that triggers when the speed dips below the end threshold.")]
        UnityEvent m_OnEnd = new UnityEvent();

        /// <summary>
        /// Event that triggers when speed meets the begin threshold.
        /// </summary>
        public UnityEvent onBegin => m_OnBegin;

        /// <summary>
        /// Event that triggers when the speed dips below the end threshold.
        /// </summary>
        public UnityEvent onEnd => m_OnEnd;

        Rigidbody m_RigidBody;
        bool m_HasBegun;

        void Awake()
        {
            m_RigidBody = GetComponent<Rigidbody>();
        }

        void Update()
        {
            CheckVelocity();
        }
        /// <summary>
        /// Will activate once the event onBegin once a certain speed (m_BeginThreshold) has been exceeded then will activate the event onEnd
        /// once the speed has been reduced to below m_EndThreshold
        /// </summary>
        void CheckVelocity()
        {
            float speed = m_RigidBody.velocity.magnitude;
            m_HasBegun = HasVelocityBegun(speed);

            if (HasVelocityEnded(speed))
                m_HasBegun = false;
        }
        
        bool HasVelocityBegun(float speed)
        {
            if (m_HasBegun) // while the velocity exceed m_EndThreshold keep the status where the object is fast and managed to pass once the speed of m_BeginThreshold
                return true;

            bool beginCheck = speed > m_BeginThreshold;

            if (beginCheck) // invoke just once the event per exceeding since it will be blocked with the condition on top or will stop with HasVelocityEnded
                m_OnBegin.Invoke();

            return beginCheck;
        }
        
        bool HasVelocityEnded(float speed)
        {
            if (!m_HasBegun) // if the velocity hasnt changed (is not enough) do nothing
                return false;

            bool endCheck = speed < m_EndThreshold;

            if (endCheck) // here the object is supposed to be fast and we check if the speed is still enough to put the status back to false or not
                m_OnEnd.Invoke();

            return endCheck;
        }
    }
}
