
using UnityEngine;

public class CytoplasmicViscosity : GameModifierBase
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
