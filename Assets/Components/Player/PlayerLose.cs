using UnityEngine;

public class PlayerLose : MonoBehaviour
{
    private Entity entity;

    [SerializeField]
    private Transform texture;

    [SerializeField] private Transform weapon;
    

    private void Start()
    {
        this.entity = this.GetComponent<Entity>();
        
        this.entity.HealthChanged.AddListener(health =>
        {
            if (health <= 0)
            {
                this.texture.gameObject.SetActive(false);
                this.weapon.gameObject.SetActive(false);
                GameplayManager.instance.gameSession = GameSession.Paused;
                UILose.instance.Lose();
            }
        });
    }
}
