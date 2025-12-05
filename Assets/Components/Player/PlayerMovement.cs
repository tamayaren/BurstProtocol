using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;
using Game.Mechanics;

public class PlayerMovement : MonoBehaviour
{
    private PlayerInputManager inputManager;
    private CharacterController characterController;
    private EntityStats entityStats;
    private Animator animator;
    private Entity entity;

    public bool canMove = true;
    public float baseSpeed = 6f;

    [SerializeField] private bool isDashing = false;

    private float dashSpeed = 0f;
    public bool canDash = true;
    public float dashTime = 1f;
    private void Start()
    {
        this.inputManager = PlayerInputManager.instance;
        
        this.entity = GetComponent<Entity>();
        this.characterController = GetComponent<CharacterController>();
        this.entityStats = GetComponent<EntityStats>();

        this.inputManager.dashAction.performed += OnDash;
        (float, float) startComputedSpeed = StatCalculator.CalculateDash(this.entityStats);
        this.dashSpeed = startComputedSpeed.Item1;
        
        this.animator = this.transform.Find("Texture")?.GetComponent<Animator>();
        
        HookCharacter();
    }

    private IEnumerator DashAction()
    {
        (float, float) computedSpeed = StatCalculator.CalculateDash(this.entityStats);
        this.dashSpeed = computedSpeed.Item1;
        this.isDashing = true;
        this.canMove = false;
        this.canDash = false;
        this.entity.EntityState = EntityState.Invincible;
        this.entity.SetIFrame(true);
        if (this.animator)
            this.animator.Play("Dash");
        yield return new WaitForSeconds(.15f);
        this.entity.EntityState = EntityState.Alive;
        yield return new WaitForSeconds(computedSpeed.Item2 - .15f);
        this.entity.SetIFrame(false);
        this.isDashing = false;
        if (this.animator)
            this.animator.SetTrigger("Dash");
        this.canMove = true;

        float dashCooldown = StatCalculator.CalculateDashCooldown(this.entityStats.dashCooldown, this.entityStats);
        
        UIDash.instance.Regenerate(dashCooldown);
        yield return new WaitForSeconds(dashCooldown);
        this.canDash = true;
    }
    
    private void OnDash(InputAction.CallbackContext context)
    {
        if (GameplayManager.instance.gameSession == GameSession.Paused) return;
        if (this.canDash)
            StartCoroutine(DashAction());
    }

    private void HookCharacter()
    {
        // Speed
        this.baseSpeed = StatCalculator.CalculateSpeed(this.baseSpeed, this.entityStats);
        this.entityStats.StatChanged.AddListener(((stat, _, value) =>
        {
            if (GameplayManager.instance.gameSession == GameSession.Paused) return;
            if (stat.Equals("Speed") || stat.Equals("Endurance"))
                this.baseSpeed = StatCalculator.CalculateSpeed(this.baseSpeed, this.entityStats);
        }));
    }

    private Vector3 GetDashDirection()
    {
        Vector3 direction = new Vector3(this.inputManager.movementInput.x, 0f, this.inputManager.movementInput.y);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, LayerMask.GetMask("Map")))
        {
            Vector3 target = hit.point;
            direction = (target - this.transform.position);

            direction.y = 0f;
            return direction.normalized;
        }

        return direction;
    }
    
    private void Update()
    {
        if (GameplayManager.instance.gameSession == GameSession.Paused) return;
        if (this.isDashing)
        {
            this.dashTime -= .03f * Time.fixedDeltaTime;
            Vector3 direction = GetDashDirection();
            
            this.characterController.Move(direction * ((this.dashSpeed * this.dashTime) * Time.deltaTime));
            return;
        }
        
        if (this.canMove)
            this.characterController.Move(new Vector3(this.inputManager.movementInput.x, 0, this.inputManager.movementInput.y) * (this.baseSpeed * Time.deltaTime));
    }
}
