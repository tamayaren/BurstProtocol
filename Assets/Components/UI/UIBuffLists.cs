using UnityEngine;

public class UIBuffLists : MonoBehaviour
{
    [SerializeField] private GameObject buffPrefab;

    [SerializeField] private Transform buffContainer;

    private void Start()
    {
        PlayerBuffs.instance.onBuffAdded.AddListener((name, metadata) =>
        {
            
        });
        
        PlayerBuffs.instance.onBuffRemoved.AddListener((name) =>
        {
            
        });
    }
}
