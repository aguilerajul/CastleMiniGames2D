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

    private void Awake()
    {
        _playerLife = FindObjectOfType<PlayerLife>();
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
            DisableBarrel();
        }
    }
}
