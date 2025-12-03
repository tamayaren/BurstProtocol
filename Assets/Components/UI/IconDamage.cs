using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class IconDamage : MonoBehaviour
{
    public static IconDamage instance;
    [SerializeField] private Image icon;
    private void Awake() => instance = this;

    private Tween shake;
    public void Shake()
    {
        if (this.shake == null) this.shake.Kill();
        DOTween.Kill(13);
        this.icon.color = Color.red;
        this.icon.DOColor(Color.white, 0.5f).SetEase(Ease.OutQuad).SetAutoKill(true).SetId(13);
        
        this.shake = DOTween.Shake(() => this.transform.position, x => this.transform.position = x, .5f, new Vector3(2f, 0, 0f), 30);
        this.shake.SetId(13);
    }
}
