using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlatformManager : MonoBehaviour
{
    [SerializeField]
    Text _txtScore;
    [SerializeField]
    GameObject _throwBarrel;
    [SerializeField]
    GameObject _throwBarrel2;

    int _difficulty = 1;

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
        if (GameManager.MiniGameStatsPropList != null && GameManager.MiniGameStatsPropList.Count > 1)
        {
            MiniGameStats mgs = GameManager.MiniGameStatsPropList.FirstOrDefault(x => x.GameName == ScenesEnum.PlatformGame.ToString());
            if (mgs != null)
            {
                _difficulty = mgs.Difficulty;
            }
        }

        if (_difficulty > 1)
        {
            _throwBarrel.SetActive(true);
        }

        if (_difficulty > 2)
        {
            _throwBarrel2.SetActive(true);
        }
    }

    private void Update()
    {
        _txtScore.text = GameManager.GetScore(Utilities.GetCurrentSceneName());
    }
}
