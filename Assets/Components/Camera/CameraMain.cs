using Unity.VisualScripting;
using UnityEngine;
using Framework;

public class CameraMain : MonoBehaviour
{
    public static CameraMain instance;
    
    [SerializeField] private Transform subject;
    private Camera camera;

    public Vector3 offset;
    public Quaternion rotation;
    public float smoothness;
    public float fov;

    private void Awake() => instance = this;
    private void Start() => this.camera = Camera.main;
    private void Update()
    {
        if (!this.camera) return;
        
        this.camera.transform.position = Vector3.Lerp(this.camera.transform.position, this.subject.position + this.offset, this.smoothness * Time.deltaTime);
        this.camera.transform.rotation = new Quaternion(this.rotation.x, this.rotation.y, this.rotation.z, this.rotation.w);
        
        this.camera.fieldOfView = Mathf.Lerp(this.camera.fieldOfView, this.fov, this.smoothness * Time.deltaTime);
    }

    public void SetSubject(Transform subject) => this.subject = subject;
}
