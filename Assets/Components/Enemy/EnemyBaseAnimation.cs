using UnityEngine;

public class EnemyBaseAnimation : MonoBehaviour
{
    // Will be centralized later
    [SerializeField] private Vector3 texMotion = new Vector3(0.1f, 0.3f, 0.1f);
    [SerializeField] private Vector3 animSpeed = new Vector3(3f, 5f, 3f);
    [SerializeField] private float smoothness = 5f;
    
    private void Update()
    {
        if (GameplayManager.instance.gameSession == GameSession.Paused) return;
        this.transform.localPosition = Vector3.Lerp(
            Vector3.zero,
            new Vector3(
                Mathf.Cos(Time.time * this.animSpeed.x * this.smoothness) * this.texMotion.x, 
                Mathf.Cos(Time.time * this.animSpeed.y * this.smoothness) * this.texMotion.y, 
                Mathf.Cos(Time.time * this.animSpeed.z * this.smoothness) * this.texMotion.z
                ),
            this.smoothness * Time.deltaTime
            );
    }
}
