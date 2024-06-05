using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovementAlien : MonoBehaviour
{
    Animator animator;
    PlayerInput input;
    
    
    float currentMovement;

    int isWalkingHash;
    int isRunningHash;

    bool movementPressed;
    bool runPressed;
    

    private void Awake()
    {
        input = new PlayerInput();

        input.CharacterControl.Movement.performed += ctx =>
        {
            currentMovement = ctx.ReadValue<float>();
            movementPressed = currentMovement > 0;
        };
        
        input.CharacterControl.run.performed += ctx => runPressed = ctx.ReadValueAsButton();
        
    }

    private void Start()
    {
        animator = GetComponent<Animator>();

        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
    }

    // Update is called once per frame
    private void Update()
    {
        HandleMovement();   
    }

    void HandleMovement()
    {
        bool isRunning = animator.GetBool(isRunningHash);
        bool isWalking = animator.GetBool(isWalkingHash);


        switch (true) 
        {
            case true when movementPressed && !isWalking:
                animator.SetBool(isWalkingHash, true);
                break;
            case true when !movementPressed && isWalking:
                animator.SetBool(isWalkingHash, false);
                break;
            case true when (!movementPressed && !runPressed) && isRunning:
                animator.SetBool(isRunningHash, false);
                break;
            case true when (movementPressed && runPressed) && !isRunning:
                animator.SetBool(isRunningHash, true);
                break;
            default:
                break;


        }
        
        if ( movementPressed && !isWalking)
        {
            animator.SetBool(isWalkingHash, true);
        }
        if ( !movementPressed && isWalking)
        {
            animator.SetBool(isWalkingHash, false);
        }
    }

    

    private void OnEnable()
    {
        input.CharacterControl.Enable();
    }

    private void OnDisable()
    {
        input.CharacterControl.Disable();   
    }
}
