
using UnityEngine;

public class StatIncreaseCritRate : GameModifierBase
{
    private EntityStats stats;
    public override void Initialize()
    {
        this.stats = GetComponent<EntityStats>();
        Debug.Log(this.name + " assigned");
        this.stats.critDamage += 1;
        PlayerBuffs.instance.RemoveBuffComplete(this.name);
        
        base.Initialize();
    }

    public override void Uninitialize()
    {
        base.Uninitialize();
    }
}
