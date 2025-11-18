using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button cleanse,mitoloy,synthesis,settings;
  
    void Start()
    {
        cleanse.onClick.AddListener(delegate { SceneManager.LoadScene(0); });
    }
    
    void Update()
    {
        
    }
}
