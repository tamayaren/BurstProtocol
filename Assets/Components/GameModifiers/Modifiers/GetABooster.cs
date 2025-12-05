
using UnityEngine;

public class GetABooster : GameModifierBase
{
    public override void Initialize()
    {
        Debug.Log(this.name + " assigned");
        base.Initialize();
    }

    public override void Uninitialize()
    {
        base.Uninitialize();
    }
}
