using System;
using System.Collections.Generic;
using UnityEngine;

namespace Magic.Elements
{
    [CreateAssetMenu(fileName = "ElementsData", menuName = "Xlab/Magic/Elements Data")]
    public sealed class ElementsData : ScriptableObject
    {
        [SerializeField] private Item[] m_items;

        public IReadOnlyList<Item> Items => m_items;

        [Serializable]
        public sealed class Item
        {
            [SerializeField] private string m_elementName;
            [SerializeField] private ElementType m_type;
            [SerializeField] private Sprite m_icon;

            public Sprite icon => m_icon;

            public string elementName => m_elementName;

            public ElementType type => m_type;
        }
    }
}

