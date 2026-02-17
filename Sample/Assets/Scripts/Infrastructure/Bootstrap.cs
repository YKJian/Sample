using UnityEngine;
using Infrastructure.States;
using Entities.Enemies;
using UI;

namespace Infrastructure
{
    public class Bootstrap: MonoBehaviour
    {
        [SerializeField] private MainMenuView m_mainMenuView;
        [SerializeField] private EnemySpawner m_enemySpawner;

        private void Awake()
        {
            var stateMachine = new StateMachine();

            stateMachine.Initialize(
                new MainMenuState(stateMachine, m_mainMenuView),
                new PauseMenuState(stateMachine), 
                new DeadState(stateMachine), 
                new GameplayState(stateMachine, m_enemySpawner));

            stateMachine.ChangeState<MainMenuState>();
        }
    }
}