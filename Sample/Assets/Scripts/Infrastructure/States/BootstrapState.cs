using Players;
using UnityEngine;

namespace Infrastructure.States
{
    public class BootstrapState : MonoBehaviour, IState
    {
        [SerializeField] private MouseResolver m_mouseResolver;

        private StateMachine m_stateMachine;

        public void Initialize(StateMachine stateMachine)
        {
            m_stateMachine = stateMachine;
        }

        public void Enter()
        {
            ServiceLocator.Register(m_mouseResolver);
            m_stateMachine.ChangeState<GameplayState>();
        }

        public void Exit()
        {

        }
    }
}