using Cameras;
using Entities.Enemies;
using Infrastructure.States;
using Markers;
using Players;
using UnityEngine.InputSystem;

namespace Infrastructure
{
    public class GameplayState : IState
    {
        private readonly StateMachine m_stateMachine;
        private readonly CameraFollow m_cameraFollow;
        private PlayerController m_playerController;

        public GameplayState(
            StateMachine stateMachine,
            CameraFollow cameraFollow)
        {
            m_stateMachine = stateMachine;
            m_cameraFollow = cameraFollow;
        }

        public void Enter()
        {
            m_playerController = ServiceLocator.Resolve<IPlayerFactory>().Create();

            m_cameraFollow.SetTarget(m_playerController.transform);
            m_playerController.health.Died += OnDied;
        }

        public void Update()
        {
            if (Keyboard.current[Key.Escape].wasPressedThisFrame)
            {
                m_stateMachine.ChangeState<PauseMenuState>();
            }
        }

        public void Exit()
        {
            m_playerController.health.Died -= OnDied;
            m_playerController = null;
        }

        private void OnDied()
        {
            m_stateMachine.ChangeState<DeadState>();
        }
    }
}