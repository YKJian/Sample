using Infrastructure.States;
using UI;
using UnityEngine;

namespace Infrastructure
{
    public class PauseMenuState : IState
    {
        private readonly StateMachine m_stateMachine;
        private readonly PauseMenuView m_pauseMenuView;

        public PauseMenuState(
            StateMachine stateMachine,
            PauseMenuView pauseMenuView)
        {
            m_stateMachine = stateMachine;
            m_pauseMenuView = pauseMenuView;
        }

        public void Enter()
        {
            Time.timeScale = 0f;
            m_pauseMenuView.gameObject.SetActive(true);
            m_pauseMenuView.ContinueClicked += OnContinueClicked;
            m_pauseMenuView.MainMenuClicked += OnMainMenuClicked;
        }

        public void Exit()
        {
            Time.timeScale = 1f;
            m_pauseMenuView.gameObject.SetActive(false);
            m_pauseMenuView.ContinueClicked -= OnContinueClicked;
            m_pauseMenuView.MainMenuClicked -= OnMainMenuClicked;
        }

        private void OnContinueClicked()
        {
            m_stateMachine.ChangeState<GameplayState>();
        }

        private void OnMainMenuClicked()
        {
            m_stateMachine.ChangeState<GameplayExitState>();
        }
    }
}