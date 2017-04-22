using UnityEngine;

public class Stakes : MonoBehaviour
{
    [SerializeField]
    int _damage;

    GameObject _player;

    private void Awake()
    {
        _player = GameObject.FindWithTag("Player");
    }

    private void Start()
    {
        if (_damage == 0)
            _damage = 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _player.GetComponent<Animator>().SetTrigger(PlayerAnimationEnum.Damaged.ToString());
            _player.GetComponent<PlayerLife>().Damage(_damage);
        }
    }
}
