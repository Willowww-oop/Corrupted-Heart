using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class PlayerInputs : MonoBehaviour
{
    public PlayerController playerController;

    private InputAction MoveAction;
    private InputAction LookAction;
    private InputAction JumpAction;
    private InputAction SprintAction;
    private InputAction ParkourAction;
    private InputAction SwapAction;

    //private InputAction RobotAction;
    //private InputAction MagmaAction;

    private InputAction Attack1Action;
    private InputAction Attack2Action;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Movement 

        MoveAction = InputSystem.actions.FindAction("Move");
        LookAction = InputSystem.actions.FindAction("Look");
        JumpAction = InputSystem.actions.FindAction("Jump");
        SprintAction = InputSystem.actions.FindAction("Sprint");
        ParkourAction = InputSystem.actions.FindAction("ParkourAbility");

        //// Swap 

        //RobotAction = InputSystem.actions.FindAction("Character1");
        //MagmaAction = InputSystem.actions.FindAction("Character2");
        SwapAction = InputSystem.actions.FindAction("SwapCharacter");

        //// Attacks 

        Attack1Action = InputSystem.actions.FindAction("Attack1");
        //Attack2Action = InputSystem.actions.FindAction("Attack2");

        // Preformed actions

        JumpAction.performed += OnJumpPerformed;
        SprintAction.performed += OnSprintStarted;
        SprintAction.canceled += OnSprintCanceled;
        ParkourAction.performed += OnParkourPerformed;

        //RobotAction.performed += OnChara1Performed;
        //MagmaAction.performed += OnChara2Performed;
        SwapAction.performed += OnSwapPerformed;

        Attack1Action.performed += OnAttack1Performed;
        //Attack2Action.performed += OnAttack2Performed;

        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 MovementVector = MoveAction.ReadValue<Vector2>();
        playerController.Move(MovementVector);

        //Vector2 LookVector = LookAction.ReadValue<Vector2>();
        //playerController.Rotate(LookVector);
    }

    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        playerController.Jump();
    }
    
    private void OnParkourPerformed(InputAction.CallbackContext context)
    {
        playerController.ParkourAbility();
    }

    private void OnSprintStarted(InputAction.CallbackContext context)
    {
        playerController.SprintStarted();
    }

    private void OnSprintCanceled(InputAction.CallbackContext context)
    {
        playerController.SprintCanceled();
    }

    private void OnSwapPerformed(InputAction.CallbackContext context)
    {
        playerController.Swap();
    }

    private void OnAttack1Performed(InputAction.CallbackContext context)
    {
        playerController.Attack();
    }
    

    //private void OnChara1Performed(InputAction.CallbackContext context)
    //{
    //    playerController.characterVal = 1;
    //    playerController.Swap();
    //}
    //private void OnChara2Performed(InputAction.CallbackContext context)
    //{
    //    playerController.characterVal = 2;
    //    playerController.Swap();
    //}

    private void OnAttack2Performed(InputAction.CallbackContext context)
    {

    }
}
