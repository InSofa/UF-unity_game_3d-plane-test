using UnityEditorInternal;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField]
    float speed, acceleration, jumpForce;

    Vector3 movementInput;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.z = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space)) {
            Jump();
        }
    }

    void FixedUpdate() {
        Vector3 velocity = rb.linearVelocity;
        movementInput.Normalize();
        velocity = Vector3.Lerp(velocity, movementInput * speed, acceleration * Time.deltaTime);
        rb.linearVelocity = velocity;
    }

    void Jump() {
        Vector3 velocity = rb.linearVelocity;
        velocity.y = jumpForce;
        rb.linearVelocity = velocity;
    }
}
