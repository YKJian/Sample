using Magic.Effects;
using System.Collections.Generic;
using UnityEngine;

namespace Magic.Spells.Projectiles
{
    [RequireComponent(typeof(Rigidbody))]
    public sealed class SpellProjectile : MonoBehaviour, ISpellProjectile
    {
        [SerializeField] private Rigidbody m_rigidbody;

        private float m_speed;
        private bool m_initialized;
        private Vector3 m_direction;
        private Vector3 m_targetPosition;
        private float m_targetDistance;
        private float m_traveledDistance;
        private IReadOnlyList<IEffect> m_effects;

        private void OnValidate()
        {
            if (!m_rigidbody)
            {
                m_rigidbody = GetComponent<Rigidbody>();
            }
        }

        private void Start()
        {
            m_rigidbody.useGravity = false;

            m_rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        }

        private void FixedUpdate()
        {
            if (!m_initialized) return;

            m_traveledDistance += m_speed * Time.fixedDeltaTime;

            if (m_traveledDistance >= m_targetDistance)
            {
                Destroy(gameObject);
            }
            else
            {
                SetLinearVelocity();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!m_initialized) return;

            if (other.TryGetComponent<IEffectable>(out var effectable))
                ApplyEffects(effectable);

            Destroy(gameObject);
        }

        public void Initialize(Vector3 targetPosition, float speed, IReadOnlyList<IEffect> effects)
        {
            m_targetPosition = targetPosition;
            // m_targetPosition.y = transform.position.y;

            m_speed = speed;
            m_effects = effects;

            m_direction = (m_targetPosition - transform.position).normalized;

            m_traveledDistance = 0f;
            m_targetDistance = Vector3.Distance(transform.position, m_targetPosition);

            if (m_direction !=  Vector3.zero)
                transform.rotation = Quaternion.LookRotation(m_direction);

            m_initialized = true;

            SetLinearVelocity();
        }

        private void ApplyEffects(IEffectable target)
        {
            if (m_effects is null) return;

            foreach (var effect in m_effects)
            {
                effect?.Apply(target);
            }
        }

        private void SetLinearVelocity() => 
            m_rigidbody.linearVelocity = m_direction * m_speed;
    }
}
