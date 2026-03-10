using UnityEngine;
using Infrastructure.States;
using Entities.Enemies;
using UI;
using Markers;
using Cameras;

namespace Infrastructure
{
    public class BootstrapGameplay: MonoBehaviour
    {
        [SerializeField] private TargetMarkerObserver m_targetMarkerObserver;
        [SerializeField] private BootstrapState m_bootStrapState;
        [SerializeField] private DeadMenuView m_deadMenuView;
        [SerializeField] private AimLineMarker m_aimLineMarker;
        [SerializeField] private EnemySpawner m_enemySpawner;
        [SerializeField] private CameraFollow m_cameraFollow;
        [SerializeField] private PauseMenuView m_pauseMenuView;

        private StateMachine m_stateMachine;

        private void Awake()
        {
            m_stateMachine = new StateMachine();
            m_bootStrapState.Initialize(m_stateMachine);

            m_stateMachine.Initialize(
                m_bootStrapState,
                new PauseMenuState(m_stateMachine, m_pauseMenuView), 
                new DeadState(m_stateMachine, m_deadMenuView), 
                new GameplayState(m_stateMachine, m_cameraFollow),
                new GameplayExitState(),
                new GameplayEntryState(
                    m_stateMachine,
                    m_enemySpawner, 
                    m_aimLineMarker,
                    m_targetMarkerObserver));

            m_stateMachine.ChangeState<BootstrapState>();
        }
    }
}