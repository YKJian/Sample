using System;
using UnityEngine;

namespace Magic.Effects
{
    [Serializable]
    public class AttackEffect : IEffect
    {
        [SerializeField][Min(0)] private float m_damage;

        public void Apply(IEffectable effectable)
        {
            // Do something
        }
    }
}
