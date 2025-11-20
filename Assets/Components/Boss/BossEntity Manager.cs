using UnityEngine;

public class BossEntityManager : MonoBehaviour
{
    public GameObject bossPrefab;
    public int bosshealth = 100;
    public WaveGameplay waves;
    
    
    void Start()
    {
        bossPrefab.SetActive(false);
    }

    void Update()
    {
        if (waves.wave == 3)
        {
            Phase1();
        }

        if (bosshealth < 50)
        {
            Phase2();
        }
    }

    void Phase1()
    {
        bossPrefab.SetActive(true);
    }

    void Phase2()
    {
        
    }
}
