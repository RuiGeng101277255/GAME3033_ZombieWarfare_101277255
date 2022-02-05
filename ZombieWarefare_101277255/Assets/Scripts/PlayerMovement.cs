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
    public readonly int isFiringHash = Animator.StringToHash("isFiring");
    public readonly int isReloadingHash = Animator.StringToHash("isReloading");

    private PlayerController playerController;
    private Vector2 inputVector = Vector2.zero;
    private Vector3 moveDir = Vector3.zero;
    private Vector2 lookDir = Vector2.zero;

    public GameObject followTarget;
    public float aimSensitivity = 1.0f;

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
        followTarget.transform.rotation *= Quaternion.AngleAxis(lookDir.x * aimSensitivity, Vector3.up);
        followTarget.transform.rotation *= Quaternion.AngleAxis(lookDir.y * aimSensitivity, Vector3.left);

        var angles = followTarget.transform.localEulerAngles;
        angles.z = 0.0f;

        var angle = followTarget.transform.localEulerAngles.x;

        if (angle > 180.0f && angle < 340.0f)
        {
            angles.x = 340.0f;
        }
        else if (angle < 180.0f && angle > 40.0f)
        {
            angles.x = 40.0f;
        }

        followTarget.transform.localEulerAngles = angles;

        transform.rotation = Quaternion.Euler(0.0f, followTarget.transform.rotation.eulerAngles.y, 0.0f);
        followTarget.transform.localEulerAngles = new Vector3(angles.x, 0.0f, 0.0f);




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

    public void OnAim(InputValue value)
    {
        playerController.isAiming = value.isPressed;
    }

    public void OnLook(InputValue value)
    {
        lookDir = value.Get<Vector2>();
        //Animation adjustment for aim direction
    }

    public void OnFire(InputValue value)
    {
        playerController.isFiring = value.isPressed;
        playerAnimator.SetBool(isFiringHash, playerController.isFiring);
    }

    public void OnReload(InputValue value)
    {
        playerController.isReloading = value.isPressed;
        playerAnimator.SetBool(isReloadingHash, playerController.isReloading);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") && !playerController.isJumping) return;

        playerController.isJumping = false;
        playerAnimator.SetBool(isJumpingHash, playerController.isJumping);
    }
}
