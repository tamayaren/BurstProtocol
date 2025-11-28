using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public static HitboxData Cast(GameObject owner, Vector3 position, Vector3 size, Quaternion rotation, LayerMask mask)
    {
        Collider[] onHit = Physics.OverlapBox(position, size, rotation, mask);

        return new HitboxData
        {
            position = position,
            size = size,
            onHit = onHit,
            owner = owner,
            result = onHit.Length > 0 ? HitboxResult.OnHit : HitboxResult.NoHit
        };
    }
    
    public struct HitboxData
    {
        public Collider[] onHit;
        public GameObject owner;
        public Vector3 position;
        public Vector3 size;
        public HitboxResult result;
    }

    public enum HitboxResult
    {
        NoHit,
        OnHit
    }

}
