using UnityEngine;

namespace Utils
{
    public class FixedRotation : MonoBehaviour
    {
        private Transform m_parent;
        private Vector3 m_worldOffset;
        private Quaternion m_rotation;

        private void Start()
        {
            m_parent = transform.parent;

            m_rotation = transform.rotation;
            m_worldOffset = transform.position - m_parent.position;
        }

        // after all the logic, we update graphics
        private void LateUpdate()
        {
            if (!m_parent)
            {
                return;
            }

            transform.position = m_parent.position + m_worldOffset;
            transform.rotation = m_rotation;
        }
    }
}

