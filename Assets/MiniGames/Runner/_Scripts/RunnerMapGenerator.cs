using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RunnerMapGenerator : MonoBehaviour
{
    [SerializeField]
    Text _txtScore;
    [SerializeField]
    GameObject _playerPrefab;
    [SerializeField]
    GameObject[] _floorPrefabs;
    [SerializeField]
    GameObject[] _coinsPrefabs;
    [SerializeField]
    bool _activateDebugMode;
    [SerializeField]
    GameObject _enemyPrefab;

    bool _generatedMap;
    int _remaningTime;
    CameraController _cameraController;

    private void Awake()
    {
        _cameraController = Camera.main.GetComponent<CameraController>();
    }

    // Use this for initialization
    void Start()
    {   
        _remaningTime = GameManager.GetTimePerGame();
        CreateGameObjects();
        //MessageBoxMenu.ShowMessage("Run as far as you can and get the coins, but be careful with the stakes, GOOD LUCK!!!", 10);
    }

    private void Update()
    {       
        _txtScore.text = GameManager.GetScore(Utilities.GetCurrentSceneName());        
    }

    private void CreateGameObjects()
    {
        if (_floorPrefabs != null)
        {
            System.Random randomIndex = new System.Random();
            GameObject[] floorPrefabRamdon = _floorPrefabs.OrderBy(x => randomIndex.Next()).ToArray();
            GameObject[] coinsPrefabRamdon = _coinsPrefabs != null ? _coinsPrefabs.OrderBy(x => randomIndex.Next()).ToArray() : new GameObject[] { };
            Vector3 spawnPosition = this.transform.position;
            
            //Generate Floor
            for (int i = 0; i < floorPrefabRamdon.Length; i++)
            {
                var collider = floorPrefabRamdon[i].GetComponent<BoxCollider2D>();
                Vector2 colliderSize = collider.size;

                Vector3 finalSpawnFloorPosition = spawnPosition;
                finalSpawnFloorPosition.y = Random.Range(-1.5f, finalSpawnFloorPosition.y);

                //Instanciate Player
                if (i == 0)
                {                    
                    InstantiatePlayer(spawnPosition);
                }

                //Instanciate Floor
                var floorInstance = Instantiate(floorPrefabRamdon[i], finalSpawnFloorPosition, Quaternion.identity);

                //Instanciate Coins
                InstanciateCoins(coinsPrefabRamdon, floorInstance.GetComponentInChildren<EmptyClass>().transform.position);

                spawnPosition.x += colliderSize.x + 2f;
            }
        }
        else if (_activateDebugMode)
        {
            Debug.Log("You need to add the Floor Prefab");
        }
    }
    
    private void InstantiatePlayer(Vector3 spawnPosition)
    {
        if(_playerPrefab !=null)
        {
            spawnPosition.y += 3f;
            spawnPosition.x += 1f;                        
            var playerInstance = Instantiate(_playerPrefab, spawnPosition, Quaternion.identity);
            playerInstance.GetComponent<PlayerController>().SetAutomaticMovement = true;
            _cameraController._target = playerInstance.transform;
        }
        else if (_activateDebugMode)
        {
            Debug.Log("You Need to add the playerPrefab");
        }
    }

    private static void InstanciateCoins(GameObject[] coinsPrefabRamdon, Vector3 spawnPosition)
    {
        if (coinsPrefabRamdon.Length > 0)
        {
            System.Random randomCoinIndex = new System.Random();
            int coinIndex = randomCoinIndex.Next(coinsPrefabRamdon.Length);
            var coinSpawmPoisition = spawnPosition;
            coinSpawmPoisition.y = Random.Range(1f, 2.8f);
            Instantiate(coinsPrefabRamdon[coinIndex], coinSpawmPoisition, Quaternion.identity);
        }
        else
        {
            Debug.Log("You need to add the coinsPrefab");
        }
        
    }
}
