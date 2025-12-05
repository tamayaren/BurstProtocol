using System.Collections;
using JetBrains.Annotations;
using UnityEngine;

public class Aki_Skill1_Mono : MonoBehaviour
{
    [SerializeField] public GameObject projectile;

    private Vector3 target;
    private Transform muzzle;
    private Entity owner;

    public Sprite projectileSprite;
    
    private int stack = 0;
    private void Start()
    {
        this.owner = GetComponent<Entity>();
    }

    public void Initialize(Transform muzzle)
    {
        this.muzzle = muzzle;
    }

    public IEnumerator Explode(GameObject proj)
    {
        yield return new WaitForSeconds(1f);

        Vector3 spot = proj.transform.position;
        for (int i = 0; i < 6; i++)
        {
            GameObject newProjectile = Instantiate(this.projectile, spot, Quaternion.identity);
        
            newProjectile.SetActive(true);
            Projectile projectileScript = newProjectile.GetComponent<Projectile>();
            SpriteRenderer spriteRenderer = newProjectile.GetComponent<SpriteRenderer>();

            spriteRenderer.sprite = this.projectileSprite;
            projectileScript.canDamage = true;
            projectileScript.Initialize(this.owner, Random.Range(10F, 15F), new Vector3(
                Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)
                ).normalized, 125f, false);
        }
        
        for (int i = 0; i < this.stack; i++)
            GenerateProjectile(4f + this.stack, new Vector3(
                Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized, 10f, proj.transform.position);
        Destroy(proj);
    }

    public GameObject GenerateProjectile(float speed, Vector3 dir, float baseDamage, [CanBeNull]Vector3? target)
    {
        this.target = Input.mousePosition;
        if (this.stack > 3)
        {
            this.stack = 0;
            return null;
        }
        
        GameObject proj = Instantiate(this.projectile, (Vector3)(target != null ? target : this.muzzle.position), Quaternion.identity);
        proj.SetActive(true);
        
        proj.transform.localScale *= 3f;
        Projectile projectileScript = proj.GetComponent<Projectile>();
        projectileScript.canDamage = false;
        projectileScript.Initialize(this.owner, speed, dir, baseDamage, true);
        StartCoroutine(Explode(proj));
        this.stack++;
        return proj;
    }
    
    public void PerformMove()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, new  Vector3(0, this.transform.position.y, 0));
        
        Debug.DrawRay(ray.origin, ray.direction, Color.red);
        if (!plane.Raycast(ray, out float enter)) return;
        Vector3 hitPoint = ray.GetPoint(enter);

        Vector3 dir = hitPoint - this.transform.position;
        dir.y = 0f;
        if (dir.sqrMagnitude < 0.0001f) dir = this.transform.forward; 

        dir.Normalize();

        GenerateProjectile(4f, dir, 250f, null);
    }
}
