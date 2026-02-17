using Entities.Enemies;
using Players;
using System;
using System.Collections.Generic;
using UI;
using UnityEngine;

namespace Infrastructure.States
{
    public class StateMachine : MonoBehaviour
    {
        private IState m_state;
        private Dictionary<Type, IState> m_states = new();

        public void Initialize(params IState[] states)
        {
            if (m_states.Count > 0) return;

            foreach(var state in states)
            {
                m_states.Add(state.GetType(), state);
            }
        }

        public void ChangeState<T>()
            where T: IState
        {
            m_state?.Exit();
            {
                m_state = m_states[typeof(T)];
            }
            m_state.Enter();
        }
    }

    public interface IState
    {
        public void Enter();

        public void Exit();
    }

    public class MainMenuState: IState
    {
        private readonly StateMachine m_stateMachine;
        private readonly MainMenuView m_mainMenuView;

        public MainMenuState(StateMachine stateMachine, MainMenuView mainMenuView)
        {
            m_stateMachine = stateMachine;
            m_mainMenuView = mainMenuView;

            m_mainMenuView.gameObject.SetActive(false);
        }

        public void Enter()
        {
            m_mainMenuView.PlayClicked += OnPlayClicked;
            m_mainMenuView.ExitClicked += OnExitClicked;
            m_mainMenuView.gameObject.SetActive(true);
        }

        public void Exit()
        {
            m_mainMenuView.PlayClicked -= OnPlayClicked;
            m_mainMenuView.ExitClicked -= OnExitClicked;
            m_mainMenuView.gameObject.SetActive(false);
        }

        private void OnPlayClicked()
        {
            m_stateMachine.ChangeState<GameplayState>();
        }

        private void OnExitClicked()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.ExitPlaymode();
#endif
            Application.Quit();
        }
    }

    public class PauseMenuState : IState
    {
        private readonly StateMachine m_stateMachine;

        public PauseMenuState(StateMachine stateMachine)
        {
            m_stateMachine = stateMachine;
        }

        public void Enter() => throw new Exception();

        public void Exit() => throw new Exception();
    }

    public class GameplayState : IState
    {
        private readonly StateMachine m_stateMachine;
        private readonly EnemySpawner m_enemySpawner;
        private readonly PlayerController m_playerController;

        public GameplayState(
            StateMachine stateMachine,
            EnemySpawner enemySpawner,
            PlayerController playerController)
        {
            m_stateMachine = stateMachine;
            m_enemySpawner = enemySpawner;
            m_playerController = playerController;
        }
        public void Enter()
        {
            m_enemySpawner.Spawn();
            m_playerController.health.ValueChanged += OnDied;
        }

        public void Exit()
        {
            m_playerController.health.ValueChanged -= OnDied;
        }

        private void OnDied()
        {
            m_stateMachine.ChangeState<DeadState>();
        }
    }

    public class DeadState : IState
    {
        private readonly StateMachine m_stateMachine;
        private readonly DeadMenuView m_deadMenuView;

        public DeadState(StateMachine stateMachine, DeadMenuView deadMenuView)
        {
            m_stateMachine = stateMachine;
            m_deadMenuView = deadMenuView;

            deadMenuView.gameObject.SetActive(false);
        }
        public void Enter()
        {
            m_deadMenuView.GoToMenuClicked += OnGoToMenuClicked;
            m_deadMenuView.gameObject.SetActive(true);
        }

        public void Exit()
        {
            m_deadMenuView.GoToMenuClicked -= OnGoToMenuClicked;
            m_deadMenuView.gameObject.SetActive(false);
        }

        private void OnGoToMenuClicked()
        {
            m_stateMachine.ChangeState<MainMenuState>();
        }
    }
}