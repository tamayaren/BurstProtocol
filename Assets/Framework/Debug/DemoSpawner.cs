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
    [SerializeField] private GameObject target;

    private int enemyamount = 30;
    private Vector2 spawnInterval = new Vector2(0.85f, 2f);
    private Vector2 objRange = new Vector2(2f, 5f);

    private float toInf;
    private float t;

    private void Start()
    { spawns = new List<GameObject>();
        GameObject spawnenemies;
        this.toInf = Random.Range(this.spawnInterval.x, this.spawnInterval.y);
        for (int i = 0; i < enemyamount; i++)
        {
            spawnenemies = Instantiate(spawn);
            spawnenemies.SetActive(false);
            spawns.Add(spawnenemies);
        }
    }
    
    private void Update()
    {
        if (this.t > this.toInf)
        {
            if ( GameObject.FindGameObjectsWithTag("Enemy").Length < 16f)
            {               
                spawn.SetActive(true);
                int objRange = (int)Random.Range(this.objRange.x, this.objRange.y);
            }
            
            this.t = 0f;
            this.toInf = Random.Range(this.spawnInterval.x, this.spawnInterval.y);
        }

        this.t += Time.deltaTime;
    }

    

}
