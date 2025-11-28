using Unity.VisualScripting;
using UnityEngine;

public class DamageTextManager : MonoBehaviour
{
    public static DamageTextManager instance;
    [SerializeField] private GameObject damageTextPrefab;
    
    private void Awake() => instance = this;

    public void GenerateText(Vector3 worldSpace, float damage)
    {
        GameObject obj = Instantiate(this.damageTextPrefab, worldSpace, Quaternion.identity, this.transform);
        
        DamageTextRenderer dmr = obj.GetComponent<DamageTextRenderer>();
        
        obj.SetActive(true);
        dmr.Generate(damage);
    }
}
