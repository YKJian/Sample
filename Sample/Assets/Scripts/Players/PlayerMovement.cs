using System;
using UnityEngine;
using UnityEngine.AI;
using Entities;

namespace Players
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class PlayerMovement : MonoBehaviour, IAcceleration
    {
        public event Action Stopped;
        public event Action<Vector3> DestinationChanged;

        [SerializeField] private NavMeshAgent m_agent;

        private float m_speed;
        private float m_acceleration;
        private float m_angularSpeed;
        private bool m_hasDestination;

        private void OnValidate()
        {
            if (!m_agent)
            {
                m_agent = GetComponent<NavMeshAgent>();
            }
        }

        private void Awake() =>
            Initialize(m_speed, m_angularSpeed);

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

        public void Initialize(float speed, float angularSpeed)
        {
            m_speed = speed;
            m_angularSpeed = angularSpeed;

            m_agent.speed = speed;
            m_agent.angularSpeed = angularSpeed;

            m_agent.updateRotation = false;
        }

        public void IncreaseAcceleration(float delta)
        {
            if (delta < 0)
            {
                throw new ArgumentException("Delta cannot be negative", nameof(delta));
            }
            m_acceleration += delta;
            SetSpeed();
        }

        public void DecreaseAcceleration(float delta)
        {
            if (delta < 0)
            {
                throw new ArgumentException("Delta cannot be negative", nameof(delta));
            }
            m_acceleration -= delta;
            SetSpeed();
        }

        public void SetDestination(Vector3 navMeshPoint)
        {
            m_agent.SetDestination(navMeshPoint);

            m_hasDestination = true;

            DestinationChanged?.Invoke(navMeshPoint);
        }

        public void RotateTowards(Vector3 worldPoint)
        {
            var direction = worldPoint - transform.position;
            direction.y = 0;

            if (direction.sqrMagnitude < 0.0001f)
            {
                return;
            }

            var targetRotate = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotate, m_agent.angularSpeed * Time.deltaTime);
        }

        private void SetSpeed()
        {
            var acceleration = m_acceleration > 0
                ? m_acceleration 
                : 1; 
            m_agent.speed = m_speed * acceleration;
        }
    }
}
