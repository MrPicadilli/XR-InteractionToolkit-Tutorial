using UnityEngine.Events;

namespace UnityEngine.XR.Content.Interaction
{
    /// <summary>
    /// Runs functionality when an object is tilted.
    /// Used with grabbable objects for pouring.
    /// </summary>
    public class OnTilt : MonoBehaviour
    {
        /// <summary>
        /// Extra angle value that is added/removed from the threshold to events from rapid-fire triggering on and off.
        /// </summary>
        const float k_AngleBuffer = 0.05f;

        [SerializeField]
        [Tooltip("Tilt range, 0 - 180 degrees.")]
        [Range(k_AngleBuffer * 2f, (1 - k_AngleBuffer * 2f))]
        float m_Threshold = 0.5f;

        [SerializeField]
        [Tooltip("The transform to check for tilt. Will default to this object if not set.")]
        Transform m_Target;

        [SerializeField]
        [Tooltip("The transform to get as the source of the 'up' direction. Will default to world up if not set.")]
        Transform m_UpSource;

        [SerializeField]
        [Tooltip("Event to trigger when tilting goes over the threshold.")]
        UnityEvent m_OnBegin = new UnityEvent();

        [SerializeField]
        [Tooltip("Event to trigger when tilting returns from the threshold.")]
        UnityEvent m_OnEnd = new UnityEvent();

        /// <summary>
        /// Event to trigger when tilting goes over the threshold.
        /// </summary>
        public UnityEvent onBegin => m_OnBegin;

        /// <summary>
        /// Event to trigger when tilting returns from the threshold.
        /// </summary>
        public UnityEvent onEnd => m_OnEnd;

        bool m_WithinThreshold;

        void Update()
        {
            CheckOrientation();
        }

        void CheckOrientation()
        {
            Vector3 targetUp = m_Target != null ? m_Target.up : transform.up;
            Vector3 baseUp = m_UpSource != null ? m_UpSource.up : Vector3.up;
            //Debug.Log("targetUp " + targetUp + "magnitude" + targetUp.magnitude);
            //Debug.Log("baseUp " + baseUp + "magnitude" + baseUp.magnitude);
            float similarity = Vector3.Dot(-targetUp, baseUp);
            //Debug.Log("Dot similarity " + similarity);
            similarity = Mathf.InverseLerp(-1, 1, similarity);
            //Debug.Log("InverseLerp similarity " + similarity);
            if (m_WithinThreshold)
                similarity += k_AngleBuffer;
            else
                similarity -= k_AngleBuffer;

            bool thresholdCheck = (similarity >= m_Threshold);

            if (m_WithinThreshold != thresholdCheck)
            {
                m_WithinThreshold = thresholdCheck;

                if (m_WithinThreshold)
                {
                    m_OnBegin.Invoke();
                }
                else
                {
                    m_OnEnd.Invoke();
                }
            }
        }
    }
}
