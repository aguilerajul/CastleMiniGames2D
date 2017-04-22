using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    float speed = 1.3f;

    public Transform _target { get; set; }

    Vector3 _offSet;

    private void Start()
    {
        _offSet = transform.position - _target.position;
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = _target.transform.position;
        targetPosition.y += 2.5f;
        targetPosition.x += 4.5f;
        targetPosition.z = this.transform.position.z;


        transform.position = Vector3.Lerp(this.transform.position, targetPosition, Time.deltaTime * speed);
    }
}
