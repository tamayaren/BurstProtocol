using System;

using TMPro;
using UnityEngine;

public class UIBuffLists : MonoBehaviour
{
    [SerializeField] private GameObject buffPrefab;

    [SerializeField] private Transform buffContainer;

    [SerializeField] private GameObject buffDescription;
    
    private void Start()
    {
        PlayerBuffs.instance.onBuffAdded.AddListener((name, metadata, modifierLogic) =>
        {
            GameObject buff = Instantiate(this.buffPrefab, this.buffContainer);
            buff.name = name;
            
            UIBuff uiBuff = buff.GetComponent<UIBuff>();
            uiBuff.Initialize(metadata, this.buffDescription, modifierLogic);
            
        });
        
        PlayerBuffs.instance.onBuffRemoved.AddListener((name) =>
        {
            Transform obj = this.buffContainer.Find(name);
            
            Debug.Log("ultraDelta " + name);
            Debug.Log(obj);
            if (obj != null)
                Destroy(obj.gameObject);
        });
    }
}
