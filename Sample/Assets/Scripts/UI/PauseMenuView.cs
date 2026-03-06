using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PauseMenuView : MonoBehaviour
    {
        public event Action ContinueClicked;
        public event Action MainMenuClicked;

        [SerializeField] private Button m_continue;
        [SerializeField] private Button m_mainMenu;

        private void OnEnable()
        {
            m_continue.onClick.AddListener(OnContinueClicked);
            m_mainMenu.onClick.AddListener(OnMainMenuClicked);
        }

        private void OnDisable()
        {
            m_continue.onClick.RemoveListener(OnContinueClicked);
            m_mainMenu.onClick.RemoveListener(OnMainMenuClicked);
        }

        private void OnContinueClicked()
        {
            ContinueClicked?.Invoke();
        }

        private void OnMainMenuClicked()
        {
            MainMenuClicked?.Invoke();
        }
    }
}