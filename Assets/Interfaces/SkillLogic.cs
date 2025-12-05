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

        private IEnumerator Cooldown()
        {
            Debug.Log("MOVE COOLDOWN 1");
            this.onCooldown = true;
            yield return new WaitForSeconds(this.cooldownDuration);
            Debug.Log("MOVE COOLDOWN 2");
            this.onCooldown = false;
        }

        public bool Perform(Entity entity, MonoBehaviour runner)
        {
            Debug.Log($"PERFORMANCE MOVE: {this.onCooldown}");
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

            runner.StartCoroutine(Cooldown());
            return this.cooldownDuration;
        }

    }
}