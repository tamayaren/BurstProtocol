using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class PlayerBuffs : SerializedMonoBehaviour
{
    public static PlayerBuffs instance;
    
    public Dictionary<string, BuffMetadata> buffs;
    public Dictionary<string, GameModifierBase> buffsLogics;
    public UnityEvent<string, BuffMetadata, GameModifierBase> onBuffAdded;
    public UnityEvent<string> onBuffRemoved;
    public UnityEvent<string, int> reupdateBuffStack;

    private void Awake()
    {
        instance = this;   
        this.buffs = new Dictionary<string, BuffMetadata>();
        this.onBuffAdded = new UnityEvent<string, BuffMetadata, GameModifierBase>();
        this.onBuffRemoved = new UnityEvent<string>();
        this.buffsLogics = new Dictionary<string, GameModifierBase>();
        this.reupdateBuffStack = new UnityEvent<string, int>();
    }

    public void AddBuff(string buffName, BuffMetadata buff)
    {
        if (this.buffs.ContainsKey(buffName))
        {
            // Add stack
            GameModifierBase buffLogic;
            TryGetComponent(out buffLogic);

            if (buffLogic.stack < buff.maxStacks)
                buffLogic.stack++;
            
            this.reupdateBuffStack.Invoke(buffName, buff.maxStacks);
            
            buffLogic.stackChanged.Invoke(buffLogic.stack);
            return;
        }
        
        Debug.Log(typeof(Rejuvenation).AssemblyQualifiedName);
        Type reference = typeof(Rejuvenation);
        
        string assemblyName = reference.Assembly.GetName().Name;
        Type type = Type.GetType(buffName + ", " + assemblyName);
        Debug.Log($"Attempt {buffName}");
        
        GameModifierBase buffAddedLogic = (GameModifierBase)this.gameObject.AddComponent(type);
        this.buffs.Add(buffName, buff);
        this.buffsLogics.Add(buffName, buffAddedLogic);

        Debug.Log(this.onBuffAdded);
        Debug.Log(buffAddedLogic);

        StartCoroutine(InitiailizeBuff(buffName, buff, buffAddedLogic));
        buffAddedLogic.Initialize();
    }

    private IEnumerator InitiailizeBuff(string name, BuffMetadata buff, GameModifierBase modifierLogic)
    {
        yield return null;
        this.onBuffAdded.Invoke(name, buff, modifierLogic);
    }

    public void RemoveBuffStack(string name)
    {
        if (this.buffs.ContainsKey(name))
        {
            GameModifierBase buffLogic;
            TryGetComponent(out buffLogic);

            if (buffLogic.stack > 1)
                buffLogic.stack--;
            
            this.reupdateBuffStack.Invoke(name, buffLogic.stack);
        }
    }
    
    public void RemoveBuffComplete(string buffName)
    {
        if (this.buffs.ContainsKey(buffName))
        {
            GameModifierBase buffLogic;
            TryGetComponent(out buffLogic);

            buffLogic.Uninitialize();
            bool metadata = this.buffs.Remove(buffName);
            Destroy(buffLogic);
            this.onBuffRemoved.Invoke(buffName);
        }
    }
}
