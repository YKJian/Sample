using Magic.Effects;
using Magic.Effects.Extensions;
using System.Collections.Generic;
using UnityEngine;

namespace Magic.Spells.Aoe
{
    public sealed class SpellAoe : MonoBehaviour, ISpellAoe
    {
        public void Initialize(Vector3 targetPosition, float radius, IReadOnlyCollection<IEffect> effects)
        {
            var colliders = Physics.OverlapSphere(targetPosition, radius, gameObject.layer);

            foreach (var collider in colliders)
            {
                if (collider.gameObject.layer == gameObject.layer)
                {
                    continue;
                }

                var effectables = collider.GetComponents<IEffectable>();
                effects.ApplyEffects(effectables);
            }
        }
    }
}