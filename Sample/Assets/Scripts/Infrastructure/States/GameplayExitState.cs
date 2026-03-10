using Entities.Enemies;

namespace Infrastructure.States
{
    public class GameplayExitState : IState
    {
        public void Enter()
        {
            var loading = ServiceLocator.Resolve<Loading>();
            var spawner = ServiceLocator.Resolve<EnemySpawner>();
            spawner.DespawnAll();

            loading.LoadScene(GlobalConstants.Scenes.Main);
        }

        public void Exit()
        {

        }
    }
}