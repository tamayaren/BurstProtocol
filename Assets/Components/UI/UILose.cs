using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UILose : MonoBehaviour
{
    public static UILose instance;
    
    private void Awake() => instance = this;
    [SerializeField] private Image parent;
    [SerializeField] private CanvasGroup canvasGroup;

    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private TextMeshProUGUI timer;
    [SerializeField] private TextMeshProUGUI enemies;
    
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Exit()
    {
        SceneManager.LoadScene(0);
    }

    public void Lose()
    {
        this.parent.gameObject.SetActive(true);
        this.canvasGroup.alpha = 0f;
        this.canvasGroup.DOFade(1f, 1f);

        float s = 0f;
        float t = 0f;
        float e = 0f;
        
        DOTween.To(() => 0f, x => s = x, GameplayManager.instance.GetScore(), 1f).OnUpdate(() =>
            {
                this.score.text = Mathf.Round(s).ToString();
            }).SetEase(Ease.OutQuad);
        
        DOTween.To(() => 0f, x => t = x, GameplayManager.instance.timer, 1f).OnUpdate(() =>
        {
            TimeSpan time = TimeSpan.FromSeconds((int)t);
            this.timer.text = string.Format("{0:00}:{1:00}:{2:00}", time.Hours, time.Minutes, time.Seconds);
        }).SetEase(Ease.OutQuad);
        
        DOTween.To(() => 0f, x => e = x, GameplayManager.instance.enemyKilled, 1f).OnUpdate(() =>
        {
            this.score.text = Mathf.Round(e).ToString();
        }).SetEase(Ease.OutQuad);
    }
}
