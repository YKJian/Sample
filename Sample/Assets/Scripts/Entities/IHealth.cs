namespace Entities
{
    public interface IHealth
    {
        public float value { get; set; }

        public void Heal(float health);

        public void TakeDamage(float damage);
    }
}