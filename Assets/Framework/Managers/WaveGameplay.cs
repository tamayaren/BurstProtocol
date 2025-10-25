using UnityEngine;
using UnityEngine.Events;

public class WaveGameplay : MonoBehaviour
{
    public int wave = 1;

    public UnityEvent<int> WaveChanged = new UnityEvent<int>();

    public void NewWave()
    {
        
    }
}
