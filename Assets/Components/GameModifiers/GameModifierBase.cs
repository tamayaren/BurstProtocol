using UnityEngine;

namespace GamespaceModifiers
{
    public abstract class GameModifierBase : MonoBehaviour
    {
        public int stack;

        public string modifierName;
        public string modifierDescription;

        public ModifierType modifierType;

        public virtual void Initialize()
        {
            
        }

        public virtual void Uninitialize()
        {
            
        }
    }

    public enum ModifierType
    {
        CharacterBuff,
        SessionModifier,
    }
}
