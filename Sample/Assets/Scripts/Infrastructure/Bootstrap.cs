using UnityEngine;
using Infrastructure.States;
using Entities.Enemies;
using UI;
using Players;
using Markers;
using Cameras;

namespace Infrastructure
{
    public class Bootstrap: MonoBehaviour
    {
        [SerializeField] private TargetMarkerObserver m_targetMarkerObserver;
        [SerializeField] private BootstrapState m_bootStrapState;
        [SerializeField] private DeadMenuView m_deadMenuView;
        [SerializeField] private AimLineMarker m_aimLineMarker;
        [SerializeField] private EnemySpawner m_enemySpawner;
        [SerializeField] private CameraFollow m_cameraFollow;

        private void Awake()
        {
            var stateMachine = new StateMachine();
            m_bootStrapState.Initialize(stateMachine);

            stateMachine.Initialize(
                m_bootStrapState,
                new PauseMenuState(stateMachine), 
                new DeadState(stateMachine, m_deadMenuView), 
                new GameplayState(
                    stateMachine,
                    m_cameraFollow,
                    m_enemySpawner, 
                    m_aimLineMarker,
                    m_targetMarkerObserver));

            stateMachine.ChangeState<BootstrapState>();
        }
    }
}