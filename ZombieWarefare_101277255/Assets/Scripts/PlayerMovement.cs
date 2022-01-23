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

    public readonly int movementXHash = Animator.StringToHash("MoveX");
    public readonly int movementYHash = Animator.StringToHash("MoveY");
    public readonly int isJumpingHash = Animator.StringToHash("isJumping");
    public readonly int isRunningHash = Animator.StringToHash("isRunning");

    private PlayerController playerController;
    private Vector2 inputVector = Vector2.zero;
    private Vector3 moveDir = Vector3.zero;
    private Rigidbody playerRB;
    private Animator playerAnimator;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        playerRB = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
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
        playerAnimator.SetFloat(movementXHash, inputVector.x);
        playerAnimator.SetFloat(movementYHash, inputVector.y);
    }

    public void OnRun(InputValue value)
    {
        playerController.isRunning = value.isPressed;
        playerAnimator.SetBool(isRunningHash, playerController.isRunning);
    }

    public void OnJump(InputValue value)
    {
        playerController.isJumping = value.isPressed;
        playerRB.AddForce((transform.up + moveDir) * jumpForce, ForceMode.Impulse);
        playerAnimator.SetBool(isJumpingHash, playerController.isJumping);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") && !playerController.isJumping) return;

        playerController.isJumping = false;
        playerAnimator.SetBool(isJumpingHash, playerController.isJumping);
    }
}
