using UnityEngine;

namespace Players
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Player Config")]
    public sealed class PlayerConfig : ScriptableObject
    {
        [SerializeField] [Range(0f, 100f)] private float m_speed = 5f;

        public float speed => m_speed;
    }
}

