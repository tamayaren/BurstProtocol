using System.Collections;
using System.Collections.Generic;
using Game.Mechanics;
using Mono.Cecil;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody rb;
    private Entity entityOwner;
    private EntityStats entityStats;
    private float speed;
    private bool started = false;
    private Vector3 direction;
    private float baseDamage;
    [SerializeField] private GameObject hitprefab;

    [SerializeField] private ParticleSystem particle;
    public bool canDamage = true;
    private void Start()
    {
        this.rb = GetComponent<Rigidbody>();
    }

    private IEnumerator SelfDestroy()
    {
        yield return new WaitForSeconds(20f / this.speed);
        this.gameObject.SetActive(false);
    }
    
    public void Initialize(Entity owner, float speed, Vector3 direction, float damage, bool? canSelfDestroy)
    {
        this.entityOwner = owner;
        this.entityStats = this.entityOwner.GetComponent<EntityStats>();
        this.speed = speed;

        this.baseDamage = damage;
        this.started = true;
        this.direction = direction;
        
        if (canSelfDestroy != null)
            StartCoroutine(SelfDestroy());
    }

    private bool isFireForget = false;
    private float lastFireTime = 0f;
    private float maxFireTime = 0f;
    private Transform projectileTarget;
    
    public IEnumerable<GameObject> GetNearbyObjects(string tag, float radius)
    {
        float radiusSqr = radius * radius;
        Vector3 currentPos = this.transform.position;

        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag(tag);

        foreach (GameObject obj in taggedObjects)
        {
            if (obj == null) continue;

            Vector3 offset = obj.transform.position - currentPos;
            if (offset.sqrMagnitude <= radiusSqr)
            {
                yield return obj;
            }
        }
    }
    
    private void FixedUpdate()
    {
        if (!this.started) return;
        if (!this.gameObject.activeInHierarchy) return;

        if (this.entityOwner.CompareTag("Player") || this.lastFireTime >= this.maxFireTime)
        {
            if (PlayerBuffs.instance.buffs.ContainsKey("SmartPathogen"))
            {
                SmartPathogen smartPathogen = (SmartPathogen)PlayerBuffs.instance.buffsLogics["SmartPathogen"];

                if (!this.isFireForget)
                {
                    this.maxFireTime = .8f + (0.3f * smartPathogen.stack);
                }
                else
                {
                    this.lastFireTime += Time.fixedDeltaTime;
                    
                    if (!this.projectileTarget)
                        foreach (GameObject enemy in GetNearbyObjects("Enemy", 50))
                        {
                            this.projectileTarget = enemy.transform;
                            break;
                        }
                    else
                    {
                        this.direction = (this.transform.position - this.projectileTarget.position).normalized;
                    }
                }
            }
        }
        
        this.rb.linearVelocity = this.direction * this.speed;
    }

    private void Update()
    {
      if (!this.started) return;
      if (!this.gameObject.activeInHierarchy) return;

      if (!this.canDamage) return;
      Ray ray = new Ray(this.transform.position, this.direction.normalized);
      Debug.DrawRay(ray.origin, ray.direction, Color.red);
      if (Physics.Raycast(ray, out RaycastHit hit))
      {
          if ((hit.point - this.transform.position).magnitude > 1f) return;
          
          if (hit.collider.gameObject == this.entityOwner.gameObject) return;
          if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Entity"))
          {
              Entity entity = hit.collider.gameObject.GetComponent<Entity>();
              if (!entity) return;

              int damage = StatCalculator.CalculateDamage(new DamageParameters(
                  this.baseDamage, 
                  DamageType.Physical, this.entityStats, 
                  entity.GetComponent<EntityStats>()));
              
              entity.AttemptDamage(damage, false, !this.entityOwner.CompareTag("Player"));
              if (this.entityOwner.CompareTag("Player"))
              {
                  GameplayManager.instance.SetCombo(1, (int)damage);
                  GameplayManager.instance.SetScore(damage);
              }
              
              this.particle.Play();
              GameObject explosion = Instantiate(this.hitprefab, this.transform.position, Quaternion.identity);
              ParticleSystem particle = explosion.GetComponentInChildren<ParticleSystem>();
              particle.Play();
          }
              
          
          this.gameObject.SetActive(false);
      }
    }
}
