using System;
using System.Collections.Generic;
using GamespaceModifiers;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class PlayerBuffs : SerializedMonoBehaviour
{
    public static PlayerBuffs instance;
    
    public Dictionary<string, BuffMetadata> buffs;
    public UnityEvent<string, BuffMetadata> onBuffAdded;
    public UnityEvent<string> onBuffRemoved;
    public UnityEvent<string, int> reupdateBuffStack;

    private void Awake()
    {
        instance = this;   
        this.buffs = new Dictionary<string, BuffMetadata>();
        this.onBuffAdded = new UnityEvent<string, BuffMetadata>();
        this.onBuffRemoved = new UnityEvent<string>();
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
            return;
        }

        Type type = Type.GetType(buffName);
        Debug.Log($"Attempt {buffName}");
        
        GameModifierBase buffAddedLogic = (GameModifierBase)this.gameObject.AddComponent(type);
        this.buffs.Add(buffName, buff);
        this.onBuffAdded.Invoke(buffName, buff);
        buffAddedLogic.Initialize();
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
            this.onBuffRemoved.Invoke(buffName);
        }
    }
}
