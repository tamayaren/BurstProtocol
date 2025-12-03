using SkillSet;
using UnityEngine;


namespace SkillSet.Base
{
    [System.Serializable]
    public class BaseSkill : SkillLogic
    {
        public new void Initialize(Entity entity)
        {
            base.Initialize(entity);
            
            this.skillId = 0;
            this.cooldownDuration = 0;
        }

        public override float Action(Entity entity, MonoBehaviour runner)
        {
            base.Action(entity, runner);
            //
            
            Debug.Log("Action PERFORMED!");
            this.OnStatusChanged.Invoke(SkillStatus.Ended);
            return this.cooldownDuration;
        }
    }
}
