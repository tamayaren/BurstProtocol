using UnityEngine;
using UnityEngine.Events;

public class WaveGameplay : MonoBehaviour
{
    public int wave = 1;
    
    public int enemieskilled = 0;
    public int enemiesrequired = 10;
    
    public UnityEvent<int> WaveChanged = new UnityEvent<int>();

    public void NewWave()
    {
        if (enemieskilled == enemiesrequired)
        {
            Time.timeScale = 0; 
        }
    }
}
