using UnityEngine;
using UnityEngine.Events;

public class Entity : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private int _maxHealth;

    [SerializeField] private EntityState _entityState = EntityState.Alive;

    public int Health
    {
        get => this._health;
        set
        {
            this._health = value;
            
            this.HealthChanged.Invoke(this._health);
        }
    }

    public int MaxHealth
    {
        get => this._maxHealth;
        set
        {
            this._maxHealth = value;
            
            this.MaxHealthChanged.Invoke(this._maxHealth);
        }
    }

    public EntityState EntityState
    {
        get => this._entityState;
        set
        {
            this._entityState = value;
            
            this.EntityStateChanged.Invoke(this._entityState);
        }
    }
    
    public UnityEvent<int> HealthChanged = new UnityEvent<int>();
    public UnityEvent<int> MaxHealthChanged = new UnityEvent<int>();
    public UnityEvent<EntityState> EntityStateChanged = new UnityEvent<EntityState>();
    
    
}

public enum EntityState
{
    Alive,
    Stunned,
    Dead,
}
