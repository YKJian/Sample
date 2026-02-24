using Players;
using UnityEngine;

namespace Infrastructure.States
{
    public class BootstrapState : MonoBehaviour, IState
    {
        [SerializeField] private MouseResolver m_mouseResolver;
        [SerializeField] private PlayerSpawnPoint m_playerSpawnPoint;

        private StateMachine m_stateMachine;

        public void Initialize(StateMachine stateMachine)
        {
            m_stateMachine = stateMachine;
        }

        public void Enter()
        {
            ServiceLocator.Register(m_mouseResolver);

            var playerFactory = new PlayerFactory(GlobalConstants.Paths.PlayerPrefab);
            ServiceLocator.Register<IPlayerFactory>(playerFactory);
            ServiceLocator.Register<IPlayerFactorySettings>(playerFactory);

            ServiceLocator.Register<PlayerSpawnPoint>(m_playerSpawnPoint);

            m_stateMachine.ChangeState<GameplayState>();
        }

        public void Exit()
        {

        }
    }
}