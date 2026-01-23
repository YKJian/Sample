using UnityEngine;

namespace Magic.Buffs
{
    public interface IBuff
    {
        public string id { get; }

        public Sprite icon { get; }

        public BuffType type { get; }

        public void Initialize(BuffContainer container);

        public void Deinitialize();

        public void Update(float deltaTime);

        public IBuff Clone();
    }

    public interface ITimedBuff : IBuff
    {
        public float timer { get; }

        public float duration { get; }
    }
}