using UnityEngine;
using UnityEngine.InputSystem;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField] Transform lookTarget;
    [SerializeField] Vector3 offset;
    [SerializeField] float angleSpeed;

    PlayerInput playerInput;
    float yaw;
    float pitch;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion lookRotation = Quaternion.identity;
        lookRotation.eulerAngles = new Vector3(pitch, yaw, 0);
        var ofs = lookRotation * offset;

        transform.position = lookTarget.position + ofs;
        transform.LookAt(lookTarget.position + new Vector3(0, offset.y,0));
    }
}
