using UnityEngine;
using UnityEngine.Rendering.UI;

public class PlayerController : MonoBehaviour {
    
    InputActions inputs;
    Vector2 lookInput;
    Vector2 moveInput;

    float camVertical = 0;
    Vector3 accel = Vector3.zero;

    public Rigidbody body;
    public Camera cam;
    public float moveSpeed = 1f;
    public float lookSpeed = 5f;

    void Start() {
        inputs = new InputActions();
        inputs.Player.Enable();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void FixedUpdate() {
        lookInput = inputs.Player.Look.ReadValue<Vector2>();
        moveInput = inputs.Player.Move.ReadValue<Vector2>();

        var mouseY = -lookInput.y;
        camVertical += mouseY * Time.deltaTime * lookSpeed;
        if (camVertical > 80) camVertical = 80;
        else if (camVertical < -80) camVertical = -80;
        else cam.transform.Rotate(new Vector3(mouseY, 0, 0) * Time.deltaTime * lookSpeed);
        transform.Rotate(new Vector3(0, lookInput.x, 0) * Time.deltaTime * lookSpeed);

        body.linearVelocity = (transform.forward * moveInput.y * moveSpeed) + (transform.right * moveInput.x * moveSpeed);
    }
}
