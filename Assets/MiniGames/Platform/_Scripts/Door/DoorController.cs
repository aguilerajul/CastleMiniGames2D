using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField]
    float _distanceToOpen = 0.5f;
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

    void Update()
    {
        float distance = Utilities.CalculateDistance(_player.transform, this.transform);

        if (distance <= _distanceToOpen && PlatformManager.PlayerGotTheKey)
        {
            _openedDoorPrefab.SetActive(true);
            _closedDoorPrefab.SetActive(false);
            _playerAnimator.SetTrigger("Up");

            GameManager.Instance.StartNextGame();
        }
    }
}
