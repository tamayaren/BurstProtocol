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
    
    [SerializeField] [Range(0, 80)] private float _critRate = 5f;
    [SerializeField] [Range(0, 400)] private float _critDamage = 10f;
    
    public UnityEvent<string, int, int> StatChanged = new UnityEvent<string, int, int>();
    public UnityEvent<string, float, float> CritChanged = new UnityEvent<string, float, float>();
    public UnityEvent<int, int> LevelChanged = new UnityEvent<int, int>();

    public float dashRange = 1f;
    public float dashCooldown = 1f;
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
