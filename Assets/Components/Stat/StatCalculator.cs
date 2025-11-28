using UnityEngine;

namespace Game.Mechanics
{
    public class StatCalculator : MonoBehaviour
    {
        private static float dashDuration = .125f;
        private static float dashSpeed = 32f;
        public static float CalculateSpeed(float baseSpeed, EntityStats stats) => baseSpeed * (stats.speed * .8f + ((float)stats.endurance/64f));
        public static float CalculateDashCooldown(float baseDashCooldown, EntityStats stats) => 3f;

        public static (float, float) CalculateDash(EntityStats stats)
        {
            float speed = dashSpeed * (1.05f+(stats.speed/255f)+(stats.endurance/64f));
            float duration = dashDuration * (1.25f+(stats.endurance/48f));
            
            return (speed, duration);
        }

        public static int CalculateDamage(DamageParameters damageParameters)
        {
            float baseDamage = damageParameters.baseDamage;
            DamageType type = damageParameters.damageType;
            
            EntityStats attacker = damageParameters.attacker;
            EntityStats foe = damageParameters.foe;
            
            EntityStatsSchema statModifier = damageParameters.statModifier;
            
            if (type == DamageType.True)
                return (int)((baseDamage) + attacker.attack * 1 + ((attacker.level + statModifier.level) / 100f));
            
            float computedDamage = Random.Range(baseDamage-(baseDamage*.05f), baseDamage+(baseDamage*.05f));
            switch (type)
            {
                default:
                case DamageType.Physical:
                    computedDamage -= (foe.defense * .25f);
                    computedDamage += attacker.attack + statModifier.attack;
                    break;
                
                case DamageType.Pathogenic:
                    computedDamage -= (foe.defense * .125f);
                    computedDamage -= (foe.pathDefense * .33f);
                    computedDamage += (attacker.pathAttack + statModifier.pathAttack) * .8f;
                    computedDamage += (attacker.attack + statModifier.attack) * .25f;
                    break;
            }

            float crit = Random.Range(0f, Mathf.Clamp(attacker.critRate + statModifier.critRate, 0f, 90f));
            if (crit <= attacker.critRate)
                computedDamage *= 1 + ((attacker.critDamage + statModifier.critRate) / 200f);

            computedDamage *= (1 + (attacker.level / 100f));
            return (int)(Mathf.Max(1f, computedDamage));
        }
    }

    public struct DamageParameters
    {
        public EntityStats attacker;
        public EntityStats foe;
        public DamageType damageType;
        public float baseDamage;
        public EntityStatsSchema statModifier;

        public DamageParameters(float baseDamage, DamageType damageType, EntityStats attacker, EntityStats foe)
        {
            this.baseDamage = baseDamage;
            this.damageType = damageType;
            this.attacker = attacker;
            this.foe = foe;
            
            this.statModifier = new EntityStatsSchema();
        }

        public DamageParameters SetStatModifier(EntityStatsSchema statModifier)
        {
            this.statModifier = statModifier;
            return this;
        }
    }

    public enum DamageType
    {
        Physical,
        Pathogenic,
        True
    }
}
