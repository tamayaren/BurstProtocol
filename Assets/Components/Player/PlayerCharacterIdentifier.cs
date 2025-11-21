using Game.Mechanics.Organelles;
using UnityEngine;

public class PlayerCharacterIdentifier : MonoBehaviour
{
    public float projectileBaseDamage;
    [SerializeField] public CharacterSystemMetadata currentCharacter;

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private EntityStats stats;
    [SerializeField] private Entity entity;
    [SerializeField] private OrganelleManager organelleManager;

    [SerializeField] private Renderer renderer;
    private void BaseStatMake()
    {
        this.entity.MaxHealth = (int)(this.currentCharacter.baseHealth + (
            (this.currentCharacter.baseHealth) +
            (1+(100/this.currentCharacter.statLevelGrowth[0]))
                ));
        this.entity.Health = this.entity.MaxHealth;
        
        this.stats.FeedInitializer(new EntityStatsSchema()
                // TODO Player Saving
            .SetLevel(1)
                
            .SetAttack(this.currentCharacter.baseAttack)
            .SetDefense(this.currentCharacter.baseDefense)
            
            .SetPathogenicAttack(this.currentCharacter.basePathDefense)
            .SetPathogenicDefense(this.currentCharacter.basePathDefense)
            
            .SetSpeed(this.currentCharacter.baseSpeed)
            .SetEndurance(this.currentCharacter.baseEndurance)
            
            .SetCritDamage(this.currentCharacter.baseCritDamage)
            .SetCritRate(this.currentCharacter.baseCritRate)
        , this.currentCharacter.statLevelGrowth);

        this.projectileBaseDamage = this.currentCharacter.baseAttackDamage;
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
