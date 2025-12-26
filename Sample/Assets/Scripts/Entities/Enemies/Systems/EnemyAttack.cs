using Magic.Spells.Data;
using Magic.Systems;
using UnityEngine;

namespace Entities.Enemies.Systems
{
    public sealed class EnemyAttack : MonoBehaviour
    {
        private Transform m_target;
        private BaseSpellData m_spell;
        private SpellCaster m_spellCaster;

        private float m_attackTime;
        private float m_cooldownTimer;

        private bool m_isInitialized;

        public void Initialize(Transform target, BaseSpellData spell, float attackTime)
        {
            if (m_isInitialized) 
            {
                return; 
            }

            m_spell = spell;
            m_target = target;
            m_attackTime = attackTime;
            m_spellCaster = new SpellCaster(transform);

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

            m_spellCaster.Cast(m_spell, m_target.position);
            m_cooldownTimer = m_attackTime;

            return true;
        }
    }
}