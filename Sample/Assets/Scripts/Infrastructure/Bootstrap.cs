using UnityEngine;
using Infrastructure.States;
using Entities.Enemies;
using UI;
using Players;

namespace Infrastructure
{
    public class Bootstrap: MonoBehaviour
    {
        [SerializeField] private BootstrapState m_bootStrapState;
        [SerializeField] private DeadMenuView m_deadMenuView;
        [SerializeField] private EnemySpawner m_enemySpawner;
        [SerializeField] private PlayerController m_playerController;

        private void Awake()
        {
            var stateMachine = new StateMachine();
            m_bootStrapState.Initialize(stateMachine);

            stateMachine.Initialize(
                m_bootStrapState,
                new PauseMenuState(stateMachine), 
                new DeadState(stateMachine, m_deadMenuView), 
                new GameplayState(stateMachine, m_enemySpawner, m_playerController));

            stateMachine.ChangeState<BootstrapState>();
        }
    }
}