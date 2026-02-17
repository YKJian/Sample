using Entities.Enemies.Data;
using Magic.Spells.Data;
using Magic.Systems;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Entities.Enemies.Systems
{
    public sealed class EnemyAttack : MonoBehaviour
    {
        private Transform m_target;
        private SpellCaster m_spellCaster;
        private BaseSpellData m_defaultSpell;
        private IReadOnlyList<SpellEnemyData> m_spells;

        private float m_attackTime;
        private bool m_isInitialized;
        private float m_cooldownTimer;

        private int m_maxCount;
        private int m_count;

        public void Initialize(
            BaseSpellData defaultSpell,
            IReadOnlyList<SpellEnemyData> spells, 
            Transform target, 
            float attackTime)
        {
            if (m_isInitialized) 
            {
                return; 
            }

            m_target = target;
            m_attackTime = attackTime;
            m_defaultSpell = defaultSpell;
            m_spellCaster = new SpellCaster(transform, true);
            m_spells = spells.OrderBy(spell => spell.count).ToArray();

            m_maxCount = spells.LastOrDefault().count;

            m_isInitialized = true;
        }

        private void Update()
        {
            if (!m_isInitialized)
            {
                return;
            }

            if (m_cooldownTimer > 0)
            {
                m_cooldownTimer -= Time.deltaTime;
            }
        }

        public bool TryAttack()
        {
            if (!m_isInitialized || !m_target)
            {
                return false;
            }

            if (m_cooldownTimer > 0)
            {
                return false;
            }

            m_count++;
            var spell = m_spells.FirstOrDefault(spell => spell.count == m_count);

            if (spell.spell is null)
            {
                m_spellCaster.Cast(m_defaultSpell, m_target.position);
            }
            else
            {
                m_spellCaster.Cast(spell.spell, m_target.position);
            }

            if (m_count == m_maxCount)
            {
                m_count = 0;
            }

            m_cooldownTimer = m_attackTime;

            return true;
        }


    }
}