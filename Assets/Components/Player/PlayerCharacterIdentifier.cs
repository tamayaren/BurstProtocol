using UnityEngine;

public class PlayerCharacterIdentifier : MonoBehaviour
{
    [SerializeField] private CharacterSystemMetadata currentCharacter;

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private EntityStats stats;

    private void BaseStatMake()
    {
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
        
        BaseStatMake();
    }

    private void Update()
    {
        Vector3 movement = this.characterController.velocity;
        this.animator.SetFloat("Speed", this.characterController.velocity.magnitude);

        if (movement.sqrMagnitude > 0.01f)
            this.spriteRenderer.flipX = Vector3.Dot(movement.normalized, this.transform.right) < 0f;
    }
}
