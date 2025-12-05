
using UnityEngine;

public class StatIncreasePathAttack : GameModifierBase
{
    private EntityStats stats;

    public override void Initialize()
    {
        this.stats = GetComponent<EntityStats>();
        Debug.Log(this.name + " assigned");
        this.stats.pathAttack += 100;
        PlayerBuffs.instance.RemoveBuffComplete(this.name);
        
        base.Initialize();
    }

    public override void Uninitialize()
    {
        base.Uninitialize();
    }
}
