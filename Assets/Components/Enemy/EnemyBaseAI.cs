using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TextCore.Text;

public class EnemyBaseAI : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Sprite bulletSprite;
    
    private Entity entity;
    private NavMeshAgent agent;
    private Transform target;
    private EntityStats entityStats;

    private float dynamicPollingRate = .25f;
    private float canDamageFloatTime = 1f;
    private float pollingTime = 0f;
    private bool enemyVoided = false;

    private bool canDamageAgain = true;
    private float tTick = 0f;

    private float randomSeed;
    private bool freeRadicalMode = false;
    [SerializeField] private Sprite[] textures;
    [SerializeField] private ParticleSystem explosion;
    private int enemyType = 0;
    private void Start()
    {
        this.entity = GetComponent<Entity>();
        this.agent = GetComponent<NavMeshAgent>();
        this.entityStats = this.entity.GetComponent<EntityStats>();

        this.enemyType = Random.Range(0, 3);
        Debug.Log($"TYPE SPAWNED {this.enemyType}");
        if (PlayerBuffs.instance.buffs.ContainsKey("MinuteMan"))
        { 
            MinuteMan buff = (MinuteMan)PlayerBuffs.instance.buffsLogics["MinuteMan"];

            int chance = Random.Range(1, 100);
            if (chance < 3 * buff.stack)
                this.entity.Health = 10;
        }
        
        if (PlayerBuffs.instance.buffs.ContainsKey("HighonFreeRadicals"))
        {
            HighonFreeRadicals buff = (HighonFreeRadicals)PlayerBuffs.instance.buffsLogics["HighonFreeRadicals"];
            
            this.entityStats.defense -= (int)(this.entityStats.defense * (.1*buff.stack));
            this.freeRadicalMode = true;
        }
        
        this.randomSeed = Random.Range(0, int.MaxValue);
        this.agent.updateRotation = false;
        switch (this.enemyType)
        {
            default:
            case 0:
                this.agent.speed = 3.5f;
                this.entity.Health = 600;
                this.entity.MaxHealth = 600;

                this.entityStats.attack = 300;
                break;
            case 1:
                this.agent.speed = 6f;
                this.entity.Health = 300;
                this.entity.MaxHealth = 300;
                this.entityStats.attack = 650;
                break;
            case 2:
                this.agent.speed = 12f;
                this.entity.Health = 15;
                this.entity.MaxHealth = 15;
                this.entityStats.attack = 2500;
                break;
        }
        
        SpriteRenderer texture = this.transform.Find("Texture").GetComponent<SpriteRenderer>();
        texture.sprite = this.textures[this.enemyType];
    }

    public void Reinitialize()
    {
        this.enemyVoided = false;
    }
    private Transform FindTarget()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        
        return player?.transform;
    }

    private void FixedUpdate()
    {
        if (this.freeRadicalMode)
        {
            int roll = Random.Range(1, 2048);

            if (roll < 16)
            {
                this.transform.position += new Vector3(Random.Range(-5f, 5f), 0f, Random.Range(-5f, 5f));
            }
        }
    }
    
    private void Update()
    {
        if (GameplayManager.instance.gameSession == GameSession.Paused)
        { 
            this.agent.isStopped = true;
            return;
        };
        if (this.entity.EntityState == EntityState.Dead) return;
        
        if (!this.target && this.pollingTime >= this.dynamicPollingRate)
        {
            this.target = FindTarget();
            
            this.pollingTime = 0; 
        }

        this.agent.isStopped = false;
        if (this.target && this.entity.EntityState == EntityState.Alive)
        {
            switch (enemyType)
            {
                default:
                case 0:
                    this.agent.SetDestination(this.target.position);

                    if (this.agent.remainingDistance <= this.agent.stoppingDistance * 64f && this.canDamageAgain)
                    {
                        
                        GameObject proj = WeaponShoot.instance.projectiles.Find(obj => !obj.activeInHierarchy);

                        if (proj != null)
                        {
                            proj.transform.position = this.transform.position;
                            proj.SetActive(true);
                        }
                
                        SpriteRenderer spriteRenderer = proj.GetComponent<SpriteRenderer>();
                        Projectile initializer = proj.GetComponent<Projectile>();
                        
                        spriteRenderer.sprite = this.bulletSprite;
                        spriteRenderer.color = Color.red;

                        initializer.enemyProjectile = true;
                        initializer.Initialize(this.entity, 6f, (this.target.position + new Vector3(Random.Range(-2f, 2f), 0f, Random.Range(-2f, 2f)) - this.transform.position).normalized, 
                            Random.Range(25, 45) * (1+(GameplayManager.instance.playerLevel/40f)), false);
                        this.canDamageAgain = false;
                    }

                    break;
                case 1:
                    this.agent.SetDestination(this.target.position + new Vector3(Mathf.Cos(Time.time + this.randomSeed / 50f) * 100f, 0f, Mathf.Cos(Time.time + this.randomSeed / 50f) * 100f));
                    
                    if (this.agent.remainingDistance <= this.agent.stoppingDistance * 128 && this.canDamageAgain)
                    {
                        for (int i = 0; i < 6; i++)
                        {
                            GameObject proj = WeaponShoot.instance.projectiles.Find(obj => !obj.activeInHierarchy);

                            if (proj != null)
                            {
                                proj.transform.position = this.transform.position;
                                proj.SetActive(true);
                            }
                    
                            SpriteRenderer spriteRenderer = proj.GetComponent<SpriteRenderer>();
                            Projectile initializer = proj.GetComponent<Projectile>();
                            
                            spriteRenderer.sprite = this.bulletSprite;
                            spriteRenderer.color = Color.red;

                            initializer.enemyProjectile = true;
                            initializer.Initialize(this.entity, Random.Range(8f, 12f), (this.target.position + new Vector3(Random.Range(-2048f, 2048f), 
                                0f, Random.Range(-2048f, 2048f)) - this.transform.position).normalized, 
                                Random.Range(35f, 45f) * (1+(GameplayManager.instance.playerLevel/40f)), false);
                            this.canDamageAgain = false;
                        }   
                    }
                    break;
                case 2:
                    this.agent.SetDestination(this.target.position);

                    if (Vector3.Distance(this.transform.position, this.target.position) < 1f && this.canDamageAgain)
                    {
                        Entity playerEntity = this.target.GetComponent<Entity>();

                        playerEntity.AttemptDamage(100f * (1+(GameplayManager.instance.playerLevel/10f)), false, true);
                        this.entity.Health = 0;
                        
                        this.explosion.transform.SetParent(null);
                        this.explosion.Play();
                        this.canDamageAgain = false;
                    }

                    break;
            }
        }
        
        if (!this.canDamageAgain)
        {
            this.tTick += Time.deltaTime;
            if (this.tTick >= this.canDamageFloatTime)
            {
                this.tTick = 0;

                switch (this.enemyType)
                {
                    default:
                    case 0:
                        this.canDamageFloatTime = Random.Range(0.8f, 1.2f);
                        break;
                    case 1:
                        this.canDamageFloatTime = Random.Range(1.3f, 2f);
                        break;
                }
                this.canDamageAgain = true;
            }
        }
        
        this.pollingTime += Time.deltaTime;
    }
}
