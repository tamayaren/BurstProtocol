
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIBuff : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TextMeshProUGUI text;
    private BuffMetadata metadata;
    private GameObject buffDescription;

    private TextMeshProUGUI title;
    private TextMeshProUGUI description;
    private TextMeshProUGUI stack;

    private bool enableHover = false;
    private GameModifierBase buffLogic;
    
    private void Start() => this.text = GetComponent<TextMeshProUGUI>();
    public void Initialize(BuffMetadata metadata, GameObject buffDescription, GameModifierBase buffLogic)
    {
        this.metadata = metadata;
        this.buffDescription = buffDescription;
        this.buffLogic = buffLogic;

        Debug.Log(metadata);
        Debug.Log(buffDescription);
        Debug.Log(buffLogic);
        if (buffLogic == null)
        {
            Debug.Log("NOT NULL DEAD");
            Destroy(this.gameObject);
            return;
        } 
        this.text.text = $"{this.metadata.fullName} {this.buffLogic.stack}";
        this.title = this.buffDescription.transform.Find("Title").GetComponent<TextMeshProUGUI>();
        this.description = this.buffDescription.transform.Find("Description").GetComponent<TextMeshProUGUI>();
        this.stack = this.buffDescription.transform.Find("Stack").GetComponent<TextMeshProUGUI>();
        
        this.enableHover = true;
        
        this.buffLogic.stackChanged.AddListener(stack =>
        {
            this.text.text = $"{this.metadata.fullName} {this.buffLogic.stack}";
        });
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        this.buffDescription.SetActive(true);

        this.title.text = this.metadata.fullName;
        this.description.text = this.metadata.buffDescription;
        this.stack.text = this.buffLogic.stack.ToString();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        this.buffDescription.SetActive(false);
    }
}
