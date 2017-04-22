using UnityEngine;

public class LadderController : MonoBehaviour
{
    PlayerController _player;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
        
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _player.DisableGravityScale();
            _player.IsOnLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _player.EnableGravityScale();
            _player.IsOnLadder = false;
        }
    }
}
