using Magic.Spells.Data;
using UnityEngine;

namespace Magic.Elements
{
    [CreateAssetMenu(fileName = "MagicConfig", menuName = "Xlab/Magic/Elements/Magic Config")]
    public sealed class MagicConfig : ScriptableObject
    {
        [SerializeField] private ElementsData m_elementsData;
        [SerializeField] private SpellsDatabase m_spellDataBase;

        [SerializeField][Min(1)] private int m_maxElements = 3;
        [SerializeField][Min(0)] private float m_cancelCooldown = 0.3f;

        public ElementsData elementsData => m_elementsData;
        public SpellsDatabase spellsDatabase => m_spellDataBase;

        public int maxElements => m_maxElements;

        public float cancelCooldown => m_cancelCooldown;
    }
}