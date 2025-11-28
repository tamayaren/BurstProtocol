using System;
using UnityEngine;
using UnityEngine.Events;

public class Entity : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private int _maxHealth;
    [SerializeField] private EntityState _entityState = EntityState.Alive;

    public bool iFrame = false;
    
    public int Health
    {
        get => this._health;
        set
        {
            this._health = value;
            if (this._health <= 0f)
                this.EntityState = EntityState.Dead;
            
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
    public UnityEvent<int> DamageInflicted = new UnityEvent<int>();

    private void Start()
    {
        this.HealthChanged.AddListener(health =>
        {
            if (health <= 0)
                this.EntityState = EntityState.Dead;
        });
    }

    public void FixedUpdate()
    {
        Vector3 vector3 = this.transform.position;
        vector3.y = 1f;
        this.transform.position = vector3;
    }

    private void OnEnable()
    {
        this.EntityState = EntityState.Alive;
        this.Health = this.MaxHealth;
    }

    public bool AttemptDamage(float damage, bool? ignoreIframe)
    {
        if (!ignoreIframe.HasValue)
            if (this.iFrame) return false;
        
        this.Health -= (int)damage;
        Debug.Log("Damage inflicted");
        
        return true;
    }
}

public enum EntityState
{
    Alive,
    Stunned,
    Dead,
    Invincible
}
