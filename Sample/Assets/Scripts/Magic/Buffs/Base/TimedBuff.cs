using System;
using UnityEngine;

namespace Magic.Buffs.Base
{
    [Serializable]
    public abstract class TimedBuff : BaseBuff, ITimedBuff
    {
        [SerializeField] private float m_duration;

        public float duration => m_duration;

        [field: NonSerialized]
        public float timer { get; private set; } 

        public TimedBuff() { }

        protected TimedBuff(string id, Sprite icon, BuffType type, float duration)
            : base(id, icon, type)
        {
            m_duration = duration;
        }

        protected override void OnInitialized()
        {
            timer = m_duration;
            base.OnInitialized();
        }

        protected override void OnDeinitializing() =>
            timer = 0;

        public sealed override void Update(float deltaTime)
        {
            if (timer > 0)
            {
                OnUpdated(deltaTime);
                timer -= deltaTime;
            }
            else
            {
                Deinitialize();
            }
        }

        protected virtual void OnUpdated(float deltaTime) { }
    }
}