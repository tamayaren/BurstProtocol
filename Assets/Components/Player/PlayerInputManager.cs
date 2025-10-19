using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    public static PlayerInputManager instance;
    
    private InputManager inputSystem;

    public Vector2 movementInput;

    public InputAction dashAction;
    private InputAction movementAction;
    
    private void Awake()
    {
        if (instance == null) instance = this;
        this.inputSystem = new InputManager();
    }

    private void Start()
    {
        this.movementAction = this.inputSystem.FindAction("Move");
        this.dashAction = this.inputSystem.FindAction("Dash");
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
