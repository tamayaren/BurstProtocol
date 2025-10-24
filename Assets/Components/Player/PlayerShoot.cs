using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private GameObject weapon;
    [SerializeField] private PlayerInputManager inputManager;
    private WeaponShoot shoot;

    public Sprite projectileSprite;
    
    // INTEGRATE TO CHARACTER IDENTIFIER SOON
    public bool canShoot = true;
    public float fireRate = .2f;
    public float speed = 32f;

    private void Start()
    {
        if (this.weapon)
            this.shoot = this.weapon.GetComponent<WeaponShoot>();

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
        if (!this.canShoot) return;
        
        this.shoot.Shoot(
            this.projectileSprite, this.speed, Input.mousePosition);

        StartCoroutine(Cooldown());
    }

    private void OnGUI()
    {
        
        Debug.DrawLine(this.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }
}
