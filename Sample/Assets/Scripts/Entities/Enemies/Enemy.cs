using Entities.Enemies.Data;
using Entities.Enemies.Systems;
using System;
using System.Data.SqlTypes;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.CullingGroup;

namespace Entities.Enemies
{
    public class Enemy : MonoBehaviour
    {
        public event Action<Enemy> Died;

        [SerializeField] private EnemyAttack m_attack;
        [SerializeField] private HealthComponent m_health;

        private EnemyData m_data;
        private EnemyStateMachine m_stateMachine;
        private Transform m_playerTransform;

        // TODO Add Health
        // TODO Add Movement
        // TODO Add AttackComponent

        private void Awake()
        {
            m_stateMachine ??= new EnemyStateMachine();
        }

        private void OnEnable()
        {
            m_health.Died += OnDied;
            m_stateMachine.StateChanged += OnStateChanged;
        }

        private void OnDisable()
        {
            m_health.Died -= OnDied;
            m_stateMachine.StateChanged += OnStateChanged;
        }

        private void Update()
        {
            if (m_stateMachine.currentState is EnemyState.Dead || !m_data)
            {
                return;
            }

            UpdateState();
        }

        public void Initialize(EnemyData data, Transform playerTransform)
        {
            m_data = data;
            m_health.Initialize(data.health);
            m_attack.Initialize(playerTransform, data.spell, data.attackTime);

            m_playerTransform = playerTransform;
            m_stateMachine ??= new EnemyStateMachine();
        }

        private void UpdateState()
        {
            var isInAttackRange = IsInRange();

            switch (m_stateMachine.currentState)
            {
                case EnemyState.Idle: HandleIdleState(isInAttackRange); break;
                // case EnemyState.Move: HandleMoveState(isInAttackRange); break;
                case EnemyState.Attack: HandleAttackState(isInAttackRange); break;
            }
        }

        private void HandleIdleState(bool isInAttackRange)
        {
            if (m_data.enemyType == AttackEnemyType.Range && isInAttackRange)
            {
                m_stateMachine.ChangeState(EnemyState.Attack);
            }
        }

        private void HandleAttackState(bool isInAttackRange)
        {
            m_attack.TryAttack();

            if (!isInAttackRange)
            {
                if (m_data.enemyType == AttackEnemyType.Melee)
                {
                    m_stateMachine.ChangeState(EnemyState.Move);
                }
                else
                {
                    m_stateMachine.ChangeState(EnemyState.Idle);
                }
            }
        }

        private bool IsInRange()
        {
            if (!m_playerTransform)
            {
                return false;
            }

            var distance = Vector3.Distance(transform.position, m_playerTransform.position);
            return distance <= m_data.attackRange;
        }

        private void OnDied() =>
            Died?.Invoke(this);

        private void OnStateChanged(EnemyState state1, EnemyState state2)
        {
            // TODO Add movement
        }
    }
}