using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class ScoreAndCombo : MonoBehaviour
{
    [SerializeField] private GameObject combo;
    
    [SerializeField] private TextMeshProUGUI comboHit;
    [SerializeField] private TextMeshProUGUI comboScore;
    [SerializeField] private TextMeshProUGUI comboRating;
    [SerializeField] private TextMeshProUGUI comboTime;

    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private TextMeshProUGUI scoreAdded;
    
    [SerializeField] private TextMeshProUGUI timer;
    private Vector3 scale;
    private int lastScore;
    private float capturedElapsed;
    private void Start()
    {
        this.lastScore = GameplayManager.instance.GetScore();
        GameplayManager.instance.ScoreChanged.AddListener(score =>
        {
            DOTween.Kill(100);
            this.scoreAdded.color = Color.green;
            this.scoreAdded.text = $"+{(score - this.lastScore).ToString()}";

            this.scoreAdded.DOFade(0f, 1f).SetId(100);
            
            this.score.color = Color.yellow;
            this.score.DOColor(Color.white, .2f).SetId(100);

            this.score.text = score.ToString();

            this.lastScore = score;
            
            this.scale = this.combo.transform.localScale;
        });
        
        GameplayManager.instance.ComboChanged.AddListener((hit, onScore) =>
        {
            Debug.Log(hit);
            if (hit == 0) {
                this.combo.SetActive(false);
                return;
            }
            
            this.combo.SetActive(true);
            this.capturedElapsed = GameplayManager.instance.timeElapsedTimer / 2f;
        
            DOTween.Kill(1001);
            this.comboHit.color = Color.green;
            this.comboScore.color = Color.yellow;
            
            this.combo.transform.localScale = new Vector3(1.15F, 1.15F, 0f);
            this.combo.transform.DOScale(new Vector3(1f, 1f, 0f), .5f).SetEase(Ease.OutBack).SetId(1001);
            this.comboScore.DOColor(Color.white, .5f).SetId(1001);
            this.comboHit.DOColor(Color.white, .5f).SetId(1001);
            
            this.comboHit.text = hit.ToString();
            this.comboScore.text = onScore.ToString();
        });
        
        this.score.text = this.lastScore.ToString();
    }

    private void Update()
    {
        TimeSpan time = TimeSpan.FromSeconds((int)GameplayManager.instance.timer);
        this.timer.text = string.Format("{0:00}:{1:00}:{2:00}", time.Hours, time.Minutes, time.Seconds);
        if (!this.combo.activeSelf) return;
        if (this.capturedElapsed > 0f)
            this.capturedElapsed -= Time.deltaTime;

        this.comboTime.text = $"{(Mathf.Round(this.capturedElapsed * 10f) / 10f)}s";
    }
}
