using Magic.Effects;
using System.Collections.Generic;
using UnityEngine;

namespace Magic.Spells.Projectiles
{
    public interface ISpellProjectile
    {
        public void Initialize(Vector3 targetPosition, float speed, IReadOnlyList<IEffect> effects);
    }
}