
using UnityEngine;

public class HealthPotion : GameModifierBase
{
    public override void Initialize()
    {
        Debug.Log("Health Potion" + " assigned");
        Entity entity = GetComponent<Entity>();
        entity.Health += (int)(entity.MaxHealth * .3f);
        
        PlayerBuffs.instance.RemoveBuffComplete(this.name);
        
        base.Initialize();
    }

    public override void Uninitialize()
    {
        base.Uninitialize();
    }
}
