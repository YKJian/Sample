using System;
using UnityEngine;

namespace Magic.Buffs.Base
{
    [Serializable]
    public abstract class TimedBuff : BaseBuff
    {
        [SerializeField] private float m_duration;

        [NonSerialized] private float m_timer;

        protected float duration => m_duration;

        public TimedBuff() { }

        protected TimedBuff(string id, float duration)
            : base(id)
        {
            m_duration = duration;
        }

        protected override void OnDeinitializing() =>
            m_timer = 0;

        public sealed override void Update(float deltaTime)
        {
            if (m_timer < m_duration)
            {
                OnUpdated(deltaTime);
                m_timer += deltaTime;
            }
            else
            {
                Deinitialize();
            }
        }

        protected virtual void OnUpdated(float deltaTime) { }
    }
}