using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public const string SCORE_TEXT = "Score: ";
    
    #region Unity Editor Variables
    [Header("Time Game Settings")]
    [SerializeField]
    int _gameDuration = 15;
    [SerializeField]
    public int Difficulty { get; set; }
    #endregion

    #region Static Variables
    static int _miniGameDuration = 15;
    static MiniGameStats MiniGameStatsProp { get; set; }    
    #endregion

    #region Private Variables
    List<int> _scenesList;
    List<int> _scenesLoaded;
    int _currentSceneIndex;
    #endregion

    #region Constants
    public const int SCENE_MENU_INDEX = 1;
    public const int DEFAULT_SCENE_GAME_INDEX = 2;
    #endregion

    #region Properties
    public static GameManager Instance { get; private set; }
    public int BuildSettingsCount
    {
        get
        {
            return SceneManager.sceneCountInBuildSettings - 1;
        }
    }
    #endregion

    #region Unity Functions
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        Instance = this;
    }

    void Start()
    {
        Difficulty = 1;
        _scenesList = new List<int>();
        _scenesLoaded = new List<int>();
        SceneManager.LoadScene(SCENE_MENU_INDEX, LoadSceneMode.Additive);
        _miniGameDuration = _gameDuration;
    }
    #endregion

    #region Custom Functions
    public void StartMiniGames()
    {
        MiniGameStatsProp = new MiniGameStats
        {
            Completed = false,
            GameNameList = new List<string>(),
            Score = 0
        };
                
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
        SceneManager.LoadScene(_currentSceneIndex);
        
    }

    public void StartNextGame()
    {
        Difficulty += Mathf.Clamp(Difficulty, 1, 6);

        if (_scenesLoaded.Count >= BuildSettingsCount)
        {
            SceneManager.LoadScene("GameOver");
        }

        int rndSceneIndex = GetRandomSceneIndex();
        while(rndSceneIndex == _currentSceneIndex)
        {
            rndSceneIndex = GetRandomSceneIndex();
        }

        var nexSceneIndex = _scenesLoaded.FirstOrDefault(x => x == rndSceneIndex);
        if (nexSceneIndex == 0)
        {
            _scenesLoaded.Add(rndSceneIndex);
            SceneManager.LoadScene(_scenesLoaded[_scenesLoaded.Count -1]);
        }
        else
        {
            SceneManager.LoadScene("Final");
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

    public static string GetScore()
    {
        return GameManager.SCORE_TEXT + MiniGameStatsProp.Score;
    }

    public static void SetScore(int score)
    {
        MiniGameStatsProp.Score += score;
    }
    #endregion

}
