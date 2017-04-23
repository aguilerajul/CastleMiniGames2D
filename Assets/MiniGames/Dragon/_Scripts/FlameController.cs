using UnityEngine;

public class FlameController : MonoBehaviour
{
    [SerializeField]
    float _timeToDestroy = 5f;

    // Use this for initialization
    void Start()
    {
        Destroy(this.gameObject, _timeToDestroy);
    }
}
