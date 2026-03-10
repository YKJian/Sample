using Entities.Enemies;
using Markers;
using Players;

namespace Infrastructure.States
{
    public class GameplayEntryState : IState
    {
        private PlayerController m_playerController;

        private StateMachine m_stateMachine;
        private EnemySpawner m_enemySpawner;
        private AimLineMarker m_aimLineMarker;
        private TargetMarkerObserver m_targetMarkerObserver;

        public GameplayEntryState(
            StateMachine stateMachine,
            EnemySpawner enemySpawner,
            AimLineMarker aimLineMarker,
            TargetMarkerObserver targetMarkerObserver)
        {
            m_stateMachine = stateMachine;
            m_enemySpawner = enemySpawner;
            m_aimLineMarker = aimLineMarker;
            m_targetMarkerObserver = targetMarkerObserver;
        }

        public void Enter()
        {
            var playerPosition = ServiceLocator.Resolve<PlayerSpawnPoint>();
            ServiceLocator.Resolve<IPlayerFactorySettings>().position = playerPosition.transform.position;
            m_playerController = ServiceLocator.Resolve<IPlayerFactory>().Create().GetComponent<PlayerController>();

            m_aimLineMarker.Initialize(m_playerController.transform);
            m_targetMarkerObserver.Initialize(m_playerController.GetComponent<PlayerMovement>());

            m_enemySpawner.Spawn();
            m_stateMachine.ChangeState<GameplayEntryState>();
        }

        public void Exit()
        {
            
        }
    }
}