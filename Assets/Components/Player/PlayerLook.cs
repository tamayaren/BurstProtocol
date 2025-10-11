using System;
using Framework;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private Transform onLook;
    [SerializeField] private float maxLook = 5f;
    
    private PlayerInputManager inputManager;
    private CameraMain cameraManager;

    private void Start()
    {
        this.inputManager = PlayerInputManager.instance;
        this.cameraManager = CameraMain.instance;
    }

    private void Update()
    {
        Vector3 lookObjectVector = this.onLook.localPosition;
        Vector2 lookVector = this.inputManager.lookInput * Time.deltaTime;
        this.onLook.localPosition = new Vector3(
            Mathf.Clamp(lookObjectVector.x + lookVector.x, -this.maxLook, this.maxLook), 
            0, 
            Mathf.Clamp(lookObjectVector.z + lookVector.y, -this.maxLook, this.maxLook));
        
        this.cameraManager.offset = new Vector3(this.onLook.localPosition.x, this.cameraManager.offset.y, this.onLook.localPosition.z);
    }
}