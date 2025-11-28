using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class DamageTextRenderer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    private IEnumerator SelfDestroy()
    {
        yield return new WaitForSeconds(3f);
        Destroy(this.gameObject);
    }
    
    public void Generate(float text)
    {
        this.text.text = text.ToString();

        this.text.transform.DOScale(Vector3.one * 1.5f, 1f).SetEase(Ease.OutBack);
        this.text.transform.DOMoveY(2f, 2f).SetEase(Ease.OutCirc);
        this.text.DOFade(0f, 1f).SetEase(Ease.OutQuad).SetDelay(1f);
        StartCoroutine(SelfDestroy());
    }
    
    private void LateUpdate() =>
        this.transform.LookAt(this.transform.position + (this.transform.position - Camera.main.transform.position));
}
