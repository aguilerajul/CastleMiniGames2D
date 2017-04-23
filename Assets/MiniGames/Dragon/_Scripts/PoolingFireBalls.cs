using UnityEngine;

public class PoolingFireBalls : MonoBehaviour
{
    #region Unity Parameters
    [SerializeField]
    float _timeToSpawn = 5f;
    #endregion

    #region Unity Methods
    void Start()
    {
        InvokeRepeating("SpawnFireBall", _timeToSpawn, _timeToSpawn);
    }
    #endregion

    #region Custom Methods
    void SpawnFireBall()
    {
        GameObject fireBallInstance = PoolManager.Instance.GetPooledObject();
        if (fireBallInstance == null)
            return;

        Vector2 spawnPosition = this.transform.position;
        spawnPosition.y -= 0.5f;
        fireBallInstance.transform.position = spawnPosition;
        fireBallInstance.SetActive(true);
    }
    #endregion

}
