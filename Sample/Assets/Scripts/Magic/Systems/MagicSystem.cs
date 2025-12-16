using Magic.Elements;
using Players;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Magic.Systems
{
    public class MagicSystem: MonoBehaviour
    {
        public event Action<MagicState> StateChanged;
        public event Action SpellCancelled;

        public event Action<IReadOnlyList<ElementType>> ElementChanged
        {
            add => spellPreparation.ElementsChanged += value;
            remove => spellPreparation.ElementsChanged -= value;
        }

        [SerializeField] private MagicConfig m_config;
        [SerializeField] private MouseResolver m_mouseResolver;

        private MagicState m_state;
        private SpellCaster m_caster;
        private Coroutine m_cooldownCoroutine;
        private SpellPreparation m_spellPreparation;

        public MagicState state
        {
            get => m_state;
            private set
            {
                if (m_state != value)
                {
                    m_state = value;
                    StateChanged?.Invoke(m_state);
                }
            }
        }

        private SpellPreparation spellPreparation =>
            m_spellPreparation ??= new SpellPreparation(m_config);

        private void Awake()
        {
            m_caster = new SpellCaster(transform);
        }

        private void OnEnable()
        {
            spellPreparation.OverflowOccured += CancelSpell;
        }
        private void OnDisable()
        {
            spellPreparation.OverflowOccured -= CancelSpell;
        }

        public void AddElement(ElementType element)
        {
            if (state is MagicState.Cooldown or MagicState.Casting)
            {
                return;
            }

            spellPreparation.AddElement(element);
            state = MagicState.Preparation;
        }

        public void TryCastSpell()
        {
            if (state is not MagicState.Preparation)
            {
                return;
            }
            
            if (spellPreparation.TryGetSpell(out var spell))
            {
                state = MagicState.Casting;

                m_caster.Cast(spell, m_mouseResolver.m_mousePosition);

                spellPreparation.Clear();
                state = MagicState.Idle;
            }
            else
            {
                CancelSpell();
            }
        }

        private void CancelSpell()
        {
            if (state is MagicState.Preparation)
            {
                spellPreparation.Clear();
                SpellCancelled?.Invoke();

                m_cooldownCoroutine = StartCoroutine(CooldownRoutine());
            }
        }

        private void StartCooldown()
        {
            if (m_cooldownCoroutine is not null)
            {
                StopCoroutine(m_cooldownCoroutine);
            }

            m_cooldownCoroutine = StartCoroutine(CooldownRoutine());
        }

        private IEnumerator CooldownRoutine()
        {
            state = MagicState.Cooldown;
            yield return new WaitForSeconds(m_config.cancelCooldown);
            state = MagicState.Idle;

            m_cooldownCoroutine = null;
        }
    }

    public enum MagicState
    {
        Idle,
        Preparation, 
        Cooldown,
        Casting
    }
}
