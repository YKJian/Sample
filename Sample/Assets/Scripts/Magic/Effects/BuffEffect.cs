using Magic.Buffs;
using System;
using UnityEngine;

namespace Magic.Effects
{
    [Serializable]
    public class BuffEffect : IEffect
    {
        [SerializeReferenceDropdown]
        [SerializeReference] private IBuff[] m_buffs;

        public void Apply(IEffectable effectable)
        {
            if (effectable is BuffContainer container)
            {
                foreach (var buff in m_buffs)
                {
                    container.Add(buff.Clone());
                }
            }
        }
    }
}