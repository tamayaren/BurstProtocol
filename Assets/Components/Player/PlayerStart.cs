using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStart : MonoBehaviour
{
    [SerializeField] private GameObject[] subjects;
    [SerializeField] private Transform baseUI;

    [SerializeField] private ParticleSystem particle;
    [SerializeField] private Image transition;
    
    private IEnumerator StartScene()
    {
        this.baseUI.localScale = new Vector3(2f, 2f, 0f);
        this.baseUI.gameObject.SetActive(true);

        this.transition.color = new Color(0f, 0f, 0f, 1f);
        this.transition.gameObject.SetActive(true);
        this.transition.DOFade(0f, 1.5f).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            this.transition.gameObject.SetActive(false);
        });
        
        Camera.main.transform.position = this.transform.position + new Vector3(0f, 15f, -15f);
        Camera.main.transform.rotation = Quaternion.Euler(50f, 0f, 0f);
        
        Camera.main.transform.DOMove(this.transform.position + new Vector3(0f, 10f, -10f), 1f);
        this.particle.Play();
        yield return new WaitForSeconds(1f);
        foreach (GameObject subject in subjects) subject.SetActive(true);
        
        yield return new WaitForSeconds(1f);

        this.baseUI.DOScale(new Vector3(1f, 1f, 0f), .2f).SetEase(Ease.OutQuad);
        GameplayManager.instance.StartGame();
    }

    private void Awake()
    {
        this.baseUI.gameObject.SetActive(false);
    }

    private void Start()
    {
        StartCoroutine(StartScene());
    }
}
