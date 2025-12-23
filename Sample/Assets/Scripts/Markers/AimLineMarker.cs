using Players;
using UnityEngine;

namespace Markers
{
    [RequireComponent (typeof (LineRenderer))]
    public class AimLineMarker : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Transform m_playerTransform;
        [SerializeField] private LineRenderer m_lineRenderer;
        [SerializeField] private MouseResolver m_mouseResolver;

        [Header("Settings")]
        [SerializeField] [Min(0)] private float m_z0Offset = 0.5f;
        [SerializeField] [Min(0)] private float m_lineWidth = 0.1f;
        [SerializeField] [Min(0)] private float m_disableDistance = 1f;

        private void OnValidate()
        {
            if (!m_lineRenderer)
            {
                m_lineRenderer = GetComponent<LineRenderer>();
            }
        }

        private void Awake()
        {
            m_lineRenderer.positionCount = 2;
            m_lineRenderer.startWidth = m_lineWidth;
            m_lineRenderer.endWidth = m_lineWidth;
        }

        private void LateUpdate()
        {
            // m_lineRenderer.SetPosition(index: 0, start);
        }

        private Vector3 GetAimPosition()
        {
            var worldPosition = m_mouseResolver.GetCursorWorldPosition();

            if (worldPosition.HasValue)
            {
                return worldPosition.Value;
            }

            return m_playerTransform.position + m_playerTransform.forward;
        }
    }
}