using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class MainMenuManager : MonoBehaviour
{
    public Transform orb,logo;
    public GameObject shopUI, charUI;
    public Button shopButton,charButton, playStart, exitGame, closeShop, closeChar;
    public void Start()
    {
        shopUI.SetActive(false);
        charUI.SetActive(false);
        // playStart.onClick.AddListener(startGame);
        // exitGame.onClick.AddListener(closeGame);
        // shopButton.onClick.AddListener(OpenShopUI);
        // closeShop.onClick.AddListener(CloseShopUI);
        // charButton.onClick.AddListener(openCharUI);
        // closeChar.onClick.AddListener(closeCharUI);
    }

    public void Update()
    {
        orb.DORotate(orb.rotation.eulerAngles + Vector3.up * 90, 5f).SetEase(Ease.Linear);
        logo.DOScale(1.2f, 3f);
    }

    public void startGame()
    {
        Debug.Log("StartGame");
        SceneManager.LoadScene("Debug");
    }

    public void closeGame()
    {
        Debug.unityLogger.Log("CloseGame");
        Application.Quit();
    }
    public void OpenShopUI()
    {
        Debug.Log("OpenShopUI");
        shopUI.SetActive(true);
    }

    public void CloseShopUI()
    {
        Debug.unityLogger.Log("CloseShopUI");
        shopUI.SetActive(false);
    }

    public void openCharUI()
    {
        Debug.unityLogger.Log("openCharUI");
        charUI.SetActive(true);
    }

    public void closeCharUI()
    {
        Debug.unityLogger.Log("closeCharUI");
        charUI.SetActive(false);
    }
}
