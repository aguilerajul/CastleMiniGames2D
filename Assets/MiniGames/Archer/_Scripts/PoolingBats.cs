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
        GameObject barrelInstance = PoolManager.Instance.GetPooledObject();
        if (barrelInstance == null)
            return;

        Vector2 spawnPosition = this.transform.position;
        spawnPosition.y -= 0.5f;
        barrelInstance.transform.position = spawnPosition;
        barrelInstance.SetActive(true);
    }
    #endregion

}
