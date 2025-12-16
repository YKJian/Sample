using Magic.Systems;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Players
{
    [RequireComponent(typeof(PlayerMovement))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerConfig m_config;
        [SerializeField] private PlayerMovement m_playerMovement;
        [SerializeField] private MouseResolver m_navMeshMouseResolver;
        [SerializeField] private MagicInputHelper m_magicInputHelper;

        private PlayerRotationCalculator m_playerRotationCalculator;

        private void OnValidate()
        {
            if (!m_playerMovement)
            {
                m_playerMovement = GetComponent<PlayerMovement>();
            }

            if (!m_navMeshMouseResolver)
            {
                m_navMeshMouseResolver = GetComponent<MouseResolver>();
            }
        }

        private void Start()
        {
            var camera = Camera.main;

            m_playerMovement.Initialize(m_config.speed, m_config.angularSpeed);
            m_playerRotationCalculator = new PlayerRotationCalculator(camera, transform);

            SetupCursor();
        }

        private void Update()
        {
            Vector3 mousePosition = Mouse.current.position.ReadValue();
            var lookPoint = m_playerRotationCalculator.Calculate(mousePosition);
            m_playerMovement.RotateTowards(lookPoint);

            if (Mouse.current.rightButton.wasPressedThisFrame)
            {
                Vector3? navPoint = m_navMeshMouseResolver.GetNavMeshPoint();

                if (navPoint.HasValue)
                {
                    m_playerMovement.SetDestination(navPoint.Value);
                }
            }

            m_magicInputHelper.Update();
        }

        private void SetupCursor()
        {
            var texture = m_config.cursorTexture;

            if (texture)
            {
                var hotspot = new Vector2(texture.width / 2f, texture.height / 2f);
                Cursor.SetCursor(texture, hotspot, CursorMode.Auto);
            }
        }
    }
}
