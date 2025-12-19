using UnityEngine;

namespace Magic.Spells.Data
{
    [CreateAssetMenu(fileName = "AoeSpellData", menuName = "Xlab/Magic/Spells/Aoe Spell")]
    public class AoeSpellData : BaseSpellData
    {
        [SerializeField] private bool m_isTarget;
        [SerializeField][Min(0)] private float m_radius;

        public float radius => m_radius;

        public bool isTarget => m_isTarget;
    }
}

