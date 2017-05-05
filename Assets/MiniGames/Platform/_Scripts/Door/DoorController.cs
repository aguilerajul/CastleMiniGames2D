using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField]
    GameObject _closedDoorPrefab;
    [SerializeField]
    GameObject _openedDoorPrefab;
    
    PlayerController _player;
    Animator _playerAnimator;

    private void Awake()
    {
        _player = FindObjectOfType<PlayerController>();
        _playerAnimator = _player.GetComponent<Animator>();

        _openedDoorPrefab.SetActive(false);
        _closedDoorPrefab.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && PlatformManager.PlayerGotTheKey)
        {
            _openedDoorPrefab.SetActive(true);
            _closedDoorPrefab.SetActive(false);
            _playerAnimator.SetTrigger("Up");

            GameManager.Instance.StartNextGame();
        }
    }
}
