using System.Collections;
using UnityEngine;

public class PlayerLife : LifeController
{
    [SerializeField]
    float _minFallingPositionInYTodie;

    GameObject _player;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if(_player.transform.position.y <= _minFallingPositionInYTodie)
        {
            Die();
        }
    }

    protected override void Die()
    {
        _player.GetComponent<Animator>().SetTrigger(PlayerAnimationEnum.Die.ToString());

        ChangeGame();
    }

    void ChangeGame()
    {
        StartCoroutine(StartNextGameCourutine());
    }

    IEnumerator StartNextGameCourutine()
    {
        yield return new WaitForSeconds(2);

        GameManager.Instance.StartNextGame();
    }
}
