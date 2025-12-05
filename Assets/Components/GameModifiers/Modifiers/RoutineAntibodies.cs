
using UnityEngine;

public class RountineAntibodies : GameModifierBase
{
    private void StatAndModify(EntityStats stats, string name, float value)
    {
        switch (name)
        {
            default:
            case "Attack":
                stats.attack += (int)(stats.attack * value);
                break;
            case "Defense":
                stats.defense += (int)(stats.defense * value);
                break;
            case "Speed":
                stats.speed += (int)(stats.speed * value);
                break;
            case "Pathogenic Attack":
                stats.pathAttack += (int)(stats.pathAttack * value);
                break;
            case "Pathogenic Defense":
                stats.pathDefense += (int)(stats.pathDefense * value);
                break;
            case "CRIT Rate":
                stats.critRate += (int)(stats.critRate * value);
                break;
            case "CRIT Damage":
                stats.critDamage += (int)(stats.critDamage * value);
                break;
            case "Endurance":
                stats.endurance += (int)(stats.endurance * value);
                break;
        }
    }
    public override void Initialize()
    {
        Entity entity = GetComponent<Entity>();
        EntityStats entityStats = entity.GetComponent<EntityStats>();

        string[] names = new[]
        {
            "Attack",
            "Defense",
            "Pathogenic Attack",
            "Pathogenic Defense",
            "Speed",
            "Endurance",
            "CRIT Rate",
            "CRIT Damage",
        };

        string currentStat = "";
        entityStats.LevelChanged.AddListener((oldL, newL) =>
        {
            string stat = names[Random.Range(0, names.Length)];
            
            if (currentStat != "")
                StatAndModify(entityStats, currentStat, -.5f);
            
            UINotification.instance.Notify($"MUTATED STAT: {stat.ToUpper()}", Color.yellow);
            StatAndModify(entityStats, stat, 2f);
            currentStat = stat;
        });
    }

    public override void Uninitialize()
    {
        base.Uninitialize();
    }
}
