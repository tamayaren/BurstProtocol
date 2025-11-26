using UnityEngine;

public class Procedural : MonoBehaviour
{
    public GameObject prefab;      
    public int spawnCount = 10;    
    public float range = 100f;      

    void Start()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            // Random position within a square area
            Vector3 position = new Vector3(
                Random.Range(-range, range),
                0, // keep Y at ground level
                Random.Range(-range, range)
            );

            // Random rotation around Y axis
            Quaternion rotation = Quaternion.Euler(0, Random.Range(-90f, 90f), 0);

            // Create a new instance of the prefab
            Instantiate(prefab, position, rotation);
        }
    }
}