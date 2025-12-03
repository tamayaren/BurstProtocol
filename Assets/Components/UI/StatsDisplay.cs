using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Game.Mechanics.Organelles;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class StatsDisplay : MonoBehaviour
{
    [SerializeField] private EntityStats playerStats;
    
    [SerializeField] private GameObject[] statLists;
    [SerializeField] private List<string> statNames;
    [SerializeField] private List<string> statInternalNames;
    
    [SerializeField] private List<TextMeshProUGUI> statValues;
    [SerializeField] private List<TextMeshProUGUI> statAdded;

    private void Start() => StartCoroutine(LateStart());
    private IEnumerator LateStart()
    {
        yield return new WaitForSeconds(0.2f);
        for (int i = 0; i < this.statLists.Length; i++)
        {
            GameObject element = this.statLists[i];
            if (element == null) continue;
            
            GameObject value = element.transform.Find("Value").gameObject;
            GameObject added = element.transform.Find("Added").gameObject;
            
            if (value && added)
            {
                TextMeshProUGUI text = value.GetComponent<TextMeshProUGUI>();
                
                this.statValues.Add(text);
                this.statAdded.Add(added.GetComponent<TextMeshProUGUI>());

                float stat = this.playerStats.GetStatFromType((Stat)(i));
                Debug.Log($"stat {i} : {stat} : {((Stat)(i)).ToString()}");
                text.text = this.playerStats.GetStatFromType((Stat)(i)).ToString();
            }
        }
        
        this.playerStats.StatChanged.AddListener((statName, lastValue, newValue) =>
        {
            int index = this.statNames.IndexOf(statName);
            TextMeshProUGUI text = this.statValues[index];
            TextMeshProUGUI added = this.statAdded[index];

            if (text && added)
            {
                int lastObject = (lastValue + newValue);
                added.text = $"{(lastObject < 0 ? "-" : "+")}{(lastObject).ToString()}";
                added.color = lastObject < 0 ? Color.red : Color.green;
                
                added.DOFade(0f, 1f).SetAutoKill(true).SetDelay(3f);
                text.text = newValue.ToString();
            }
        });
    }
}
