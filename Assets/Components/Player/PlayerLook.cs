using System;
using Framework;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private float maxLook = 1f;
    [SerializeField] private Vector2 look;
    
    private PlayerInputManager inputManager;
    private CameraMain cameraManager;

    private void Start()
    {
        this.inputManager = PlayerInputManager.instance;
        this.cameraManager = CameraMain.instance;
    }

    private void Update()
    {
        Vector2 lookVector = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - (new Vector2(Screen.width * .5f, Screen.height * .5f));
        this.look = lookVector / new Vector2(Screen.width, Screen.height);

        CameraMain.instance.lookOffset = new Vector3(this.look.x, 0f, this.look.y) * this.maxLook;
    }
}