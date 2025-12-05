using DG.Tweening;
using TMPro;
using UnityEngine;

public class UINotification : MonoBehaviour
{
    public static UINotification instance;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private CanvasGroup canvasGroup;
    
    public void Awake() => instance = this;
    public void Notify(string text, Color color)
    {
        DOTween.Kill(53);
        Debug.Log("text assigned");
        this.text.text = text;
        this.text.color = color;
        
        this.canvasGroup.gameObject.SetActive(true);
        
        this.canvasGroup.alpha = 0f;
        this.canvasGroup.DOFade(1f, .3f).SetId(53);
            
        this.canvasGroup.DOFade(0f, .3f).SetDelay(5f).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            this.canvasGroup.gameObject.SetActive(false);
        }).SetId(53);
    }
}
