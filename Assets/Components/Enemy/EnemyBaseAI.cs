using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TextCore.Text;

public class EnemyBaseAI : MonoBehaviour
{
    private Entity entity;
    private NavMeshAgent agent;
    private Transform target;

    private float dynamicPollingRate = .25f;
    private float pollingTime = 0f;
    private void Start()
    {
        this.entity = GetComponent<Entity>();
        this.agent = GetComponent<NavMeshAgent>();
        
        this.agent.updateRotation = false;
    }

    private Transform FindTarget()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        
        return player?.transform;
    }
    
    private void Update()
    {
        if (!this.target && this.pollingTime >= this.dynamicPollingRate)
        {
            this.target = FindTarget();
            
            this.pollingTime = 0; 
        }

        if (this.target && this.entity.EntityState == EntityState.Alive)
        {
            this.agent.SetDestination(this.target.position);
        }
        
        this.pollingTime += Time.deltaTime;
    }
}
