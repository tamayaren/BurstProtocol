using UnityEngine;

public class WeaponShoot : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    private GameObject projectile;

    public Entity owner;
    
    private void Start()
    {
        this.projectile = GameObject.Find("Projectiles");
    }
    
    public void Shoot(Sprite sprite, float speed, Vector3 target)
    {
        Ray ray = Camera.main.ScreenPointToRay(target);
        Plane plane = new Plane(Vector3.up, new  Vector3(0, this.transform.position.y, 0));
        
        Debug.DrawRay(ray.origin, ray.direction, Color.red);
        Debug.Log("out1");
        if (!plane.Raycast(ray, out float enter)) return;
        Vector3 hitPoint = ray.GetPoint(enter);

        Vector3 dir = hitPoint - this.transform.position;
        dir.y = 0f;
        if (dir.sqrMagnitude < 0.0001f) dir = this.transform.forward; 

        dir.Normalize();

        GameObject proj = Instantiate(this.bullet, this.transform.position, Quaternion.identity, this.projectile.transform);
        
        Debug.Log("init");
        SpriteRenderer spriteRenderer = proj.GetComponent<SpriteRenderer>();
        Projectile initializer = proj.GetComponent<Projectile>();
        
        spriteRenderer.sprite = sprite;
        initializer.Initialize(this.owner, speed, dir);
    }
}
