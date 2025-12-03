using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class DamageTextRenderer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    
    public void Generate(float text, bool isEnemy)
    {
        this.text.text = text.ToString();
        
        this.text.color = isEnemy ? Color.red : Color.white;
        
        Sequence seq = DOTween.Sequence();
        seq
            .Join(this.text.transform.DOScale(Vector3.one * 1.5f, 1f).SetEase(Ease.OutBack))
            .Join(this.text.transform.DOBlendableLocalMoveBy(new Vector3(
                Random.Range(-1f, 1f), Random.Range(-1f, 2f), Random.Range(-1f, 1f)), 1f).SetEase(Ease.OutCirc))
            .Join(this.text.DOFade(0f, 1f).SetEase(Ease.OutQuad).SetDelay(1f))
            .Play()
            .onComplete = () => Destroy(this.gameObject);
    }
    
    private void LateUpdate() =>
        this.transform.LookAt(this.transform.position + (this.transform.position - Camera.main.transform.position));
}
