using UnityEngine;

public class BossEntityManager : MonoBehaviour
{
    public int bosshealth = 100;
    public WaveGameplay waves;
    public float bossAtkSpd = 1f;
    public float bossUltCD = 1f;
    
    void Start()
    {
        
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
        
    }

    void Phase2()
    {
        
    }
}
