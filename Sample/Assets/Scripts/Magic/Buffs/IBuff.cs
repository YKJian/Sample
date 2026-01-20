using System;

namespace Magic.Buffs
{
    public interface IBuff: ICloneable
    {
        public string Id { get; }

        public void Initialize(BuffContainer container);

        public void Deinitialize();

        public void Update(float deltaTime);
    }
}