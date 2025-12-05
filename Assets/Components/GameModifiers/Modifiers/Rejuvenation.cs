
using UnityEngine;

public class Rejuvenation : GameModifierBase
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
