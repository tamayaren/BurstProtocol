using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class Entity : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private int _maxHealth;
    [SerializeField] private EntityState _entityState = EntityState.Alive;

    private GameObject hitprefab;
    private SpriteRenderer texture;
    public bool iFrame = false;
    
    public int Health
    {
        get => this._health;
        set
        {
            this._health = Mathf.Clamp(value, 0, this._maxHealth);
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

    private EntityState lastValue;
    public EntityState EntityState
    {
        get => this._entityState;
        set
        {
            this._entityState = value;

            if (value != this.lastValue)
                this.EntityStateChanged.Invoke(this._entityState);
            
            this.lastValue = this._entityState;
        }
    }
    
    public UnityEvent<int> HealthChanged = new UnityEvent<int>();
    public UnityEvent<int> MaxHealthChanged = new UnityEvent<int>();
    public UnityEvent<EntityState> EntityStateChanged = new UnityEvent<EntityState>();
    public UnityEvent<int> DamageInflicted = new UnityEvent<int>();
    public List<string> CollectedBuff = new List<string>();
    
    public IEnumerator OnExplode()
    {
        yield return new WaitForSeconds(.1f);
        Destroy(this.gameObject);
    }
    
    private void Start()
    {
        this.texture = this.transform.Find("Texture")?.GetComponent<SpriteRenderer>();
        this.HealthChanged.AddListener(health =>
        {
            if (health <= 0)
            {
                this.EntityState = EntityState.Dead;
                
                if (this.gameObject.CompareTag("Player")) return;
                GameplayManager.instance.SetScore(this.MaxHealth);
                GameplayManager.instance.SetCombo(1, this.MaxHealth);
                StartCoroutine(OnExplode());
            }
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

    public bool AttemptDamage(float damage, bool? ignoreIframe, bool isEnemy)
    {
        if (!ignoreIframe.HasValue)
            if (this.iFrame) return false;

        this.texture.color = Color.red;
        this.texture.DOColor(Color.white, .2f);
        
        this.Health -= (int)damage;
        
        this.DamageInflicted.Invoke((int)damage);
        DamageTextManager.instance.GenerateText(this.transform.position, damage, isEnemy);
        return true;
    }

    public void SetIFrame(bool value)
    {
        this.iFrame = value;
        this.texture.DOColor(this.iFrame ? Color.blue : Color.white, .2f);
    }
}

public enum EntityState
{
    Alive,
    Stunned,
    Dead,
    Invincible
}
