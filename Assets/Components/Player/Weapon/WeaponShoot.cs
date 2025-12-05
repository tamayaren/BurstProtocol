using System.Collections.Generic;
using UnityEngine;

public class WeaponShoot : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    private readonly int projectileAmount = 100;

    [SerializeField] private Transform muzzle;
    
    private List<GameObject> projectiles;
    private GameObject projectileParent;
    private GameObject projectileHolder; 
    public Entity owner;
    
    private void Start()
    {
        this.projectileParent = GameObject.Find("Projectiles");
        
        this.projectiles = new List<GameObject>();
        
        for (int i = 0; i < this.projectileAmount; i++)
        {
            GameObject projectileHolder = Instantiate(this.bullet,this.muzzle.position,Quaternion.identity, this.projectileParent.transform);
            projectileHolder.SetActive(false);
            this.projectiles.Add(projectileHolder);
        }
    }
    
    public void Shoot(Sprite sprite, float speed, Vector3 target, CharacterSystemMetadata character)
    {
        Ray ray = Camera.main.ScreenPointToRay(target);
        Plane plane = new Plane(Vector3.up, new  Vector3(0, this.transform.position.y, 0));
        
        Debug.DrawRay(ray.origin, ray.direction, Color.red);
        if (!plane.Raycast(ray, out float enter)) return;
        Vector3 hitPoint = ray.GetPoint(enter);

        Vector3 dir = hitPoint - this.transform.position;
        dir.y = 0f;
        if (dir.sqrMagnitude < 0.0001f) dir = this.transform.forward; 

        dir.Normalize();

        GameObject proj = this.projectiles.Find(obj => !obj.activeInHierarchy);
        
        if (proj != null)
        {
            proj.transform.position = this.muzzle.position;
            proj.SetActive(true);
        }
        
        SpriteRenderer spriteRenderer = proj.GetComponent<SpriteRenderer>();
        Projectile initializer = proj.GetComponent<Projectile>();
        
        spriteRenderer.sprite = sprite;
        initializer.Initialize(this.owner, speed, dir, character.baseAttackDamage, false);
    }
}
