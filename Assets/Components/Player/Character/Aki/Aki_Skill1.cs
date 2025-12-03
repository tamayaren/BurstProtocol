using SkillSet;
using UnityEngine;

namespace SkillSet.Aki
{
    [System.Serializable]
    public class AkiSkill1 : SkillLogic
    {
        public new string skillName = "Go Feral!";
        public new float skillId = 1;
        public new int cooldownDuration = 10;
        
        public new void Initialize(Entity entity)
        {
            base.Initialize(entity);
        }

        public override float Action(Entity entity, MonoBehaviour runner)
        {
            base.Action(entity, runner);
            //
            
            Debug.Log("[AS] Aki Skill 1 Performed!");
            this.OnStatusChanged.Invoke(SkillStatus.Ended);
            return this.cooldownDuration;
        }
    }
}
