using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Constants
    public const int SCENE_MENU_INDEX = 1;
    public const int DEFAULT_SCENE_GAME_INDEX = 2;
    public const string SCORE_TEXT = "Score: ";
    #endregion

    #region Unity Editor Variables
    [Header("Time Game Settings")]
    [SerializeField]
    int _gameDuration = 15;    
    #endregion

    #region Static Variables
    static int _miniGameDuration = 15;
    static int _difficultyLevel = 1;
    public static string _currentGameName;
    #endregion

    #region Private Variables
    List<int> _scenesList;
    List<int> _scenesLoaded;
    int _currentSceneIndex;    
    #endregion
    
    #region Properties
    public static GameManager Instance { get; private set; }
    int BuildSettingsCount
    {
        get
        {
            return SceneManager.sceneCountInBuildSettings - 2;
        }
    }

    #region Static Properties
    public static List<MiniGameStats> MiniGameStatsPropList { get; set; }
    #endregion

    #endregion

    #region Unity Functions
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        Instance = this;
    }

    void Start()
    {
        _miniGameDuration = _gameDuration;
        _scenesList = new List<int>();
        _scenesLoaded = new List<int>();        
        MiniGameStatsPropList = new List<MiniGameStats>();

        SceneManager.LoadScene(SCENE_MENU_INDEX, LoadSceneMode.Additive);
    }
    #endregion

    #region Custom Functions
    public void StartMiniGames()
    {
        _scenesList = new List<int>();
        _scenesLoaded = new List<int>();
        MiniGameStatsPropList = new List<MiniGameStats>();

        for (int i = DEFAULT_SCENE_GAME_INDEX; i < BuildSettingsCount; i++)
        {
            var randomSceneIndex = GetRandomSceneIndex();
            while(_scenesList.FirstOrDefault(x => x == randomSceneIndex) != 0 
                && i < BuildSettingsCount)
            {
                randomSceneIndex = GetRandomSceneIndex();                
            }
            _scenesList.Add(randomSceneIndex);
        }

        System.Random randomOrderIndex = new System.Random();
        var randomScenesList = _scenesList.OrderBy(x => randomOrderIndex.Next()).ToList();
        
        _currentSceneIndex = randomScenesList[0];
        _scenesLoaded.Add(_currentSceneIndex);

        MiniGameStatsPropList.Add(new MiniGameStats
        {
            Completed = false,
            Difficulty = _difficultyLevel,
            GameName = System.Enum.GetName(typeof(ScenesEnum), _currentSceneIndex)
        });
        
        SceneManager.LoadScene(_currentSceneIndex);
    }

    public void StartNextGame()
    {
        if(_difficultyLevel < 5)
        {
            _difficultyLevel += 1;
        }

        int buildSettings = BuildSettingsCount - 2;
        if (_scenesLoaded.Count >= buildSettings)
        {
            SceneManager.LoadScene("Score");
        }
        else
        {
            int rndSceneIndex = GetRandomSceneIndex();
            while (rndSceneIndex == _currentSceneIndex)
            {
                rndSceneIndex = GetRandomSceneIndex();
            }

            var nexSceneIndex = _scenesLoaded.FirstOrDefault(x => x == rndSceneIndex);
            if (nexSceneIndex == 0)
            {
                _scenesLoaded.Add(rndSceneIndex);

                int sceneIndex = _scenesLoaded[_scenesLoaded.Count - 1]; ;
                MiniGameStatsPropList.Add(new MiniGameStats
                {
                    Completed = false,
                    Difficulty = _difficultyLevel,
                    GameName = System.Enum.GetName(typeof(ScenesEnum), sceneIndex)
                });

                _currentGameName = System.Enum.GetName(typeof(ScenesEnum), sceneIndex);
                SceneManager.LoadScene(sceneIndex);
            }
            else if (_scenesLoaded.Count < (SceneManager.sceneCountInBuildSettings - 3))
            {
                StartNextGame();
            }
            else
            {
                SceneManager.LoadScene("Score");
            }
        }
    }

    private int GetRandomSceneIndex()
    {
        return Random.Range(DEFAULT_SCENE_GAME_INDEX, BuildSettingsCount);
    }

    public static int GetTimePerGame()
    {
        return _miniGameDuration;
    }

    public static string GetScore(string gameName)
    {
        MiniGameStats miniGameStats = MiniGameStatsPropList.FirstOrDefault(x => x.GameName == gameName);
        if(miniGameStats == null)
        {
            return GameManager.SCORE_TEXT + "0";
        }
        else
        {
            return GameManager.SCORE_TEXT + MiniGameStatsPropList.FirstOrDefault(x => x.GameName == gameName).Score;
        }       
    }

    public static void SetScore(int score, string gameName)
    {
        MiniGameStatsPropList.FirstOrDefault(x => x.GameName == gameName).Score += score;
    }
    #endregion

}
