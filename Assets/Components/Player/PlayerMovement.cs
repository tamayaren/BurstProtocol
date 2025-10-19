using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;

public class PlayerMovement : MonoBehaviour
{
    private PlayerInputManager inputManager;
    private CharacterController characterController;
    private EntityStats entityStats;

    public bool canMove = true;
    public float baseSpeed = 6f;

    public bool canDash = true;
    private void Start()
    {
        this.inputManager = PlayerInputManager.instance;
        
        this.characterController = GetComponent<CharacterController>();
        this.entityStats = GetComponent<EntityStats>();

        this.inputManager.dashAction.performed += OnDash;
    }

    private IEnumerator DashAction()
    {
        this.canMove = false;
        yield return new WaitForSeconds(1f);
        this.canMove = true;
        yield return new WaitForSeconds(StatCalculator.CalculateDashCooldown(this.entityStats.dashCooldown, this.entityStats));
        this.canDash = true;
    }
    
    private void OnDash(InputAction.CallbackContext context)
    {
        if (this.canDash)
            StartCoroutine(DashAction());
    }

    private void HookCharacter()
    {
        // Speed
        this.baseSpeed = StatCalculator.CalculateSpeed(this.baseSpeed, this.entityStats);
        this.entityStats.StatChanged.AddListener(((stat, _, value) =>
        {
            if (stat.Equals("Speed") || stat.Equals("Endurance"))
                this.baseSpeed = StatCalculator.CalculateSpeed(this.baseSpeed, this.entityStats);
        }));
    }

    private void Update()
    {
        if (this.canMove)
            this.characterController.Move(new Vector3(this.inputManager.movementInput.x, 0, this.inputManager.movementInput.y) * (this.baseSpeed * Time.deltaTime));
    }
}
