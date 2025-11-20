using System;
using UnityEditor;
using UnityEngine;

namespace Game.Mechanics.Organelles
{
    public struct Organelle
    {
        public Guid organelleIdentifier;
        public int level;

        public Stat mainStat;
        public OrganelleType type;
        
        public Stat[] subStats;
        
        public float[] subStatValues;
        public float statValue;

        public bool initialized;
    }

    public enum OrganelleType
    {
        Nucleus = 1,
        Mitochondrion = 2,
        Ribosome = 3,
        Cilia = 4,
        Cytoskeleton = 5,
        Centriole = 6
    }

    public enum Stat
    {
        Health = 0,
        Attack = 1,
        Defence = 2,
        PathogenicAttack = 3,
        PathogenicDefence = 4,
        Speed = 5,
        Endurance = 6,
        CritRate = 7,
        CritDamage = 8,
    }
}

