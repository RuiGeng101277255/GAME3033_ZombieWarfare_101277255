using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float walkSpeed = 5.0f;
    [SerializeField]
    float runSpeed = 10.0f;
    [SerializeField]
    float jumpForce = 5.0f;

    private PlayerController playerController;
    private Vector2 inputVector = Vector2.zero;
    private Vector3 moveDir = Vector3.zero;
    private Rigidbody playerRB;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        playerRB = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.isJumping) return;
        if (!(inputVector.magnitude > 0)) moveDir = Vector3.zero;

        moveDir = transform.forward * inputVector.y + transform.right * inputVector.x;
        float currentSpeed = playerController.isRunning ? runSpeed : walkSpeed;
        Vector3 movementVec = moveDir *  (currentSpeed * Time.deltaTime);
        transform.position += movementVec;
    }

    public void OnMovementAction(InputValue value)
    {
        inputVector = value.Get<Vector2>();
        Debug.Log(value);
    }

    public void OnRun(InputValue value)
    {
        playerController.isRunning = value.isPressed;
        Debug.Log(value);
    }

    public void OnJump(InputValue value)
    {
        playerController.isJumping = value.isPressed;
        playerRB.AddForce((transform.up + moveDir) * jumpForce, ForceMode.Impulse);
        Debug.Log(value);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") && !playerController.isJumping) return;

        playerController.isJumping = false;
    }
}
