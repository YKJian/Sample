namespace Magic.Buffs
{
    public interface IBuff
    {
        public string Id { get; }

        public void Initialize(BuffContainer container);

        public void Deinitialize();

        public void Update(float deltaTime);

        public IBuff Clone();
    }
}