using UnityEngine;
using UnityEngine.Events;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager instance;
    
    private void Awake() => instance = this;

    public bool gameplayStarted;
    public GameSession gameSession;
    
    public float timer;
    private int score;
    public UnityEvent<int> ScoreChanged = new UnityEvent<int>();

    private int comboHit;
    private int comboScore;

    public UnityEvent<int, int> ComboChanged = new UnityEvent<int, int>();
    public float timeElapsed;
    public float timeElapsedTimer = 5f;
    private void Update()
    {
        if (this.gameSession == GameSession.Paused) return;
        if (!this.gameplayStarted) return;
        this.timer += Time.deltaTime;
        this.timeElapsed += Time.deltaTime;
        
        if (this.comboHit > 0)
            this.timeElapsed += Time.deltaTime;

        if (this.timeElapsed >= this.timeElapsedTimer)
        {
            this.comboHit = 0;
            this.comboScore = 0;
            
            this.ComboChanged.Invoke(this.comboHit, this.comboScore);
            this.timeElapsed = 0f;
        }
    }

    public void SetScore(int score)
    {
      this.score += score;  
      this.ScoreChanged.Invoke(this.score);
    } 
    
    public int GetScore() => this.score;

    public void StartGame()
    {
        this.gameplayStarted = true;
        this.gameSession = GameSession.Running;
        CameraMain.instance.subjectLooking = true;  
    }

    public void SetCombo(int hit, int score)
    {
        this.comboHit = (int)Mathf.Clamp(this.comboHit+hit, 0f, Mathf.Infinity);
        this.comboScore = (int)Mathf.Clamp(this.comboScore+score, 0f, Mathf.Infinity);

        this.timeElapsed = 0f;
        this.ComboChanged.Invoke(this.comboHit, this.comboScore);
    }
    
    public int GetComboHit() => this.comboHit;
    public int GetComboScore() => this.comboScore;
}

public enum GameSession
{
    Running,
    Paused
}
