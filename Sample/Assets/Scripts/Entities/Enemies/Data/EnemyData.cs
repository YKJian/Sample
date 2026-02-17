using Magic.Spells.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.Enemies.Data
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Xlab/Enemies/Enemy")]
    public class EnemyData : ScriptableObject
    {
        [SerializeField] private AttackEnemyType m_enemyType;

        [Header("Parameters")]
        [SerializeField] [Min(0)] private float m_health;
        [SerializeField] [Range(0f, 100f)] private float m_speed;

        [Header("Attack")]
        [SerializeField] private BaseSpellData m_defaultSpell;
        [SerializeField] private SpellEnemyData[] m_spells;
        [SerializeField] [Min(0)] private float m_attackTime;
        [SerializeField] [Min(0)] private float m_attackRange;
      
        public float health => m_health;
        
        public float speed => m_speed;


        public float attackTime => m_attackTime;

        public float attackRange => m_attackRange;

        public AttackEnemyType enemyType => m_enemyType;

        public BaseSpellData defaultSpell => m_defaultSpell;
        
        public IReadOnlyList<SpellEnemyData> spells => m_spells;
    }

    [Serializable]
    public struct SpellEnemyData
    {
        [SerializeField] public int count;
        [SerializeField] public BaseSpellData spell;
    }
}