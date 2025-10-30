using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody rb;
    private Entity entityOwner;
    private DemoSpawner spawner;

    private float speed;
    private bool started = false;
    private Vector3 direction;

    private void Start() => this.rb = this.GetComponent<Rigidbody>();

    private IEnumerator SelfDestroy()
    {
        yield return new WaitForSeconds(10f / this.speed);
        spawner.spawn.SetActive(false);
        //Destroy(this.gameObject);
    }
    
    public void Initialize(Entity owner, float speed, Vector3 direction)
    {
        this.entityOwner = owner;
        this.speed = speed;
        
        this.started = true;
        this.direction = direction;

        StartCoroutine(SelfDestroy());
    }

    private void FixedUpdate()
    {
        if (!this.started) return;
        this.rb.linearVelocity = this.direction * this.speed;
    }

    private void Update()
    {
      if (!this.started) return;
      
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
              
              Destroy(entity.gameObject);
          }
          
          Destroy(this.gameObject);
      }
    }
}
