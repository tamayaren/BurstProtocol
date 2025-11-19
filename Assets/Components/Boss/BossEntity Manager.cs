using UnityEngine;

public class BossEntityManager : MonoBehaviour
{
    public WaveGameplay waves;
    
    void Start()
    {
        
    }

    void Update()
    {
        if (waves.wave == 3)
        {
            Phase1();
        }
        
    }

    void Phase1()
    {
        
    }

    void Phase2()
    {
        
    }
}
