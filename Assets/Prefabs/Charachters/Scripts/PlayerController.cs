using System.Threading;
using NUnit.Framework;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class PlayerController : MonoBehaviour
{
    [SerializeField] public float speed = 7.5f;
    [SerializeField] public float extraSpeed = 7.5f;
    [SerializeField] public float jumpSpeed = 8.0f;
    [SerializeField] public float gravity = 20.0f;
    [SerializeField] public float lookSpeed = 2.0f;
    [SerializeField] float lookXLimit = 60.0f;
    
    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    Vector2 rotation = Vector2.zero;
    private Animator _animator;
    private float currentSpeed;
    [SerializeField] private Transform playerCameraParent;
    
    [HideInInspector]
    public bool canMove = true;

    void Start()
    {
        playerCameraParent = GameObject.Find("GameManager").GetComponent<MainConfig>().playerCameraParent.transform;
        _animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        rotation.y = transform.eulerAngles.y;
    }

    void Update()
    {
        if (characterController.isGrounded)
        {
            // We are grounded, so recalculate move direction based on axes
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);
            
            ButtonsLogic(Input.GetAxis("Vertical"));

            float curSpeedX = canMove ? currentSpeed * Input.GetAxis("Vertical") : 0;
            float curSpeedY = canMove ? currentSpeed * Input.GetAxis("Horizontal") : 0;
            

            moveDirection = (forward * curSpeedX) + (right * curSpeedY);

            if (Input.GetButton("Jump") && canMove)
            {
                moveDirection.y = jumpSpeed;
                _animator.SetBool("isJump", true);
            }
        }

        moveDirection.y -= gravity * Time.deltaTime;
        characterController.Move(moveDirection * Time.deltaTime);

        // Player and Camera rotation
        if (canMove)
        {
            rotation.y += Input.GetAxis("Mouse X") * lookSpeed;
            rotation.x += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotation.x = Mathf.Clamp(rotation.x, -lookXLimit, lookXLimit);
            playerCameraParent.localRotation = Quaternion.Euler(rotation.x, 0, 0);
            transform.eulerAngles = new Vector2(0, rotation.y);
        }
    }

    private void ButtonsLogic(float curSpeedX)
    {
        _animator.SetBool("isJump", false);
        _animator.SetFloat("vertical", curSpeedX);

        _animator.SetBool("isShift", false);
        currentSpeed = speed;
        
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _animator.SetBool("isShift", true);
            currentSpeed = extraSpeed;
        }
    }
    
}