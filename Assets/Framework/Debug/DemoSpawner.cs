using UnityEngine;

public class DemoSpawner : MonoBehaviour
{
    [SerializeField] private GameObject spawn;
    [SerializeField] private GameObject target;

    private Vector2 spawnInterval = new Vector2(0.85f, 2f);
    private Vector2 objRange = new Vector2(2f, 5f);

    private float toInf;
    private float t;

    private void Start()
    {
        this.toInf = Random.Range(this.spawnInterval.x, this.spawnInterval.y);
    }
    private void Update()
    {
        if (this.t > this.toInf)
        {
            if (GameObject.FindGameObjectsWithTag("Enemy").Length < 16f)
            {
                int objRange = (int)Random.Range(this.objRange.x, this.objRange.y);
                for (int i = 0; i < objRange; i++)
                {
                    Instantiate(this.spawn, (this.target.transform.position + new Vector3(Random.Range(-3f, 3f), this.target.transform.position.y, Random.Range(-3f, 3f)) * Random.Range(3, 12)), Quaternion.identity);
                }
            }
            
            
            this.t = 0f;
            this.toInf = Random.Range(this.spawnInterval.x, this.spawnInterval.y);
        }

        this.t += Time.deltaTime;
    }

}
