using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class PlayerController : MonoBehaviour
{
    #region Unity Editor Variables
    [Header("Character Movement Values")]
    [SerializeField]
    float _movementSpeed = 10f;
    [SerializeField]
    float _jumpForce = 10f;
    [SerializeField]
    float _climbSpeed = 1.5f;
    #endregion

    #region Public Properties
    public bool SetAutomaticMovement { get; set; }
    public bool IsOnLadder { get; set; }
    public bool IsShooting { get; set; }
    public bool IsDamaged { get; set; }
    #endregion

    #region Private Variables
    float _horizontalMovement;
    float _verticalMovement;
    float _currentGravityScale;

    Rigidbody2D _rbPlayer;
    Animator _animator;
    #endregion

    #region Unity Functions    
    void Awake()
    {
        _rbPlayer = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _currentGravityScale = _rbPlayer.gravityScale;
    }

    public virtual void Update()
    {
        _horizontalMovement = Input.GetAxis("Horizontal");
        _verticalMovement = Input.GetAxis("Vertical");

        SetAnimationByMovement();
        Jump();
    }

    private void SetAnimationByMovement()
    {        
        float directionToMove = GetDirectionToMove();
        if (directionToMove < 0 && !IsShooting && !IsOnLadder && !IsDamaged)
        {            
            _animator.SetTrigger(PlayerAnimationEnum.WalkingLeft.ToString());
        }
        else if (directionToMove > 0 && !IsShooting && !IsOnLadder && !IsDamaged)
        {            
            _animator.SetTrigger(PlayerAnimationEnum.Walk.ToString());
        }
        else if (IsOnLadder)
        {
            Climb();
        }
        else if(_verticalMovement != 0)
        {
            _animator.SetTrigger(PlayerAnimationEnum.Up.ToString());
        }
        else
        {
            _animator.SetTrigger(PlayerAnimationEnum.Idle.ToString());
        }
    }
    
    public void DisableGravityScale()
    {
        _rbPlayer.gravityScale = 0;
    }

    public void EnableGravityScale()
    {
        _rbPlayer.gravityScale = _currentGravityScale;
    }

    private void Climb()
    {
        _rbPlayer.gravityScale = 0f;
        Vector2 verticalVectorMovement = new Vector2(_rbPlayer.velocity.x, _verticalMovement * _climbSpeed);
        _animator.SetTrigger(PlayerAnimationEnum.Up.ToString());
        _rbPlayer.velocity = verticalVectorMovement;
    }

    public virtual void FixedUpdate()
    {
        Vector2 movement = (Vector2)transform.right * _movementSpeed * GetDirectionToMove();
        _rbPlayer.AddForce(movement);
    }
    #endregion

    #region Custom Functions
    public void Jump()
    {
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            _rbPlayer.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }
    }
    
    private bool IsGrounded()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_rbPlayer.position, 0.2f);
        return colliders.FirstOrDefault(x => x.gameObject.layer == LayerMask.NameToLayer("Floor"));
    }

    private float GetDirectionToMove()
    {
        return SetAutomaticMovement ? 1 : _horizontalMovement;
    }
    #endregion
}