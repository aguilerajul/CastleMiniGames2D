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
    public bool IsDead { get; set; }
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
        if (IsDead)
        {
            _animator.ResetTrigger(AnimationEnum.Walk.ToString());
            _animator.ResetTrigger(AnimationEnum.WalkingLeft.ToString());
            _animator.ResetTrigger(AnimationEnum.Up.ToString());

            _animator.SetTrigger(AnimationEnum.Idle.ToString());
            _animator.SetTrigger(AnimationEnum.Die.ToString());

            return;
        }

        _horizontalMovement = Input.GetAxis("Horizontal");
        _verticalMovement = Input.GetAxis("Vertical");
        SetAnimationByAction();
        Jump();
    }

    private void SetAnimationByAction()
    {
        float directionToMove = GetHorizontalDirectionToMove();        
        if (directionToMove < 0 && !IsShooting && !IsOnLadder)
        {
            _animator.ResetTrigger(AnimationEnum.Up.ToString());
            _animator.ResetTrigger(AnimationEnum.Walk.ToString());

            _animator.SetTrigger(AnimationEnum.WalkingLeft.ToString());
        }
        else if (directionToMove > 0 && !IsShooting && !IsOnLadder)
        {
            _animator.ResetTrigger(AnimationEnum.Up.ToString());
            _animator.ResetTrigger(AnimationEnum.WalkingLeft.ToString());

            _animator.SetTrigger(AnimationEnum.Walk.ToString());
        }
        else if (IsOnLadder && _verticalMovement != 0)
        {
            directionToMove = 0;
            
            _animator.ResetTrigger(AnimationEnum.Walk.ToString());
            _animator.ResetTrigger(AnimationEnum.WalkingLeft.ToString());

            _animator.SetTrigger(AnimationEnum.Up.ToString());
            Climb();
        }
        else if(!IsOnLadder)
        {
            _animator.SetTrigger(AnimationEnum.Idle.ToString());
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
        _animator.SetTrigger(AnimationEnum.Up.ToString());
        _rbPlayer.velocity = verticalVectorMovement;
    }

    public virtual void FixedUpdate()
    {
        if (IsDead || IsOnLadder)
            return;

        Vector2 movement = (Vector2)transform.right * _movementSpeed * GetHorizontalDirectionToMove();
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

    private float GetHorizontalDirectionToMove()
    {
        return SetAutomaticMovement ? 1 : _horizontalMovement;
    }
    #endregion
}