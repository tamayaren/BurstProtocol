
using UnityEngine;

public class StatIncreaseDefense : GameModifierBase
{
    private EntityStats stats;
    
    public override void Initialize()
    {
        this.stats = GetComponent<EntityStats>();
        Debug.Log(this.name + " assigned");
        this.stats.defense += 50;
        PlayerBuffs.instance.RemoveBuffComplete(this.name);
        
        base.Initialize();
    }

    public override void Uninitialize()
    {
        base.Uninitialize();
    }
}
