using System;
using UnityEngine;
using UnityEngine.AI;

namespace Players
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class PlayerMovement : MonoBehaviour
    {
        public event Action Stopped;
        public event Action<Vector3> DestinationChanged;

        [SerializeField] private NavMeshAgent m_agent;

        private float m_speed;
        private bool m_hasDestination;

        private void OnValidate()
        {
            if (!m_agent)
            {
                m_agent = GetComponent<NavMeshAgent>();
            }
        }

        private void Awake() =>
            Initialize(m_speed);

        private void Update()
        {
            if(!m_hasDestination || m_agent.pathPending)
            {
                return;
            }

            if (m_agent.remainingDistance <= m_agent.stoppingDistance)
            {
                if (!m_agent.hasPath || m_agent.velocity.sqrMagnitude <= 0.001f)
                {
                    m_agent.isStopped = false;
                    m_hasDestination = false;

                    Stopped?.Invoke();
                }
            }
        }

        public void Initialize(float speed)
        {
            m_speed = speed;
            m_agent.speed = speed;
        }

        public void SetDestination(Vector3 navMeshPoint)
        {
            m_agent.SetDestination(navMeshPoint);

            m_hasDestination = true;

            DestinationChanged?.Invoke(navMeshPoint);
        }
    }
}
