using System.Collections;
using UnityEngine;

public class PlayerLife : LifeController
{
    GameObject _player;
    Animator _playerAnimator;
    float _minPosition;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _minPosition = _player.transform.position.y - 5f;
    }

    private void Update()
    {
        if (_player.transform.position.y <= _minPosition)
        {
            Die();
        }
    }

    protected override void Die()
    {
        _player.GetComponent<PlayerController>().IsDead = true;
        ChangeGame();
    }

    void ChangeGame()
    {
        StartCoroutine(StartNextGameCourutine());
    }

    IEnumerator StartNextGameCourutine()
    {
        yield return new WaitForSeconds(1);

        GameManager.Instance.StartNextGame();
    }
}
