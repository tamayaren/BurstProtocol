
using UnityEngine;

public class SmartPathogen : GameModifierBase
{
    private EntityStats stats;
    public override void Initialize()
    {
        Debug.Log("REjuvenation" + " assigned");
        Entity entity = GetComponent<Entity>();
        entity.Health = entity.MaxHealth;

        base.Initialize();
    }

    public override void Uninitialize()
    {
        base.Uninitialize();
    }
}
