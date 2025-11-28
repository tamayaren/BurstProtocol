using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    public static PlayerInputManager instance;
    
    private InputManager inputSystem;

    public Vector2 movementInput;

    public UnityEvent<int> skillActionRequest = new UnityEvent<int>();
    
    public InputAction dashAction;
    public InputAction shootAction;
    
    [SerializeField] private InputAction movementAction;
    [SerializeField] private InputAction[] skills = new InputAction[3];
    
    private void Awake()
    {
        if (instance == null) instance = this;
        this.inputSystem = new InputManager();
        
        this.movementAction = this.inputSystem.FindAction("Move");
        this.dashAction = this.inputSystem.FindAction("Dash");
        this.shootAction = this.inputSystem.FindAction("Attack");

        this.skills = new[]
        {
            this.inputSystem.FindAction("Skill 1"),
            this.inputSystem.FindAction("Skill 2"),
            this.inputSystem.FindAction("Skill 3")
        };
        
        HookSkillInputActions();
    }

    private void HookSkillInputActions()
    {
        for (int i = 0; i < this.skills.Length; i++)
        {
            int id = i;
            InputAction action = this.skills[id];

            Debug.Log(action);
            if (action == null) continue;
            Debug.Log(action.name);
            action.performed += (callback) =>
            {
                if (!callback.performed) return;
                
                Debug.Log("Input Red");
                this.skillActionRequest.Invoke(id);
            };
        }
    }
    
    private void OnEnable()
    {
        if (this.inputSystem == null) this.inputSystem = new InputManager();
        
        this.inputSystem.Enable();
    }

    private void OnDisable()
    {
        this.inputSystem.Disable();
    }

    private void Update()
    {
        if (this.inputSystem == null) return;
        
        this.movementInput = this.movementAction.ReadValue<Vector2>();
    }
}
