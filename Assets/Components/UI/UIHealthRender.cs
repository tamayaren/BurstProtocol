using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthRender : MonoBehaviour
{
    [SerializeField] private Entity playerEntity;
    [SerializeField] private Image fillImage;
    [SerializeField] private TextMeshProUGUI healthText;

    private float lastHealth;
    private void OnHealthUpdate(int health)
    {
        if (this.lastHealth > health) IconDamage.instance.Shake();
        this.healthText.text = $"{Mathf.Round(this.playerEntity.Health).ToString()}/{Mathf.Round(this.playerEntity.MaxHealth)}";
        this.fillImage.fillAmount = (float)((float)this.playerEntity.Health / (float)this.playerEntity.MaxHealth);
    }
    
    private void Start()
    {
        this.lastHealth = this.playerEntity.Health;
        this.playerEntity?.HealthChanged.AddListener(OnHealthUpdate);
        OnHealthUpdate(this.playerEntity.Health);
    }
}
