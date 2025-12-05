using UnityEngine;
using UnityEngine.Events;

public class GameModifierBase : MonoBehaviour
{
    public int stack = 1;

    public string modifierName;
    public string modifierDescription;

    public ModifierType modifierType;
    
    public UnityEvent<int> stackChanged = new UnityEvent<int>();
    public virtual void Initialize()
    {
        Debug.Log("Base assigned");
    }

    public virtual void Uninitialize()
    {
        Destroy(this.gameObject);
    }
}

public enum ModifierType
{
    CharacterBuff,
    SessionModifier,
}

