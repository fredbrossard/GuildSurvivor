using Game.Entity.Player;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent (typeof(PlayerMovement))]
public class PlayerInput : MonoBehaviour
{
    [SerializeField] private InputActionReference moveActionRef;
    private PlayerMovement playerMovement;

    private bool _actionPerfomed = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        moveActionRef.action.performed += MovePerfomed;
        moveActionRef.action.canceled += MoveCanceled;
    }

    private void OnDestroy()
    {
        moveActionRef.action.performed -= MovePerfomed;
        moveActionRef.action.canceled -= MoveCanceled;
    }

    private void MoveCanceled(InputAction.CallbackContext context)
    {
        _actionPerfomed = false;
    }

    //It's impossible without canceld callback to perfomed action, wtf unity pretty shit input system?
    private void MovePerfomed(InputAction.CallbackContext context)
    {
        _actionPerfomed = true;
    }

    private void Update()
    {
        if (_actionPerfomed)
            Move();
    }

    private void Move()
    {
        Vector2 movePos = moveActionRef.action.ReadValue<Vector2>();
        Debug.Log(movePos);
        playerMovement.Move(movePos);
    }
}
