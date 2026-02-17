using UnityEngine;
using Infrastructure.States;
using Entities.Enemies;
using UI;
using Players;

namespace Infrastructure
{
    public class Bootstrap: MonoBehaviour
    {
        [SerializeField] private MainMenuView m_mainMenuView;
        [SerializeField] private DeadMenuView m_deadMenuView;
        [SerializeField] private EnemySpawner m_enemySpawner;
        [SerializeField] private PlayerController m_playerController;

        private void Awake()
        {
            var stateMachine = new StateMachine();

            stateMachine.Initialize(
                new MainMenuState(stateMachine, m_mainMenuView),
                new PauseMenuState(stateMachine), 
                new DeadState(stateMachine, m_deadMenuView), 
                new GameplayState(stateMachine, m_enemySpawner, m_playerController));

            stateMachine.ChangeState<MainMenuState>();
        }
    }
}