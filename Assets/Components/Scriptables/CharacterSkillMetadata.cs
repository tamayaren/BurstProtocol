using System;
using System.Collections.Generic;
using SkillSet.Aki;
using UnityEngine;

public static class CharacterSkillMetadata
{
    public static readonly Dictionary<string, Type> SkillRegistry = new Dictionary<string, Type>()
    {
        { "AkiSkill1", typeof(AkiSkill1) }
    };
}
