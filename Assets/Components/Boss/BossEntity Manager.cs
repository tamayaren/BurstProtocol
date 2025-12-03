using UnityEngine;

public class BossEntityManager : MonoBehaviour
{
    public GameObject bossPrefab;
    public EntityStats bossStats;
    public Entity bossEntity;
    private PlayerCharacterIdentifier identifier;
    
    void Start()
    {
        this.bossEntity = GetComponent<Entity>();
        this.bossStats = GetComponent<EntityStats>();
    }

    void Update()
    {
        if (bossEntity.Health == 100)      
        {
            Phase1();
        }

        if (bossEntity.Health == 50)
        {
            Phase2();
        }
    }
    
    void Phase1()
    {
        this.bossPrefab.SetActive(true);
        bossStats.Feed(new EntityStatsSchema()    
            .SetLevel(3)
            .SetAttack(300)
            .SetDefense(300)
            .SetEndurance(30)
            .SetSpeed(30)
            .SetCritDamage(50)
            .SetCritRate(30)
        , this.identifier.currentCharacter.statLevelGrowth);
    }

    void Phase2()
    {
        bossStats.Feed(new EntityStatsSchema()   
            .SetLevel(6)
            .SetAttack(600)
            .SetDefense(600)
            .SetEndurance(60)
            .SetSpeed(40)
            .SetCritDamage(80)
            .SetCritRate(50)
        , this.identifier.currentCharacter.statLevelGrowth);
        
    }
}
