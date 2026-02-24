using Players;
using UnityEngine;

namespace Infrastructure
{
    public interface IPlayerFactorySettings
    {
        public Vector3 position {  get; set; }
    }

    public interface IPlayerFactory
    {
        public PlayerController Create();

        public void Release();
    }

    public class PlayerFactory : IPlayerFactory, IPlayerFactorySettings
    {
        private readonly string m_path;
        private PlayerController m_playerPrefab;
        private PlayerController m_playerInstance;

        public Vector3 position { get; set; }

        public PlayerFactory(string path)
        {
            m_path = path;
        }

        public PlayerController Create()
        {
            if (m_playerInstance != null)
            {
                return m_playerInstance;
            }

            if (m_playerPrefab == null)
            {
                var playerPrefab = Resources.Load<GameObject>(m_path);
                m_playerPrefab = playerPrefab.GetComponent<PlayerController>(); 
            }

            m_playerInstance = Object.Instantiate(m_playerPrefab, 
                ((IPlayerFactorySettings)this).position, 
                Quaternion.identity);
            m_playerInstance.Initialize(Camera.main, ServiceLocator.Resolve<MouseResolver>());

            return m_playerInstance;
        }

        public void Release()
        {
            Object.Destroy(m_playerInstance.gameObject);
            m_playerInstance = null;
        }
    }
}