using UnityEngine;
using UnityEngine.UI;

public class UIHealthRender : MonoBehaviour
{
    [SerializeField] private Entity playerEntity;
    [SerializeField] private Image fillImage;
    
    private void OnHealthUpdate(int health) =>
        this.fillImage.fillAmount = (float)((float)this.playerEntity.Health / (float)this.playerEntity.MaxHealth);
    
    private void Start()
    {
        this.playerEntity?.HealthChanged.AddListener(OnHealthUpdate);
        OnHealthUpdate(this.playerEntity.Health);
    }
}
