using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : Movement
{
    private PlayerInputActions _inputActions;
    private bool _isDashing;
    public float DashTime { get; private set; }
    private Vector2 _playerDirection;
    public Vector2 DashStartPos { get; private set; }
    public Vector2 DashEndPos { get; private set; }
    public PlayerReferences PlayerReferences;
    public float DashSpeed;
    public float DashDuration;
    public float DashCooldown;

    private void Start()
    {
        _maxSpeed = MaxSpeed;
        _inputActions = new PlayerInputActions();
        _inputActions.Enable();
        _inputActions.Player.Dash.started += Dash;
        _inputActions.Player.Movement.performed += SetPlayerDirection;
    }

    void FixedUpdate()
    {
        if (_isDashing)
        {
            if (Time.time - DashTime < DashDuration)
            {
                Rigidbody2D.AddForce(_playerDirection*DashSpeed, ForceMode2D.Impulse);
            }
            else
            {
                _isDashing = false;
                DashTime = Time.time;
                DashEndPos = transform.position;
                OnDashEnd?.Invoke();
            }
        }
        else
        {
            Vector2 dir = _inputActions.Player.Movement.ReadValue<Vector2>();
            if (!dir.Equals(Vector2.zero))
            {
                Rigidbody2D.AddForce(dir*Speed, ForceMode2D.Impulse);
                if (Mathf.Abs(Rigidbody2D.velocity.x) > _maxSpeed)
                {
                    Rigidbody2D.velocity = new Vector2(_maxSpeed * dir.x, Rigidbody2D.velocity.y);
                }

                if (Mathf.Abs(Rigidbody2D.velocity.y) > _maxSpeed)
                {
                    Rigidbody2D.velocity = new Vector2(Rigidbody2D.velocity.x,_maxSpeed * dir.y);
                }

                if (dir.x == 0)
                {
                    Rigidbody2D.velocity = new Vector2(0,Rigidbody2D.velocity.y);
                }

                if (dir.y == 0)
                {
                    Rigidbody2D.velocity = new Vector2(Rigidbody2D.velocity.x, 0);
                }
            }
            else
            {
                Rigidbody2D.velocity = Vector2.zero;
            }
        }
    }

    private void Dash(InputAction.CallbackContext context)
    {
        if (Time.time - DashTime >= DashCooldown)
        {
            DashStartPos = transform.position;
            _isDashing = true;
            DashTime = Time.time;
        }
    }

    private void SetPlayerDirection(InputAction.CallbackContext context)
    {
        _playerDirection = _inputActions.Player.Movement.ReadValue<Vector2>();
    }

    private void OnDestroy()
    {
        _inputActions.Player.Dash.started -= Dash;
        _inputActions.Player.Movement.performed -= SetPlayerDirection;
    }

    public Action OnDashEnd;
}
