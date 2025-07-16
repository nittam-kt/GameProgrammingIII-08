using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float accel;
    [SerializeField] float jumpSpeed;
    [SerializeField] GameObject hint;
    [SerializeField] float rotateRatio;
    [SerializeField] Animator animator;
    [SerializeField] GameObject fire;
    [SerializeField] float fireSpeed;

    Rigidbody rb;
    PlayerInput playerInput;
    bool isGrounding;
    bool canJump;

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
        if (isGrounding && move3d != Vector3.zero)
        {
            rb.AddForce(move3d * accel, ForceMode.Acceleration);
            transform.forward = Vector3.Slerp(transform.forward, move3d, rotateRatio * Time.deltaTime);
        }

        if (canJump && playerInput.actions["Jump"].WasPressedThisFrame())
        {
            var vel = rb.linearVelocity;
            vel.y = jumpSpeed;
            rb.linearVelocity = vel;
        }

        if (playerInput.actions["Attack"].WasCompletedThisFrame())
        {
            var f = Instantiate(fire);
            f.SetActive(true);
            f.transform.position = transform.TransformPoint(f.transform.localPosition);
            f.GetComponent<Rigidbody>().linearVelocity = transform.forward * fireSpeed;
        }
        hint.SetActive(isGrounding);

        // アニメーターにスピードを与える
        animator.SetFloat("Speed", rb.linearVelocity.magnitude);
    }

    private void FixedUpdate()
    {
        isGrounding = false;
        canJump = false;
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
            if (normal.y > 0.9f)
            {
                canJump = true;
            }
        }
    }
}
