using Magic.Effects;
using Magic.Elements;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Magic.Spells.Data
{
    public abstract class BaseSpellData : ScriptableObject
    {
        [SerializeField] private string m_spellName;
        [SerializeField] private GameObject m_visualEffect;
        [SerializeField] private ElementType[] m_combination;

        [SerializeReferenceDropdown]
        [SerializeReference] private IEffect[] m_effects;

        public string spellName => m_spellName; 
        public GameObject visualEffect => m_visualEffect;
        public IReadOnlyList<ElementType> combination => m_combination;

        private void OnValidate()
        {
            if (m_combination?.Length > 3)
            {
                m_combination = m_combination.Take(3).ToArray();
            }
        }
    }
}