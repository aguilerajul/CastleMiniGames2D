using System.Collections;
using System.Linq;
using UnityEngine;

public class ArcherController : MonoBehaviour
{
    #region Unity Parameters
    [SerializeField]
    GameObject _arrowPrefab;
    [SerializeField]
    float _arrowThrowForce;
    [SerializeField]
    Transform _arrowSpawnPosition;
    [SerializeField]
    float _throwArrowRate = 1f;
    [SerializeField]
    AudioClip _arrowEffect;
    [SerializeField]
    float _maxMovementWidth = 10f;
    [SerializeField]
    float _minMovementWidth = -10f;
    [SerializeField]
    float _maxMovementHeight = 10f;
    [SerializeField]
    float _minMovementHeight = -10f;
    #endregion

    #region Private Variables
    float _currentThrowArrowRate;
    PlayerController _playerController;
    bool _canShoot;
    #endregion

    #region Unity Methods
    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
    }

    private void Start()
    {
        _currentThrowArrowRate = _throwArrowRate;
    }

    private void Update()
    {
        LimitPlayerMovement();

        if (Input.GetAxis("Fire1") > 0 && Time.time > _currentThrowArrowRate)
        {
            _currentThrowArrowRate = Time.time + _throwArrowRate;
            _canShoot = true;
        }
        else
        {
            _canShoot = false;
        }
    }

    private void FixedUpdate()
    {
        if (_canShoot)
        {
            FireWithMouseDirection();
        }
    }
    #endregion

    #region Custom Methods
    private void FireWithMouseDirection()
    {
        Vector2 direction = new Vector2();
        float angle = GetMouseAngle(out direction);
        SetAnimationByAngle(angle);
        ThrowArrow(angle, direction);
    }

    private float GetMouseAngle(out Vector2 direction)
    {
        Vector3 worldMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldMouse.z = 0;

        direction = (worldMouse - transform.position).normalized;
        float angle = -Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;

        return angle;
    }

    private void ThrowArrow(float angle, Vector2 direction)
    {
        Vector2 _newSpawnPosition = _arrowSpawnPosition.position;

        if (angle < 45f && angle > -45f)
        {
            _newSpawnPosition.y += 1f;
        }
        else if (angle > 45f)
        {
            _newSpawnPosition.x -= 1f;
        }
        else if (angle < -45f)
        {
            _newSpawnPosition.x += 1.3f;
        }

        //TODO: ThrowArrows
        var arrowInstance = Instantiate(_arrowPrefab, _newSpawnPosition, Quaternion.AngleAxis(angle, Vector3.forward));

        Rigidbody2D arrowRb = arrowInstance.GetComponent<Rigidbody2D>();
        arrowRb.AddForce(new Vector2(direction.x, direction.y) * _arrowThrowForce, ForceMode2D.Impulse);
        if (_arrowEffect != null)
        {
            AudioSource.PlayClipAtPoint(_arrowEffect, this.transform.position);
        }

    }

    private void SetAnimationByAngle(float angle)
    {
        _playerController.IsShooting = true;
        if (angle < 45f && angle > -45f)
        {
            _playerController.GetComponent<Animator>().SetTrigger(AnimationEnum.ShootingUp.ToString());
        }
        else if (angle > 45f)
        {
            _playerController.GetComponent<Animator>().SetTrigger(AnimationEnum.ShootingLeft.ToString());
        }
        else if (angle < -45f)
        {
            _playerController.GetComponent<Animator>().SetTrigger(AnimationEnum.ShootingRight.ToString());
        }
        SetAnimationShooting();
    }

    void SetAnimationShooting()
    {
        StartCoroutine(SetAnimationShootingCourutine());
    }

    IEnumerator SetAnimationShootingCourutine()
    {
        yield return new WaitForSeconds(1f);

        _playerController.IsShooting = false;
    }

    void StopCourutines()
    {
        StopCoroutine(SetAnimationShootingCourutine());
    }

    void LimitPlayerMovement()
    {
        _playerController.transform.position = Utilities.LimitPlayerMovementsInScene(_playerController.transform, _minMovementWidth, _maxMovementWidth, _minMovementHeight, _maxMovementHeight);
    }

    #endregion
}
