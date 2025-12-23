using UnityEngine;

namespace Entities.Enemies.Data
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Xlab/Enemies/Enemy")]
    public class EnemyData : ScriptableObject
    {
        // check that
        // we refer to generated field - <Speed>K_Background...
        // [field: SerializeField]
        // 
        [SerializeField] private AttackEnemyType m_enemyType;

        [Header("Parameters")]
        [SerializeField] [Min(0)] private float m_health;
        [SerializeField] [Range(0f, 100f)] private float m_speed;

        [Header("Attack")]
        [SerializeField] [Min(0)] private float m_attackTime;
        [SerializeField] [Min(0)] private float m_attackRange;

        // TODO Add ProjectileRange
        // TODO Add Damage

        public AttackEnemyType enemyType => m_enemyType;
        public float health => m_health;
        public float speed => m_speed;
        public float attackTime => m_attackTime;
        public float attackRange => m_attackRange;
    }
}