using UnityEngine;

public class Coins : MonoBehaviour
{
    [SerializeField]
    float _distance = 2f;
    [SerializeField]
    GameObject _coinParticle;

    PlayerController _player;

    private void Awake()
    {
        _player = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        GetCoins();
    }

    private void GetCoins()
    {
        if (_player != null)
        {
            if (Utilities.CalculateDistance(_player.transform, this.transform) <= _distance)
            {
                var particlePrefab = Instantiate(_coinParticle, this.transform.position, Quaternion.identity);
                Destroy(this.gameObject);
                Destroy(particlePrefab, 2f);
                GameManager.SetScore(1, Utilities.GetCurrentSceneName());
            }
        }
    }
}
