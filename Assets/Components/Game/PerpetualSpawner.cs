using UnityEngine;

public class PerpetualSpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemy;

    private EntityStats playerStats;
    private GameObject player;
    
    public int baseSpawn = 2;
    public float spawnElapsed = 1f;

    public int totalEnemies;
    public int maxEnemies = 5;
    [SerializeField] private float elapsed;

    private Transform parent;
    private void Start()
    {
        this.player = GameObject.FindGameObjectWithTag("Player");
        this.playerStats = this.player.GetComponent<EntityStats>();
        
        this.parent = GameObject.Find("Enemies").transform;
        this.playerStats.LevelChanged.AddListener((oldA, newA) =>
        {
            this.baseSpawn = 2 * this.playerStats.level;
            this.maxEnemies = 5 + Mathf.Clamp((this.playerStats.level), 1, 30);
        });
    }
    
    private void FixedUpdate() => 
        this.totalEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;

    private void Update()
    {
        if (!GameplayManager.instance.gameplayStarted) return;

        if (this.totalEnemies >= this.maxEnemies) return;
        if (this.elapsed >= this.spawnElapsed)
        {
            for (int i = 0; i < this.maxEnemies - this.totalEnemies; i++)
            {
                if (this.totalEnemies >= this.maxEnemies) break;
                Vector2 dir2D = Random.insideUnitCircle.normalized;
                float objOffsetDist = Random.Range(8f, 16f);
                    
                Vector3 spawnPos = this.player.transform.position + (new Vector3(dir2D.x, 0f, dir2D.y)) * objOffsetDist;
                spawnPos.y = 0;
                
                GameObject enemy = Instantiate(this.enemy, spawnPos, Quaternion.identity, this.parent);
                Entity enemyEntity = enemy.GetComponent<Entity>();
                
                enemyEntity.EntityStateChanged.AddListener(state =>
                {
                    if (state == EntityState.Dead)
                    {
                        GameplayManager.instance.AddNewEnemyKill();
                    }
                });
                
                this.totalEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
            }
            
            this.elapsed = 0f;
        }
        
        this.elapsed += Time.deltaTime;
    }
}
