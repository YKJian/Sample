using System;
using UnityEngine;
using Magic.Elements;
using UnityEngine.InputSystem;

namespace Magic.Systems
{
    [Serializable]
    public sealed class MagicInputHelper
    {
        [SerializeField] private MagicSystem m_magicSystem;

        [Header("Key Bindings")]
        [SerializeField] private Key m_element1Key = Key.Q;
        [SerializeField] private Key m_element2Key = Key.W;
        [SerializeField] private Key m_element3Key = Key.E;
        [SerializeField] private Key m_element4Key = Key.R;

        private bool m_isInitialized;

        private Mouse m_mouse;
        private Keyboard m_keyboard;

        public void Update()
        {
            if (!m_isInitialized)
            {
                Initialize();
            }

            if (m_keyboard[m_element1Key].wasPressedThisFrame)
            {
                m_magicSystem.AddElement(ElementType.Element1);
            }
            else if (m_keyboard[m_element2Key].wasPressedThisFrame)
            {
                m_magicSystem.AddElement(ElementType.Element2);
            }
            else if (m_keyboard[m_element3Key].wasPressedThisFrame)
            {
                m_magicSystem.AddElement(ElementType.Element3);
            }
            else if (m_keyboard[m_element4Key].wasPressedThisFrame)
            {
                m_magicSystem.AddElement(ElementType.Element4);
            }

            if (m_mouse.leftButton.wasPressedThisFrame)
            {
                m_magicSystem.TryCastSpell();
            }
        }

        private void Initialize()
        {
            m_mouse = Mouse.current;
            m_keyboard = Keyboard.current;

            m_isInitialized = true;
        }
    }
}
