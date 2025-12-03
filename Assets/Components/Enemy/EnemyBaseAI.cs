using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TextCore.Text;

public class EnemyBaseAI : MonoBehaviour
{
    private Entity entity;
    private NavMeshAgent agent;
    private Transform target;
    private WaveGameplay gameplay;

    private float dynamicPollingRate = .25f;
    private float pollingTime = 0f;
    private bool enemyVoided = false;

    private bool canDamageAgain = true;
    private float tTick = 0f;
    private void Start()
    {
        this.entity = GetComponent<Entity>();
        this.agent = GetComponent<NavMeshAgent>();
        this.gameplay = GameObject.FindObjectOfType<WaveGameplay>();

        this.agent.updateRotation = false;
        this.entity.EntityStateChanged.AddListener(state =>
        {
            if (state == EntityState.Dead && !this.enemyVoided)
            {
                this.enemyVoided = true;
                this.gameplay.enemieskilled++;
                this.gameObject.SetActive(false);
            }
        });
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
    
    private void Update()
    {
        if (GameplayManager.instance.gameSession == GameSession.Paused) return;
        
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

                playerEntity.AttemptDamage(1f, false, true);
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
