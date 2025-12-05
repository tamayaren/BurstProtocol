using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private GameObject weapon;
    [SerializeField] private PlayerInputManager inputManager;
    private PlayerCharacterIdentifier character;
    private WeaponShoot shoot;

    public Sprite projectileSprite;
    
    public bool canShoot = true;
    public float fireRate = .2f;
    public float speed = 32f;

    private void Start()
    {
        if (this.weapon)
            this.shoot = this.weapon.GetComponent<WeaponShoot>();

        this.character = GetComponent<PlayerCharacterIdentifier>();
        this.fireRate = this.character.currentCharacter.fireRate;
        this.speed = this.character.currentCharacter.projectileSpeed;
        this.inputManager.shootAction.performed += Shoot;
    }

    private IEnumerator Cooldown()
    {
        this.canShoot = false;
        yield return new WaitForSeconds(this.fireRate);
        this.canShoot = true;
    }

    private void Shoot(InputAction.CallbackContext context)
    {
        if (GameplayManager.instance.gameSession == GameSession.Paused) return;
        if (!this.canShoot) return;

        int stack = 1;
        if (TryGetComponent<GetABooster>(out GetABooster getABooster))
        {
            stack += getABooster.stack;    
        }

        for (int i = 0; i < stack; i++)
        {
            this.shoot.Shoot(
                this.projectileSprite, this.speed + Random.Range(stack, (stack)*3), Input.mousePosition +
                    new Vector3(Random.Range(-24f, 24f) * stack, 0f, Random.Range(-24, 24f) * stack)
                , this.character.currentCharacter);
        }

        StartCoroutine(Cooldown());
    }

    private void OnGUI()
    {
        
        Debug.DrawLine(this.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }
}
