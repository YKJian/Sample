using System;
using UnityEngine;
using Entities;

namespace Magic.Effects
{
    [Serializable]
    public sealed class HealEffect : IEffect
    {
        [SerializeField][Min(0)] private float m_heal;

        public void Apply(IEffectable effectable)
        {
            if (effectable is IHealth health)
            {
                health.Heal(m_heal);
            }
        }
    }
}
