namespace Entities
{
    public interface IAcceleration 
    {
        public void IncreaseAcceleration(float delta);

        public void DecreaseAcceleration(float delta);
    }
}