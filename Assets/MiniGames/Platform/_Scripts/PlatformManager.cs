using UnityEngine;
using UnityEngine.UI;

public class PlatformManager : MonoBehaviour
{
    [SerializeField]
    Text _txtScore;

    public static bool PlayerGotTheKey {get; set;}

    GameObject _player;
    CameraController _cameraController;
    
    private void Awake()
    {
        _cameraController = Camera.main.GetComponent<CameraController>();
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    // Use this for initialization
    void Start()
    {
        _cameraController._target = _player.transform;
    }

    private void Update()
    {
        _txtScore.text = GameManager.GetScore();
    }
}
