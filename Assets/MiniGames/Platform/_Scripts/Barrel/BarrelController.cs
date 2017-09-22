using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelController : MonoBehaviour
{
    [SerializeField]
    float _timeToDisableBarrel = 12f;
    [SerializeField]
    int _playerDamage = 1;

    PlayerLife _playerLife;
    private GameObject _player;
    private Animator _playerAnimator;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        if (_player != null)
        {
            _playerLife = _player.GetComponent<PlayerLife>();
            _playerAnimator = _player.GetComponent<Animator>();
        }
        
    }

    private void OnEnable()
    {
        Invoke("DisableBarrel", _timeToDisableBarrel);
    }
   
    private void OnDisable()
    {
        CancelInvoke();
    }

    void DisableBarrel()
    {
        this.gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            _playerLife.Damage(_playerDamage);
            _playerAnimator.ResetTrigger(AnimationEnum.Up.ToString());
            _playerAnimator.ResetTrigger(AnimationEnum.Walk.ToString());
            _playerAnimator.ResetTrigger(AnimationEnum.WalkingLeft.ToString());

            _playerAnimator.SetTrigger(AnimationEnum.Damaged.ToString());

            DisableBarrel();
        }
    }
}
