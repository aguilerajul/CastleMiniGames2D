using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonController : MonoBehaviour
{
    #region Unity Editor Variables
    [Header("Character Movement Values")]
    [SerializeField]
    float _movementSpeed = 10f;
    [SerializeField]
    float _jumpForce = 10f;
    [SerializeField]
    float _climbSpeed = 1.5f;
    [SerializeField]
    float _minMovementWidth = -8;
    [SerializeField]
    float _maxMovementWidth = 0f;
    [SerializeField]
    float _minMovementHeigh = 2f;
    [SerializeField]
    float _maxMovementHeight = 4.2f;
    [SerializeField]
    float _fireRate = 1f;
    [SerializeField]
    AudioClip _fireSoundEffect;
    [SerializeField]
    float _flameForce;
    [SerializeField]
    GameObject _flamePrefab;
    [SerializeField]
    Transform _flameSpawnPoint;
    #endregion

    #region Private Variables
    float _horizontalMovement;
    float _verticalMovement;
    float _currentGravityScale;
    bool _isFiring;
    float _currentFireRate;
    bool _canShoot;

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
        _currentFireRate = _fireRate;
        _currentGravityScale = _rbPlayer.gravityScale;
    }

    public virtual void Update()
    {
        _horizontalMovement = Input.GetAxis("Horizontal");
        _verticalMovement = Input.GetAxis("Vertical");

        LimitPlayerMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _currentFireRate)
        {
            _currentFireRate = Time.time + _fireRate;
            _canShoot = true;
        }
        else
        {
            _canShoot = false;
        }
    }

    private void FixedUpdate()
    {
        SetMovement();

        if (_canShoot)
        {
            Fire();
        }
    }

    private void SetMovement()
    {
        Vector2 movement = new Vector2(_horizontalMovement, _verticalMovement) * _movementSpeed;
        _rbPlayer.AddForce(movement);
    }

    private void Fire()
    {
        var flameInstance = Instantiate(_flamePrefab, _flameSpawnPoint.position, Quaternion.identity);

        Rigidbody2D arrowRb = flameInstance.GetComponent<Rigidbody2D>();
        arrowRb.AddForce(Vector2.right * _flameForce, ForceMode2D.Impulse);
        if (_fireSoundEffect != null)
        {
            AudioSource.PlayClipAtPoint(_fireSoundEffect, this.transform.position);
        }
    }

    private void SetAnimationByMovement()
    {
        float directionToMove = _horizontalMovement;
        if (_isFiring)
        {
            _animator.SetTrigger(AnimationEnum.ShootingRight.ToString());
        }
        else
        {
            _animator.SetTrigger(AnimationEnum.Flying.ToString());
        }
    }

    void LimitPlayerMovement()
    {
        this.transform.position = Utilities.LimitPlayerMovementsInScene(this.transform, _minMovementWidth, _maxMovementWidth, _minMovementHeigh, _maxMovementHeight);
    }
    #endregion
}
