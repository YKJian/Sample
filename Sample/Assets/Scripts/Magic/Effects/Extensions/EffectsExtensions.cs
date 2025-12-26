using Magic.Effects;
using System.Collections.Generic;

namespace Magic.Effects.Extensions
{
    public static class EffectsExtensions
    {
        public static void ApplyEffects(
            this IReadOnlyCollection<IEffect> effects,
            IEffectable effectable)
        {
            if (effects is null) return;

            foreach (var effect in effects)
            {
                effect?.Apply(effectable);
            }
        }

        public static void ApplyEffects(
            this IReadOnlyCollection<IEffect> effects,
            IReadOnlyCollection<IEffectable> effectables)
        {
            if (effects is null) return;

            foreach (var effect in effects)
            {
                foreach (var effectable in effectables)
                {
                    effect?.Apply(effectable);
                }
            }
        }
    }
}