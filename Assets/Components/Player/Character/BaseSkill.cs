using SkillSet;
using UnityEngine;

[System.Serializable]
public class BaseSkill : SkillLogic
{
    public void Start() => Initialize();
    public override void Initialize()
    {
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
