using UnityEngine;
using UnityEngine.UI;

namespace Entities.Views
{
    public class HealthBarView : MonoBehaviour
    {
        [SerializeField] private Image m_bar;
        [SerializeField] private HealthComponent m_healthComponent;

        private void OnEnable()
        {
            SetValue();
            m_healthComponent.ValueChanged += SetValue;
        }

        private void OnDisable()
        {
            m_healthComponent.ValueChanged -= SetValue;
        }

        private void SetValue()
        {
            m_bar.fillAmount = ((float)m_healthComponent.value / (float)m_healthComponent.maxValue);
        }
    }
}