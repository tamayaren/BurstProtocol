using DG.Tweening;
using TMPro;
using UnityEngine;

public class UIKillCount : MonoBehaviour
{
    public TextMeshProUGUI counter;
    public TextMeshProUGUI totalKills;

    private void UpdateCounter(int c)
    {
        DOTween.Kill(130);
        this.counter.color = Color.green;
        this.counter.DOColor(Color.white, .3f).SetEase(Ease.OutQuad).SetId(130);

        this.counter.text = (GameplayManager.instance.nextLevel - GameplayManager.instance.killRequired).ToString();
    }

    private void UpdateTotal(int c)
    {
        DOTween.Kill(1333);
        this.totalKills.color = Color.yellow;
        this.totalKills.DOColor(Color.white, .3f).SetEase(Ease.OutQuad).SetId(1333);

        this.totalKills.text = c.ToString();
    }
    
    private void Start()
    {
        GameplayManager.instance.OnKillRequired.AddListener(UpdateCounter);
        GameplayManager.instance.OnEnemyKilled.AddListener(UpdateTotal);
        
        UpdateCounter(GameplayManager.instance.killRequired);
        UpdateCounter(GameplayManager.instance.enemyKilled);
    }
}
