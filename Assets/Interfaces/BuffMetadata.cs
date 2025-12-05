using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu(menuName = "Metadata/Buff", fileName = "Buff")]
public class BuffMetadata : ScriptableObject
{
    public string fullName;
    public string buffDescription;
    public int maxStacks;

    public Dictionary<string, int> statIncrease;
    public Sprite icon;
}
