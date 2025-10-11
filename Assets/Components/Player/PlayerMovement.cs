using System;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class PlayerMovement : MonoBehaviour
{
    private PlayerInputManager inputManager;
    
    private CharacterController characterController;

    public float speed = 12f;

    private void Start()
    {
        this.inputManager = PlayerInputManager.instance;
        
        this.characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        this.characterController.Move(new Vector3(this.inputManager.movementInput.x, 0, this.inputManager.movementInput.y) * (this.speed * Time.deltaTime));
    }
}
