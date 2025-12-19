namespace Entities
{
    public interface IHealth
    {
        public void Heal(float health);

        public void TakeDamage(float damage);
    }
}