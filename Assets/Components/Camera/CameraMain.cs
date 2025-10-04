using UnityEngine;

public class CameraMain : MonoBehaviour
{
    [SerializeField] private Transform subject;

    private void Update()
    {
        
    }

    public void SetSubject(Transform subject) => this.subject = subject;
}
