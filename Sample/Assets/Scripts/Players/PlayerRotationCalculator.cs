using UnityEngine;

namespace Players
{
    public sealed class PlayerRotationCalculator
    {
        private readonly Camera m_camera;
        private readonly Transform m_playerTransform;

        public PlayerRotationCalculator(Camera camera, Transform playerTransform)
        {
            m_camera = camera;
            m_playerTransform = playerTransform;
        }

        public Vector3 Calculate(Vector3 mousePosition)
        {
            var playerScreenPosition = m_camera.WorldToScreenPoint(m_playerTransform.position);
            var delta = (Vector2)(mousePosition - playerScreenPosition);

            var cameraRight = m_camera.transform.right;
            cameraRight.y = 0f;
            cameraRight.Normalize();

            var cameraForward = m_camera.transform.forward;
            cameraForward.y = 0f;
            cameraForward.Normalize();

            var worldDirection = cameraRight * delta.x + cameraForward * delta.y;
            worldDirection.y = 0f;

            if (worldDirection.sqrMagnitude > 0.0001f)
            {
                return m_playerTransform.position + worldDirection;
            }
            else
            {
                return Vector3.zero;
            }
        }
    }
}
