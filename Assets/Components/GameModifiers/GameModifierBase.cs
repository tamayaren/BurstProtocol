using UnityEngine;

namespace GamespaceModifiers
{
    public abstract class GameModifierBase : MonoBehaviour
    {
        public int stack;
        public int stackLimit;

        public string modifierName;
        public string modifierDescription;

        public ModifierType modifierType;

        private void Awake() => DynamicDress();

        public virtual void DynamicDress()
        {
            
        }

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
