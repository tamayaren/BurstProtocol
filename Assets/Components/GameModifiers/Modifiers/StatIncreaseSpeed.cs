
using UnityEngine;

public class StatIncreaseSpeed : GameModifierBase
{
    private EntityStats stats;
    public override void Initialize()
    {
        this.stats = GetComponent<EntityStats>();
        Debug.Log(this.name + " assigned");
        this.stats.speed += 1;
        PlayerBuffs.instance.RemoveBuffComplete(this.name);
        
        base.Initialize();
    }

    public override void Uninitialize()
    {
        base.Uninitialize();
    }
}
