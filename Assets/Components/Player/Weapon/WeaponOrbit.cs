using UnityEngine;

public class WeaponOrbit : MonoBehaviour
{
    public bool isOrbiting = true;
    public bool lookAtMouse = false;
    private Transform parent;

    [SerializeField] private float smoothness = 5f;
    public float distance = 1f;
    public float height = 0f;

    private void Start()
    {
        this.parent = this.transform.parent;
    }

    private void Orbit()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, this.parent.position);

        float dist;
        if (plane.Raycast(ray, out dist))
        {
            Vector3 hitPoint = ray.GetPoint(dist);
            Vector3 dir = (hitPoint - this.parent.position).normalized;
            Vector3 desiredPos = this.parent.position + dir * this.distance + Vector3.up * this.height;

            this.transform.position = Vector3.Lerp(this.transform.position, desiredPos, Time.deltaTime * this.smoothness);

            Vector3 lookDirection = this.parent.position - hitPoint;
            lookDirection.y = 0f;

            if (this.lookAtMouse)
                this.transform.rotation = Quaternion.LookRotation(lookDirection);
        }
    }
    
    private void Update()
    {
        if (this.isOrbiting)
            Orbit();
    }
}
