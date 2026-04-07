using UnityEngine;
using UnityEngine.InputSystem;

public class MousePointerScript : MonoBehaviour
{
    private Transform mouse_transform;

    private void Start()
    {
        mouse_transform = this.transform;
    }

    private void LookAtMouse()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - mouse_transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; //angle rotation
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward); //z axis 
        //any idea on how to make the clock hand spin from the bottom and not the center?
        mouse_transform.rotation = rotation;
    }

    void Update()
    {
        LookAtMouse();
    }
}
