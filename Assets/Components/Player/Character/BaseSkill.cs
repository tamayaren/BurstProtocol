using SkillSet;
using UnityEngine;

[System.Serializable]
public class BaseCharacterSkills : SkillLogic
{
    public new void Initialize(Entity entity)
    {
        base.Initialize(entity);
        
        this.skillId = 0;
        this.cooldownDuration = 0;
    }

    public override void Action(Entity entity)
    {
        base.Action(entity);
        //
        
        Debug.Log("Action PERFORMED!");
        this.OnStatusChanged.Invoke(SkillStatus.Ended);
    }
}
