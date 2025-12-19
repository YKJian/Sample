using System.Collections.Generic;
using UnityEngine;

namespace Magic.Spells.Data
{
    [CreateAssetMenu(fileName = "SpellsDatabase", menuName = "Xlab/Magic/Spells/Spells Database")]
    public sealed class SpellsDatabase : ScriptableObject
    {
        [SerializeField] private BaseSpellData[] m_spells;

        public IReadOnlyList<BaseSpellData> spells => m_spells;
    }
}
