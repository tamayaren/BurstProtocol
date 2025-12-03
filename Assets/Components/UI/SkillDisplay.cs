using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillDisplay : MonoBehaviour
{
    public static SkillDisplay instance;
    [SerializeField] private GameObject[] skillObjects;

    [SerializeField] private Image[] skillFill;
    [SerializeField] private TextMeshProUGUI[] skillCD;

    private void Awake() => instance = this;

    // private void Start()
    // {
    //     for (int i = 0; i < this.skillObjects.Length; i++)
    //     {
    //         GameObject skillObject = this.skillObjects[i];
    //
    //         if (skillObject)
    //         {
    //             this.skillFill[i] = skillObject.GetComponent<Image>();
    //             this.skillCD[i] = skillObject.GetComponent<TextMeshProUGUI>();
    //
    //             this.skillFill[i].fillAmount = 0f;
    //             this.skillCD[i].text = "";
    //         }
    //     }
    // }

    public void Cooldown(int index, float f)
    {
        Image image = this.skillFill[index];
        TextMeshProUGUI text = this.skillCD[index];

        if (image && text)
        {
            DOTween.Kill(index + 551);

            image.fillAmount = 1f;
            image.DOFillAmount(0f, f).SetId(index + 551);

            
            Debug.Log("Fufilled");
            float time = f;
            DOTween.To(() => time, x => time = x, 0f, time).OnUpdate(() =>
                {
                    text.text = (Mathf.Round(time * 10) / 10f).ToString();
                })

                .OnComplete(() =>
                {
                    text.text = "";
                }).SetId(index + 551);
            
        }
    }
}
