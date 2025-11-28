using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class WaveGameplay : MonoBehaviour
{
    public int wave = 1;
    
    public int enemieskilled = 0;
    public int enemiesrequired = 10;
    public TMP_Text counter;
    
    public UnityEvent<int> WaveChanged = new UnityEvent<int>();

    [SerializeField] private GameObject ui;
    
    public void NewWave()
    {
        if (this.enemieskilled >= this.enemiesrequired)
        {
            this.wave++;
            this.enemieskilled = 0;

            this.enemiesrequired += 5 * this.wave;
            Time.timeScale = 0f;
            this.ui.SetActive(true);
        }
    }

    public void OnCursorEnter()
    {
        
    }
    public void Click()
    {
        this.ui.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Update()
    {
        this.NewWave();
        this.counter.text = this.enemieskilled + "/"+ this.enemiesrequired.ToString();
        
    }
}
