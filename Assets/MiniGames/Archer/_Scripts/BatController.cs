using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class BatController : MonoBehaviour
{
    #region Unity Parameters
    [SerializeField]
    float _timeToBats = 7f;
    [SerializeField]
    int _playerDamage = 1;
    [SerializeField]
    float _distanceToAttack;
    [SerializeField]
    float _movementSpeed = 3f;
    #endregion

    #region Private Variables
    PlayerLife _playerLife;
    Rigidbody2D _batRg;
    #endregion

    #region Unity Methods
    private void Awake()
    {
        _playerLife = FindObjectOfType<PlayerLife>();
        _batRg = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector3 direction = _playerLife.transform.position - this.transform.position;
        float distance = direction.sqrMagnitude;
        _batRg.MovePosition(transform.position + direction.normalized * _movementSpeed * Time.fixedDeltaTime);
    }

    private void OnEnable()
    {
        Invoke("DisableBat", _timeToBats);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Arrow"))
        {
            DisableBat();
            Destroy(collision.collider.gameObject);

            GameManager.SetScore(1, Utilities.GetCurrentSceneName());
        }

        if (collision.collider.CompareTag("Player"))
        {
            _playerLife.Damage(_playerDamage);
            DisableBat();
        }
    }
    #endregion

    #region Custom Methods
    void DisableBat()
    {
        this.gameObject.SetActive(false);
    }
    #endregion
}
