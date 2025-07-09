using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float accel;
    [SerializeField] float jumpSpeed;
    [SerializeField] GameObject hint;

    Rigidbody rb;
    PlayerInput playerInput;
    bool isGrounding;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.sleepThreshold = 0;
        playerInput = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        var move = playerInput.actions["Move"].ReadValue<Vector2>();
        var forward = Camera.main.transform.forward;
        var right = Camera.main.transform.right;

        forward.y = 0;
        right.y = 0;
        forward = forward.normalized;
        right = right.normalized;

        var move3d = move.x * right + move.y * forward;
        if (isGrounding)
        {
            rb.AddForce(move3d * accel, ForceMode.Acceleration);
        }

        if (isGrounding && playerInput.actions["Jump"].WasPressedThisFrame())
        {
            var vel = rb.linearVelocity;
            vel.y = jumpSpeed;
            rb.linearVelocity = vel;
        }

        hint.SetActive(isGrounding);
    }

    private void FixedUpdate()
    {
        isGrounding = false;
    }

    private void OnCollisionStay(Collision collision)
    {
        foreach (var contact in collision.contacts)
        {
            var normal = contact.normal;
            if(normal.y > 0.5f)
            {
                isGrounding = true;
            }
        }
    }
}
