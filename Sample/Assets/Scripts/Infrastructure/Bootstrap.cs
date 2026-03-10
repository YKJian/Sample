using UnityEngine;

namespace Infrastructure
{
    [DefaultExecutionOrder(-500)]
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private Loading m_loading;

        private void Awake()
        {
            ServiceLocator.Clear();
            ServiceLocator.Register(m_loading);
        }
    }
}