using Magic.Buffs.Extensions;
using Magic.Effects;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Magic.Buffs
{
    public sealed class BuffContainer : MonoBehaviour, IEffectable
    {
        public event Action<IBuff> BuffAdded;
        public event Action<IBuff> BuffRemoved;

        private HashSet<string> m_ids = new();
        private Dictionary<string, IBuff> m_buffs = new();

        public IReadOnlyCollection<IBuff> Buffs => m_buffs.Values;

        public void Add(IBuff buff)
        {
            if (m_buffs.TryGetValue(buff.id, out IBuff existingBuff))
            {
                existingBuff.Refresh(this);
                m_ids.Remove(existingBuff.id);
            }
            else
            {
                m_buffs.Add(buff.id, buff);
                buff.Initialize(this);

                BuffAdded?.Invoke(buff);
            }
        }

        public void Remove(IBuff buff)
        {
            m_ids.Add(buff.id);
        }

        public void Update()
        {
            foreach (var buff in m_buffs.Values)
            {
                buff.Update(Time.deltaTime);
            } 

            foreach (var id in m_ids)
            {
                var buff = m_buffs[id];

                m_buffs.Remove(id);
                BuffRemoved(buff);
            }

            m_ids.Clear();
        }
    }
}