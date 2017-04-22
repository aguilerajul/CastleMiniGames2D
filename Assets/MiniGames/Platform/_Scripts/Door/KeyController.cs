using UnityEngine;

public class KeyController : MonoBehaviour
{
    [SerializeField]
    float _catchDistance = 0.5f;

    PlayerController _player;

    private void Awake()
    {
        _player = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        RotateKey();
        GetKey();
    }

    private void RotateKey()
    {
        this.transform.rotation = new Quaternion(0.3f, 0.3f, 0, 0);
    }

    private void GetKey()
    {
        float distance = Utilities.CalculateDistance(_player.transform, this.transform);

        if (distance <= _catchDistance)
        {
            this.gameObject.SetActive(false);
            PlatformManager.PlayerGotTheKey = true;
        }
    }
}
