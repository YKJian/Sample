using Entities.Enemies;

namespace Infrastructure.States
{
    public class GameplayExitState : IState
    {
        public void Enter()
        {
            Loading loading = ServiceLocator.Resolve<Loading>();
            EnemySpawner spawner = ServiceLocator.Resolve<EnemySpawner>();
            spawner.DespawnAll();

            loading.LoadScene(GlobalConstants.Scenes.Main);
        }

        public void Exit()
        {

        }
    }
}