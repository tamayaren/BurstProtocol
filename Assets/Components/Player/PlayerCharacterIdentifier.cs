using UnityEngine;

public class PlayerCharacterIdentifier : MonoBehaviour
{
    [SerializeField] private CharacterSystemMetadata currentCharacter;

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private EntityStats stats;
    [SerializeField] private Entity entity;

    [SerializeField] private Renderer renderer;
    private void BaseStatMake()
    {
        this.entity.MaxHealth = this.currentCharacter.baseHealth;
        this.entity.Health = this.currentCharacter.baseHealth;
        
        this.stats.attack = this.currentCharacter.baseAttack;
        this.stats.defense = this.currentCharacter.baseDefense;
        this.stats.speed = this.currentCharacter.baseSpeed;
        this.stats.endurance = this.currentCharacter.baseEndurance;
        
        this.stats.critDamage = this.currentCharacter.baseCritDamage;
        this.stats.critRate = this.currentCharacter.baseCritRate;

        this.stats.pathAttack = this.currentCharacter.basePathAttack;
        this.stats.pathDefense = this.currentCharacter.basePathDefense;
    }
    
    private void Start()
    {
        this.animator.runtimeAnimatorController = this.currentCharacter.animator;

        this.characterController = GetComponent<CharacterController>();
        this.entity = GetComponent<Entity>();
        this.renderer = this.animator.gameObject.GetComponent<Renderer>();
        
        BaseStatMake();
    }

    private void Update()
    {
        Vector3 movement = this.characterController.velocity;
        this.animator.SetFloat("Speed", this.characterController.velocity.magnitude);

        if (movement.sqrMagnitude > 0.01f)
        {
            bool isRotation = Vector3.Dot(movement.normalized, this.transform.right) < 0f;
            
            this.spriteRenderer.flipX = isRotation;
        }
    }
}
