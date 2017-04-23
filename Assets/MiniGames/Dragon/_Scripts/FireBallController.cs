using UnityEngine;

public class FireBallController : MonoBehaviour
{
    [SerializeField]
    float _fireBallForce;
    [SerializeField]
    AudioClip _fireBallEffect;
    [SerializeField]
    int _playerDamage;
    [SerializeField]
    int _timeToFireBall;

    PlayerLife _playerLife;
    Rigidbody2D _rigbFireBall;

    private void Awake()
    {
        _playerLife = FindObjectOfType<PlayerLife>();
        _rigbFireBall = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        ThrowFireBall();
    }

    private void ThrowFireBall()
    {
        _rigbFireBall.AddForce(Vector2.left * _fireBallForce, ForceMode2D.Impulse);
        if (_fireBallEffect != null)
        {
            AudioSource.PlayClipAtPoint(_fireBallEffect, this.transform.position);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("DragonFlame"))
        {
            DisableFireBall();
            Destroy(collision.collider.gameObject);

            GameManager.SetScore(1, Utilities.GetCurrentSceneName());
        }

        if (collision.collider.CompareTag("Player"))
        {
            _playerLife.Damage(_playerDamage);
            DisableFireBall();
        }
    }

    private void OnEnable()
    {
        Invoke("DisableFireBall", _timeToFireBall);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    void DisableFireBall()
    {
        this.gameObject.SetActive(false);
    }
}
