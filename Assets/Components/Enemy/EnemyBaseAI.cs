using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TextCore.Text;

public class EnemyBaseAI : MonoBehaviour
{
    private Entity entity;
    private NavMeshAgent agent;
    private Transform target;
    private EntityStats entityStats;

    private float dynamicPollingRate = .25f;
    private float pollingTime = 0f;
    private bool enemyVoided = false;

    private bool canDamageAgain = true;
    private float tTick = 0f;

    private bool freeRadicalMode = false;
    private void Start()
    {
        this.entity = GetComponent<Entity>();
        this.agent = GetComponent<NavMeshAgent>();
        this.entityStats = this.entity.GetComponent<EntityStats>();

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
        
        this.agent.updateRotation = false;
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
        if (GameplayManager.instance.gameSession == GameSession.Paused) return;
        if (this.entity.EntityState == EntityState.Dead) return;
        
        if (!this.target && this.pollingTime >= this.dynamicPollingRate)
        {
            this.target = FindTarget();
            
            this.pollingTime = 0; 
        }

        if (this.target && this.entity.EntityState == EntityState.Alive)
        {
            this.agent.SetDestination(this.target.position);

            if (this.agent.remainingDistance <= this.agent.stoppingDistance && this.canDamageAgain)
            {
                Entity playerEntity = this.target.GetComponent<Entity>();

                playerEntity.AttemptDamage(10f, false, true);
                this.canDamageAgain = false;
            }
        }

        if (!this.canDamageAgain)
        {
            this.tTick += Time.deltaTime;
            if (this.tTick >= this.dynamicPollingRate)
            {
                this.tTick = 0;
                this.canDamageAgain = true;
            }
        }
        
        this.pollingTime += Time.deltaTime;
    }
}
