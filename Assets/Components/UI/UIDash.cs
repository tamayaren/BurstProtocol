using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIDash : MonoBehaviour
{
    public static UIDash instance;
    [SerializeField] private Image bar;
    [SerializeField] private TextMeshProUGUI cdIndicator;
    private Color start;
    private void Awake() => instance = this;

    private Tween textTween;
    private Tween fillTween;
    private void Start()
    {
        this.start = this.bar.color;
    }
    public void Regenerate(float time)
    {
        if (this.textTween != null) this.textTween.Kill();
        if (this.fillTween != null) this.fillTween.Kill();
        
        this.bar.fillAmount = 0;

        float t = time;
        this.textTween = DOTween.To(() => time, x => t = x, 0f, time).OnUpdate(() =>
            {
                this.cdIndicator.text = (Mathf.Round(t * 10) / 10f).ToString();
            })

            .OnComplete(() =>
        {
            this.cdIndicator.text = "";
        });
        
        this.fillTween = this.bar.DOFillAmount(1f, time).OnComplete(() =>
        {
            this.bar.color = Color.white;
            this.bar.DOColor(this.start, .3f).SetEase(Ease.InQuad);
        });
    }
}
