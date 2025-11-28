using UnityEngine;

public class BossEntityManager : MonoBehaviour
{
    public GameObject bossPrefab;
    public int bosshealth = 100;
    public WaveGameplay waves;
    
    
    void Start()
    {
        this.bossPrefab.SetActive(false);
    }

    void Update()
    {
        if (this.waves.wave == 3)
        {
            Phase1();
        }

        if (this.bosshealth < 50)
        {
            Phase2();
        }
    }

    void Phase1()
    {
        this.bossPrefab.SetActive(true);
    }

    void Phase2()
    {
        
    }
}
