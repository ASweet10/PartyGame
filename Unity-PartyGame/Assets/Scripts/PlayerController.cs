using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    [SerializeField] Camera mainCamera;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask ground;
    public bool cursorLocked;
    AudioSource footstepAudioSource;

    [Header("Controls")]
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;


    [Header("Movement Parameters")]
    [SerializeField, Range(1, 100)] float speed = 25f;
    [SerializeField, Range(1, 20)] float sprintSpeed = 13f;
    [SerializeField] private float jumpHeight = 12f;
    [SerializeField, Range(0.0f, 0.5f)] float moveSmoothTime = 0.3f;

    Vector2 targetDir;
    Vector2 currentDir;
    Vector2 currentDirVelocity;
    Vector3 velocity;
    float velocityY;
    float gravity = -30f;


    [Header("Stamina")]
    [SerializeField, Range(1, 20)] float maxStamina = 15f;
    [SerializeField] AudioSource windedAudioSource;
    float currentStamina;
    
    /*
    [Header("Crouch Parameters")]
    [SerializeField] private float crouchHeight = 0.5f;
    [SerializeField] private float standingHeight = 2.1f;
    [SerializeField] private float timeToCrouch = 0.25f;
    [SerializeField, Range(1, 5)] private float crouchSpeed = 2.5f;
    private bool isCrouching;
    private bool duringCrouchAnimation;
    private bool canCrouch = true;
    */

    private bool isGrounded = false;
    CharacterController controller;

    private void Awake()
    {
        footstepAudioSource = gameObject.GetComponent<AudioSource>();
        controller = gameObject.GetComponent<CharacterController>();
    }
    private void Start()
    {
        currentStamina = maxStamina;
        if(cursorLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = true;
        }
    }
    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.2f, ground);

        targetDir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        targetDir.Normalize();

        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);

        velocityY += gravity * 2f * Time.deltaTime;

        Vector3 velocity = (transform.forward * currentDir.y + transform.right * currentDir.x) * speed + Vector3.up * velocityY;

        controller.Move(velocity * Time.deltaTime);

        if(isGrounded && Input.GetKeyDown(jumpKey))
        {
            velocityY = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        if(!isGrounded && controller.velocity.y < -1f)
        {
            velocityY = -9.8f;
        }

        PlayFootstepAudio();

    }

    private void PlayFootstepAudio()
    {
        if(!footstepAudioSource.isPlaying)
        {
            footstepAudioSource.Play();
        }
    }

    public void PauseFootstepAudio()
    {
        footstepAudioSource.Pause();
    }

        /*
    private void UpdateStamina()
    {
        if(currentStamina <= 0)
        {
            windedAudioSource.Play();
            canRun = false;
        }
        else if(currentStamina >= maxStamina)
        {
            currentStamina = maxStamina;
            canRun = true;
        }
        //If player moving
        if(moveSide != 0 || moveForward != 0)
        {
            //Sprinting, lose stamina
            if(currentSpeed == sprintSpeed)
            {
                currentStamina -= 1 * Time.deltaTime;
            }
            //Walking, repair stamina
            else
            {
                currentStamina += 1.5f * Time.deltaTime;
            }
        }
    }
    */

    /*
    private void AttemptToCrouch()
    {
        if(!duringCrouchAnimation && controller.isGrounded)
        {
            if(Input.GetKeyDown(KeyCode.C))
            {
                StartCoroutine(CrouchOrStand());
            }
        }
    }

    private IEnumerator CrouchOrStand()
    {
        duringCrouchAnimation = true;

        float timeElapsed = 0f;
        float currentHeight = controller.height;
        float targetHeight;
        if(isCrouching)
        {
            targetHeight = standingHeight;
            isCrouching = false;
        }
        else
        {
            targetHeight = crouchHeight;
            isCrouching = true;
        }

        while(timeElapsed > timeToCrouch)
        {
            controller.height = Mathf.Lerp(currentHeight, targetHeight, timeElapsed / timeToCrouch);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        controller.height = targetHeight;

        duringCrouchAnimation = false;
    }
    */
}


