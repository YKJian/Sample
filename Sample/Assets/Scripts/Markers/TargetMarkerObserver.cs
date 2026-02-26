using Infrastructure;
using Players;
using UnityEngine;

namespace Markers
{
    public sealed class TargetMarkerObserver : MonoBehaviour
    {
        [SerializeField] private TargetMarker m_targetMarker;
        
        private PlayerMovement m_playerMovement;

        private PlayerMovement playerMovement
        {
            get 
            {
                if (m_playerMovement != null)
                {
                    return m_playerMovement;
                }

                m_playerMovement = ServiceLocator
                .Resolve<IPlayerFactory>()
                .Create()
                .GetComponent<PlayerMovement>();

                return m_playerMovement;
            }
        }

        public void Initialize(PlayerMovement playerMovement)
        {
            m_playerMovement = playerMovement;

            m_playerMovement.Stopped += OnPlayerStopped;
            m_playerMovement.DestinationChanged += OnDestinationChanged;
        }

        private void Deinitialize()
        {
            
        }

        private void OnPlayerStopped() =>
            m_targetMarker.Hide();

        private void OnDestinationChanged(Vector3 worldPosition) =>
            m_targetMarker.Show(worldPosition);
    }
}
