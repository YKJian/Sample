using Entities.Enemies;

namespace Infrastructure.States
{
    public class GameplayExitState : IState
    {
        private EnemySpawner m_enemySpawner;

        public GameplayExitState(EnemySpawner enemySpawner)
        {
            m_enemySpawner = enemySpawner;
        }

        public void Enter()
        {
            Loading loading = ServiceLocator.Resolve<Loading>();
            m_enemySpawner.DespawnAll();

            loading.LoadScene(GlobalConstants.Scenes.Main);
        }

        public void Exit()
        {

        }
    }
}