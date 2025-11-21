using System.Collections.Generic;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Serialization;

public class DemoSpawner : MonoBehaviour
{
    public List<GameObject> spawns;
    public GameObject spawn;
    public GameObject target;
    
    GameObject spawnenemies;
    public Transform enemyParent;
    private int enemypoolamount = 10;
    
    private Vector2 spawnInterval = new Vector2(0.85f, 2f);
    private Vector2 objRange = new Vector2(2f, 5f);
    
    private float toInf;
    private float t;

    private void Start()
    {
        this.spawns = new List<GameObject>();
         
        this.toInf = Random.Range(this.spawnInterval.x, this.spawnInterval.y);
        
        this.target = GameObject.FindGameObjectWithTag("Player");
        for (int i = 0; i < this.enemypoolamount; i++)
        {
            GameObject spawnenemies = Instantiate(this.spawn, this.enemyParent);
            spawnenemies.SetActive(false);
            this.spawns.Add(spawnenemies);
        }
    }
    
    private void Update()
    {
        if (this.t > this.toInf)
        {
            if ( GameObject.FindGameObjectsWithTag("Enemy").Length < 16f)
            {   
                float range = Random.Range(this.objRange.x, this.objRange.y);

                GameObject objToSpawn = this.spawns.Find(obj => !obj.activeInHierarchy);

                if (objToSpawn != null)
                {
                    Vector3 spawnPos = target.transform.position + Random.insideUnitSphere * range;
                    spawnPos.y = 0;
                    objToSpawn.transform.position = spawnPos;
                    objToSpawn.SetActive(true);
                }
            }
            
            this.t = 0f;
            this.toInf = Random.Range(this.spawnInterval.x, this.spawnInterval.y);
        }

        this.t += Time.deltaTime;
    }
}
