using System.Collections;
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

    private void Start()
    {
        this.rb = GetComponent<Rigidbody>();
    }

    private IEnumerator SelfDestroy()
    {
        yield return new WaitForSeconds(10f / this.speed);
        this.gameObject.SetActive(false);
    }
    
    public void Initialize(Entity owner, float speed, Vector3 direction, float damage)
    {
        this.entityOwner = owner;
        this.entityStats = this.entityOwner.GetComponent<EntityStats>();
        this.speed = speed;

        this.baseDamage = damage;
        this.started = true;
        this.direction = direction;

        StartCoroutine(SelfDestroy());
    }

    private void FixedUpdate()
    {
        if (!this.started) return;
        if (!this.gameObject.activeInHierarchy) return;
        this.rb.linearVelocity = this.direction * this.speed;
    }

    private void Update()
    {
      if (!this.started) return;
      if (!this.gameObject.activeInHierarchy) return;
      
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
              
              entity.AttemptDamage(damage, false);
              
              GameObject explosion = Instantiate(this.hitprefab, this.transform.position, Quaternion.identity);
              ParticleSystem particle = explosion.GetComponentInChildren<ParticleSystem>();
              particle.Play();
          }
              
          
          this.gameObject.SetActive(false);
      }
    }
}
