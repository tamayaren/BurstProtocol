using DG.Tweening;
using DG.Tweening.Core;
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

    public GameObject player;
    public Entity playerEntity;
    public EntityStats playerStats;
    
    public int enemyKilled;
    public int killRequired;
    
    public int nextLevel;
    public int playerLevel;
    
    public UnityEvent<int> OnEnemyKilled = new UnityEvent<int>();
    public UnityEvent<int> OnKillRequired = new UnityEvent<int>();
    public UnityEvent<int> OnNextLevel = new UnityEvent<int>();
    
    private void Start()
    {
        DOTween.SetTweensCapacity(500, 100);
        DOTween.defaultEaseType = Ease.Linear;
        
        this.player = GameObject.FindGameObjectWithTag("Player");
        this.playerStats = this.player.GetComponent<EntityStats>();
        this.playerEntity = this.player.GetComponent<Entity>();

        this.nextLevel = 5;
    }
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

    public void AddNewEnemyKill()
    {
        this.enemyKilled++;
        this.killRequired++;
        
        if (this.killRequired >= this.nextLevel)
        {
            this.killRequired = 0;
            this.nextLevel = (int)(this.nextLevel * 1.2f);
            
            this.playerStats.LevelUp();
            UINotification.instance.Notify($"LEVELED UP! {this.playerStats.level-1} -> {this.playerStats.level}", Color.white);
            this.OnNextLevel.Invoke(this.nextLevel);
            
            this.playerLevel = this.playerStats.level;
            if (this.playerStats.level % 2 == 0)
            {
                BuffDistributor.instance.Distribute();
            }
        }
        this.OnEnemyKilled.Invoke(this.enemyKilled);
        this.OnKillRequired.Invoke(this.killRequired);
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
