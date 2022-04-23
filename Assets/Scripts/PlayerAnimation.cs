using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimation : MonoBehaviour
{
    private PlayerInputActions _inputActions;
    private Vector2 _currentDirection;
    private Vector2 _movementDirection;
    public bool Attacking;
    public PlayerReferences PlayerReferences;
    public SpriteRenderer Renderer;
    public Animator Animator;
    void Start()
    {
        _inputActions = new PlayerInputActions();
        _inputActions.Enable();
        _inputActions.Player.Movement.performed += OnMovement;
        _inputActions.Player.Movement.canceled += OnMovement;
        _inputActions.Player.Dash.started += OnDash;
    }
    
    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 direction = (mousePos-(Vector2)transform.position).normalized;
        if (Mouse.current.leftButton.wasPressedThisFrame && !Attacking)
        {
            SetDirection(direction, Vector2.zero);
            Attacking = true;
            Animator.SetBool("Attacking", Attacking);
            Animator.ResetTrigger("Attack");
            Animator.SetTrigger("Attack");
        }

        if (Mouse.current.leftButton.wasReleasedThisFrame && Attacking)
        {
            Attacking = false;
            Animator.SetBool("Attacking", Attacking);
        }

        SetDirection(direction, _movementDirection);
    }

    private void OnDash(InputAction.CallbackContext callbackContext)
    {
        
    }
    
    private void OnMovement(InputAction.CallbackContext callbackContext)
    {
        _movementDirection = callbackContext.ReadValue<Vector2>();
    }

    private void SetDirection(Vector2 mouseDirection, Vector2 movementDirection)
    {
        float angle = Vector2.Angle(new Vector2(1,0), mouseDirection);
        Animator.SetBool("Reverse", false);

        if (_movementDirection != Vector2.zero)
        {
            Animator.SetBool("Walk", true);
        }
        else
        {
            Animator.SetBool("Walk", false);
        }
        if (angle <= 180 && angle > 135)
        {
            if (movementDirection.x > 0)
            {
                Animator.SetBool("Reverse", true);
            }
            if (_currentDirection == Vector2.left)
            {
                return;
            }
            Animator.SetBool("Left", true);
            Animator.SetBool("Right", false);
            Animator.SetBool("Backward", false);
            Animator.SetBool("Forward", false);
            _currentDirection = Vector2.left;
        }
        else if (angle <= 45 && angle > 0)
        {
            if (movementDirection.x < 0)
            {
                Animator.SetBool("Reverse", true);
            }
            if (_currentDirection == Vector2.right)
            {
                return;
            }
            Animator.SetBool("Left", false);
            Animator.SetBool("Right", true);
            Animator.SetBool("Backward", false);
            Animator.SetBool("Forward", false);
            _currentDirection = Vector2.right;
        }else if (angle <= 135 && angle > 45)
        {
            if (mouseDirection.y > 0)
            {
                if (movementDirection.y < 0)
                {
                    Animator.SetBool("Reverse", true);
                }
                if (_currentDirection == Vector2.down)
                {
                    return;
                }
                Animator.SetBool("Backward", true);
                Animator.SetBool("Forward", false);
                _currentDirection = Vector2.down;
            }
            else
            {
                if (movementDirection.y > 0)
                {
                    Animator.SetBool("Reverse", true);
                }
                if (_currentDirection == Vector2.up)
                {
                    return;
                }
                Animator.SetBool("Forward", true);
                Animator.SetBool("Backward", false);
                _currentDirection = Vector2.up;
            }
            Animator.SetBool("Left", false);
            Animator.SetBool("Right", false);
        }
    }

    private void OnDestroy()
    {
        _inputActions.Player.Movement.performed -= OnMovement;
        _inputActions.Player.Movement.canceled -= OnMovement;
        _inputActions.Player.Dash.started -= OnDash;
    }
}
