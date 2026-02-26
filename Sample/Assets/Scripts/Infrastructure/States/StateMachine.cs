using Cameras;
using Entities.Enemies;
using Markers;
using Players;
using System;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        private readonly CameraFollow m_cameraFollow;
        private readonly EnemySpawner m_enemySpawner;
        private readonly AimLineMarker m_aimLineMarker;
        private readonly TargetMarkerObserver m_targetMarkerObserver;
        
        private PlayerController m_playerController;

        public GameplayState(
            StateMachine stateMachine,
            CameraFollow cameraFollow,
            EnemySpawner enemySpawner,
            AimLineMarker aimLineMarker,
            TargetMarkerObserver targetMarkerObserver)
        {
            m_stateMachine = stateMachine;
            m_cameraFollow = cameraFollow;
            m_enemySpawner = enemySpawner;
            m_aimLineMarker = aimLineMarker;
            m_targetMarkerObserver = targetMarkerObserver;
        }

        public void Enter()
        {
            var playerPosition = ServiceLocator.Resolve<PlayerSpawnPoint>();
            ServiceLocator.Resolve<IPlayerFactorySettings>().position = playerPosition.transform.position;
            m_playerController = ServiceLocator.Resolve<IPlayerFactory>().Create().GetComponent<PlayerController>();

            m_cameraFollow.SetTarget(m_playerController.transform);
            m_aimLineMarker.Initialize(m_playerController.transform);
            m_targetMarkerObserver.Initialize(m_playerController.GetComponent<PlayerMovement>());

            m_enemySpawner.Spawn();
            m_playerController.health.Died += OnDied;
        }

        public void Exit()
        {
            m_playerController.health.Died -= OnDied;
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
            SceneManager.LoadScene(GlobalConstants.Scenes.Main);
        }
    }
}