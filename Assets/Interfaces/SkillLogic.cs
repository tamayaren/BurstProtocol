using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace SkillSet {
    public enum SkillStatus
    {
        Started,
        Performing,
        Ended,
        Cancelled
    }
    
    [System.Serializable]
    public abstract class SkillLogic
    {
        protected int skillId;
        public bool onCooldown;

        [SerializeField] private float cooldown;
        public float cooldownDuration { get => this.cooldown; set => this.cooldown = value; }

        [NonSerialized] public Action<bool> OnCooldown;
        [NonSerialized] public Action<SkillStatus> OnStatusChanged;

        public EntityStats stats;
        public Entity entity;

        public string skillName;
        
        public virtual void Initialize(Entity entity)
        {
            this.entity = entity;
            this.stats = entity.GetComponent<EntityStats>();
        }

        public bool Perform(Entity entity, MonoBehaviour runner)
        {
            if (this.onCooldown) return false;
            
            Debug.Log("Perform action");
            return true;
        }

        public void CanPerformNow() => this.onCooldown = false;


        public virtual bool Update(Entity entity, MonoBehaviour runner)
        {
            return false;
        }
        public virtual float Action(Entity entity, MonoBehaviour runner)
        {
            Debug.Log("Action performed!");
            return this.cooldownDuration;
        }

    }
}