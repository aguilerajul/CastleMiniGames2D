using UnityEngine;

public class PoolingBats : MonoBehaviour
{
    #region Unity Parameters
    [SerializeField]
    float _timeToSpawn = 5f;
    #endregion

    #region Unity Methods
    void Start()
    {
        InvokeRepeating("SpawnBats", _timeToSpawn, _timeToSpawn);
    }
    #endregion

    #region Custom Methods
    void SpawnBats()
    {
        GameObject batInstance = PoolManager.Instance.GetPooledObject();
        if (batInstance == null)
            return;

        Vector2 spawnPosition = this.transform.position;
        spawnPosition.y -= 0.5f;
        batInstance.transform.position = spawnPosition;
        batInstance.SetActive(true);
    }
    #endregion

}
