using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField]
    [Range(0f, 20f)]
    float speed;

    float useSpeed;

    [Header("Basic lerp variables")]

    [SerializeField]
    [Range(0, 20)]
    float accelerate;

    [SerializeField]
    [Range(0, 20)]
    float deAccelerate;

    [Space]

    Vector2 movementInput;
    Vector2 useInput;

    [Header("Input table(?)")]
    [SerializeField]
    InputActionReference move;


    [Header("Curve acceleration variables")]
    [SerializeField]
    AnimationCurve speedCurve;

    float curveTime;

    [SerializeField]
    float accelerationTime, deAccelerationTime = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {
        takeInput();
    }

    void FixedUpdate() {
        movementLogic();
    }

    void takeInput() {
        movementInput = move.action.ReadValue<Vector2>();
    }

    void movementLogic() {
        /*Basic lerp movement
        Vector2 velocity = rb.velocity;
        float lerpFloatValue = accelerate;

        if(movementInput == Vector2.zero)
        {
            lerpFloatValue = deAccelerate;
        }
        else
        {
            movementInput.Normalize();
        }

        velocity = Vector2.Lerp(velocity, movementInput * speed, lerpFloatValue * Time.fixedDeltaTime);
        rb.velocity = velocity;
        */

        if (useSpeed == 0 || movementInput != Vector2.zero) {
            useInput = movementInput;
        }

        Vector3 velocity = rb.linearVelocity;
        if (movementInput == Vector2.zero) {
            curveTime = Mathf.Clamp01(curveTime - (Time.deltaTime / deAccelerationTime));
        } else {
            curveTime = Mathf.Clamp01(curveTime + (Time.deltaTime / accelerationTime));
            useInput.Normalize();
        }

        useSpeed = speedCurve.Evaluate(curveTime) * speed;

        velocity = new Vector3(useInput.x * useSpeed, 0, useInput.y * useSpeed);
        rb.linearVelocity = velocity;
    }
}
