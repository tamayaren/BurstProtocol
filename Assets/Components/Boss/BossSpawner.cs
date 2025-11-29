using UnityEngine;
public class BossSpawner : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject bossDrone;
    public WaveGameplay waves;
    void Start()
    {
        
    }
    void Update()
    {
        if (waves.wave == 3)
        {
            spawnPoint = Instantiate(bossDrone, spawnPoint.position, Quaternion.identity).transform;
        }
        
        
    }
}
