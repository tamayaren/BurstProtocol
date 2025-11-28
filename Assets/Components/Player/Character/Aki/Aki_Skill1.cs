using SkillSet;
using UnityEngine;

namespace SkillSet.Aki
{
    [System.Serializable]
    public class AkiSkill1 : SkillLogic
    {
        public new string skillName = "Go Feral!";
        public new void Initialize(Entity entity)
        {
            base.Initialize(entity);
            
            this.skillId = 1;
            this.cooldownDuration = 3;
        }

        public override void Action(Entity entity, MonoBehaviour runner)
        {
            base.Action(entity, runner);
            //
            
            Debug.Log("[AS] Aki Skill 1 Performed!");
            this.OnStatusChanged.Invoke(SkillStatus.Ended);
        }
    }
}
