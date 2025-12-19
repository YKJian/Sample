using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace Players
{
    public class MouseResolver : MonoBehaviour
    {
        [SerializeField] private LayerMask m_layerMask = ~0;
        [SerializeField][Min(0)] private float m_raycastDistance = 1000f;
        [SerializeField][Min(0)] private float m_navMeshSampleMaxDistance = 100f;

        private Mouse m_mouse;
        private Camera m_camera;

        public Vector3 m_mousePosition => m_mouse.position.ReadValue();

        public void Awake()
        {
            m_mouse = Mouse.current;
            m_camera = Camera.main;
        }

        public Vector3? GetNavMeshPoint()
        {
            var ray = m_camera.ScreenPointToRay(m_mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, m_raycastDistance, m_layerMask))
            {
                if (NavMesh.SamplePosition(hit.point, out var navHit, m_navMeshSampleMaxDistance, NavMesh.AllAreas))
                {
                    return navHit.position;
                }
            }

            return null;
        }

        public Vector3? GetCursorWorldPosition()
        {
            var ray = m_camera.ScreenPointToRay(m_mousePosition);

            if (Physics.Raycast(ray, out var hit))
            {
                return hit.point;
            }

            var plane = new Plane(inNormal: Vector3.up, inPoint: Vector3.zero);

            if (plane.Raycast(ray, out var distance))
            {
                return ray.GetPoint(distance);
            }

            return null;
        }
    }
}