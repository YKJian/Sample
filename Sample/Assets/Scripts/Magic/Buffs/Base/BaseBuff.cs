using System;
using UnityEngine;

namespace Magic.Buffs.Base
{
    [Serializable]
    public abstract class BaseBuff : IBuff
    {
        [field: SerializeField]
        public string id { get; private set; }

        [field: SerializeField]
        public Sprite icon { get; private set; }

        [field: SerializeField]
        public BuffType type { get; private set; }

        protected BuffContainer container { get; private set; }

        public BaseBuff() { }

        protected BaseBuff(string id, Sprite icon, BuffType type)
        {
            this.id = id;
            this.icon = icon;
            this.type = type;    
        }

        public void Initialize(BuffContainer container)
        {
            this.container = container;
            OnInitialized();
        }

        protected virtual void OnInitialized() { }

        public void Deinitialize()
        {
            OnDeinitializing();

            container.Remove(this);
            container = null;
        }

        protected virtual void OnDeinitializing() { }

        public virtual void Update(float deltaTime) { }

        public abstract IBuff Clone();
    }
}