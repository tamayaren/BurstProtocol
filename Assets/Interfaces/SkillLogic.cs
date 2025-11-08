using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace SkillSet {
    public interface ISkillLogic
    {
        public abstract void Initialize();
        public abstract void Action(Entity entity);
    }

    public enum SkillStatus
    {
        Started,
        Performing,
        Ended,
        Cancelled
    }
    
    [System.Serializable]
    [CreateAssetMenu(fileName = "New Skill", menuName = "Skill/Skill")]
    public class SkillLogic: ScriptableObject, ISkillLogic
    {
        protected int skillId;
        public bool onCooldown;
        public int cooldownDuration { get; set; }

        public UnityEvent<bool> OnCooldown = new UnityEvent<bool>();
        public UnityEvent<SkillStatus> OnStatusChanged = new UnityEvent<SkillStatus>();
        
        public virtual void Initialize()
        {
            
        }

        public bool Perform(Entity entity, MonoBehaviour runner)
        {
            if (this.onCooldown) return false;
            
            this.OnStatusChanged.Invoke(SkillStatus.Started);
            Debug.Log("Perform action");
            
            return true;
        }
        
        public virtual void Action(Entity entity)
        {
            Debug.Log("Action performed!");
            this.OnStatusChanged.Invoke(SkillStatus.Performing);
        }
    }
}