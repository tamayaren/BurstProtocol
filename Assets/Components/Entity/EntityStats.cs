using UnityEngine;
using UnityEngine.Events;

public class EntityStats : MonoBehaviour
{
    [SerializeField] [Range(1, 80)] private int _level = 1;
    [SerializeField] [Range(100, 8000)] private int _attack = 100;
    [SerializeField] [Range(100, 8000)] private int _defense = 100;
    [SerializeField] [Range(100, 8000)] private int _pathAttack = 100;
    [SerializeField] [Range(100, 8000)] private int _pathDefense = 100;
    
    [SerializeField] [Range(16, 255)] private int _speed = 16;
    [SerializeField] [Range(1, 64)] private int _endurance = 1;
    
    [SerializeField] [Range(0, 100)] private float _critRate = 5f;
    [SerializeField] [Range(0, 400)] private float _critDamage = 10f;

    [SerializeField] private float fireRate;
    [SerializeField] private float attackDamage;
    
    public UnityEvent<string, int, int> StatChanged = new UnityEvent<string, int, int>();
    public UnityEvent<string, float, float> CritChanged = new UnityEvent<string, float, float>();
    public UnityEvent<int, int> LevelChanged = new UnityEvent<int, int>();

    public bool initialized;
    public float dashRange = 1f;
    public float dashCooldown = 1f;

    public void FeedInitializer(EntityStatsSchema schema, float[] statLevelGrowth)
    {
        if (!this.initialized) return;
        
        this.initialized = true;
        this.level = schema.level;

        this.attack = (int) (schema.attack + (2000*(statLevelGrowth[1])));
        this.defense = (int) (schema.defense + (2000*(statLevelGrowth[2])));
        
        this.pathAttack = (int) (schema.pathAttack + (2000*(statLevelGrowth[3])));
        this.pathDefense = (int) (schema.pathDefense + (2000*(statLevelGrowth[4])));
        
        this.speed = (int) (schema.speed + (24*(statLevelGrowth[5])));
        this.endurance = (int) (schema.endurance + (16*(statLevelGrowth[6])));
        
        this.critRate = (schema.critRate + (10*(statLevelGrowth[7])));
        this.critDamage = (schema.critDamage + (30*(statLevelGrowth[8])));
    }
    
    public int level { get => this._level; set { this.LevelChanged.Invoke(this._level, value); this._level = value; } }

    private void _StatChanged(string statName, int oldValue, int value) => this.StatChanged.Invoke(statName,  oldValue, value);
    private void _CritChanged(string statName, float oldValue, float value) => this.CritChanged.Invoke(statName, oldValue, value);
    public int attack { get => this.level; set { _StatChanged("Attack", this._attack, value); this._attack = value; } }
    public int defense { get => this.level; set { _StatChanged("Defense", this._defense, value); this._defense = value; } }
    public int pathAttack { get => this.level; set { _StatChanged("PathogenicAttack", this._pathAttack, value); this._pathAttack = value; } }
    public int pathDefense { get => this.level; set { _StatChanged("PathogenicDefense", this._pathDefense, value); this._pathDefense = value; } }
    public int speed { get => this.level; set { _StatChanged("Speed", this._speed, value); this._speed = value; } }
    public int endurance { get => this.level; set { _StatChanged("Endurance", this._endurance, value); this._endurance = value; } }
    
    public float critRate { get => this.level; set { _CritChanged("CritRate", this._critRate, value); this._critRate = value; } }
    public float critDamage { get => this.level; set { _CritChanged("CritDamage", this._critDamage, value); this._critDamage = value; } }
}


public struct EntityStatsSchema
{
    public int level;
    public int attack;
    public int defense;
    public int pathAttack;
    public int pathDefense;
    
    public int speed;
    public int endurance;
    
    public float critRate;
    public float critDamage;
    
    public EntityStatsSchema(int? level)
    {
        this.level = level ?? 0;
        this.attack = 0;
        this.defense = 0;
        this.pathAttack = 0;
        this.pathDefense = 0;
        
        this.speed = 0;
        this.endurance = 0;
        
        this.critRate = 0;
        this.critDamage = 0;
    }

    public EntityStatsSchema SetLevel(int level)
    {
        this.level = level;
        return this;
    }

    public EntityStatsSchema SetAttack(int attack)
    {
        this.attack = attack;
        return this;
    }

    public EntityStatsSchema SetDefense(int defense)
    {
        this.defense = defense;
        return this;
    }

    public EntityStatsSchema SetPathogenicAttack(int pathAttack)
    {
        this.pathAttack = pathAttack;
        return this;
    }

    public EntityStatsSchema SetPathogenicDefense(int pathDefense)
    {
        this.pathDefense = pathDefense;
        return this;
    }

    public EntityStatsSchema SetSpeed(int speed)
    {
        this.speed = speed;
        return this;
    }

    public EntityStatsSchema SetEndurance(int endurance)
    {
        this.endurance = endurance;
        return this;
    }

    public EntityStatsSchema SetCritRate(float critRate)
    {
        this.critRate = critRate;
        return this;
    }

    public EntityStatsSchema SetCritDamage(float critDamage)
    {
        this.critDamage = critDamage;
        return this;
    }
}
