using UnityEngine;

namespace BuffSet
{
    public interface IBuff
    {
        public bool UpdateOnActive();
        public bool Initialize();
    }
    
    public abstract class Buff: MonoBehaviour
    {
        public int duration;
        
    }
}