using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DragonManager : MonoBehaviour
{
    [SerializeField]
    Text _txtScore;
    [SerializeField]
    GameObject _fireBallPoolingDifficulty2;
    [SerializeField]
    GameObject _fireBallPoolingDifficulty3;


    int _difficulty = 1;

    private void Start()
    {
        if (GameManager.MiniGameStatsPropList != null && GameManager.MiniGameStatsPropList.Count > 1)
        {
            MiniGameStats mgs = GameManager.MiniGameStatsPropList.FirstOrDefault(x => x.GameName == ScenesEnum.DragonGame.ToString());
            if (mgs != null)
            {
                _difficulty = mgs.Difficulty;
            }
        }

        if (_difficulty > 1)
        {
            _fireBallPoolingDifficulty2.SetActive(true);
        }

        if (_difficulty > 2)
        {
            _fireBallPoolingDifficulty3.SetActive(true);
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
