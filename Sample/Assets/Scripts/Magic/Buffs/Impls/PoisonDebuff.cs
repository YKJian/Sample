using Entities;
using Magic.Buffs.Base;
using System;
using UnityEngine;

namespace Magic.Buffs.Impls
{
    [Serializable]
    public sealed class PoisonDebuff : TimedBuff
    {
        [SerializeField] [Min(0)] private float m_interval = 1f;
        [SerializeField] [Min(0)] private float m_damagePerSeconds = 2f;

        [NonSerialized] private float m_timer;
        private IHealth m_health;

        public PoisonDebuff(
            string id,
            Sprite icon,
            BuffType type,
            float duration,
            float interval,
            float damagePerSeconds)
            : base (id, icon, type, duration)
        {
            m_interval = interval;
            m_damagePerSeconds = damagePerSeconds;
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            m_health = container.GetComponent<IHealth>();
        }

        protected override void OnDeinitializing()
        {
            m_timer = 0;
            m_health = null;
            base.OnDeinitializing();
        }

        protected override void OnUpdated(float deltaTime)
        {
            if (m_health == null)
            {
                Deinitialize();
                return;
            }

            if (m_timer < m_interval)
            {
                m_timer += deltaTime;
            }
            else
            {
                m_timer = 0;
                m_health.TakeDamage(m_damagePerSeconds);
            }
        }

        public override IBuff Clone() =>
            new PoisonDebuff(id, icon, type, duration, m_interval, m_damagePerSeconds);
    }
}