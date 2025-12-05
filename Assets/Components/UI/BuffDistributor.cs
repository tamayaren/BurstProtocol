using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BuffDistributor : MonoBehaviour
{
    public static BuffDistributor instance;
    [SerializeField] private BuffMetadata[] buffs;
    
    [SerializeField] private Button[] buttons;
    [SerializeField] private GameObject parent;
    
    private void Awake() => instance = this;

    public IEnumerator Create()
    {
        this.parent.SetActive(true);
        CanvasGroup parent = this.parent.GetComponent<CanvasGroup>();

        parent.alpha = 0f;
        parent.DOFade(1f, .25f).SetEase(Ease.OutQuad);
        
        BuffMetadata[] buffs = new[]
        {
            this.buffs[Random.Range(0, this.buffs.Length)],
            this.buffs[Random.Range(0, this.buffs.Length)],
            this.buffs[Random.Range(0, this.buffs.Length)],
        };

        int index = 0;
        bool hasChosen = false;
        foreach (Button button in this.buttons)
        {
            BuffMetadata buff = buffs[index];
            
            CanvasGroup canvasGroup = button.GetComponent<CanvasGroup>();
            canvasGroup.alpha = 0f;
            if (buff)
            {
                TextMeshProUGUI buffTitle = button.transform.Find("Title").GetComponent<TextMeshProUGUI>();
                TextMeshProUGUI buffDescription = button.transform.Find("Description").GetComponent<TextMeshProUGUI>();
                Image buffImage = button.transform.Find("Icon").GetComponent<Image>();
                
                buffImage.sprite = buff.icon;
                buffTitle.text = buff.fullName;
                buffDescription.text = buff.buffDescription;
                yield return new WaitForSeconds(0.1f * index);
                canvasGroup.DOFade(1f, .2f).SetEase(Ease.OutQuad).OnComplete(() =>
                {
                    button.onClick.AddListener(() =>
                    {
                        if (hasChosen) return;
                        Debug.Log(buff.name);
                        try
                        {
                            PlayerBuffs.instance.AddBuff(buff.name, buff);
                        }
                        catch (Exception e)
                        {
                            Debug.LogWarning(e.Message);
                            Debug.LogWarning(e.StackTrace);
                        }

                        
                        GameplayManager.instance.gameSession = GameSession.Running;
                        parent.DOFade(0f, .25f).SetEase(Ease.OutQuad).OnComplete(() =>
                        {
                            this.parent.SetActive(false);
                        });
                        
                        hasChosen = true;
                        button.onClick.RemoveAllListeners();
                    });
                });
                index++;
            }
        }
    }
    public void Distribute()
    {
        GameplayManager.instance.gameSession = GameSession.Paused;
        StartCoroutine(Create());
    }
}
