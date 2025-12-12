namespace Magic.Effects
{
    public interface IEffect
    {
        public void Apply(IEffectable effectable);
    }

    public interface IEffectable { }
}