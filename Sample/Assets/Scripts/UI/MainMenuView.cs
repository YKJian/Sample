using Infrastructure;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] private Button m_playButton;
        [SerializeField] private Button m_exitButton;

        private Loading m_loading;

        private void Start()
        {
            m_loading = ServiceLocator.Resolve<Loading>();
        }

        private void OnEnable()
        {
            m_playButton.onClick.AddListener(OnPlayClick);
            m_exitButton.onClick.AddListener(OnExitClick);
        }

        private void OnDisable()
        {
            m_playButton.onClick.RemoveListener(OnPlayClick);
            m_exitButton.onClick.RemoveListener(OnExitClick);
        }

        private void OnPlayClick()
        {
            gameObject.SetActive(false);
            m_loading.LoadScene(GlobalConstants.Scenes.Game);
        }

        private void OnExitClick()
        {
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.ExitPlaymode();
#endif
            Application.Quit();

        }
    }
}