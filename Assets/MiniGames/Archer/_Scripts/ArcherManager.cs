using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ArcherManager : MonoBehaviour
{
    [SerializeField]
    Text _txtScore;
    [SerializeField]
    GameObject _batPoolingDifficulty2;
    [SerializeField]
    GameObject _batPoolingDifficulty2_1;
    [SerializeField]
    GameObject _batPoolingDifficulty3;
    [SerializeField]
    GameObject _batPoolingDifficulty3_1;

    int _difficulty = 1;

    private void Start()
    {
        SetDifficulty();
        //MessageBoxMenu.ShowMessage("Survive to the bats attack by 30 seconds, you can move with wasd or arrows key and fire with the left click of mouse, GOOD LUCK!!!", 10);
    }

    private void SetDifficulty()
    {
        if (GameManager.MiniGameStatsPropList != null && GameManager.MiniGameStatsPropList.Count > 1)
        {
            MiniGameStats mgs = GameManager.MiniGameStatsPropList.FirstOrDefault(x => x.GameName == ScenesEnum.ArcherGame.ToString());
            if (mgs != null)
            {
                _difficulty = mgs.Difficulty;
            }
        }

        if (_difficulty > 1)
        {
            _batPoolingDifficulty2.SetActive(true);
            _batPoolingDifficulty2_1.SetActive(true);
        }

        if (_difficulty > 2)
        {
            _batPoolingDifficulty3.SetActive(true);
            _batPoolingDifficulty3_1.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        DisplayScore();
    }

    void DisplayScore()
    {
        _txtScore.text = GameManager.GetScore(Utilities.GetCurrentSceneName());
    }
}
