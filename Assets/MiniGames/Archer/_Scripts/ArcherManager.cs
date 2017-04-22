using UnityEngine;
using UnityEngine.UI;

public class ArcherManager : MonoBehaviour
{
    [SerializeField]
    Text _txtScore;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _txtScore.text = GameManager.GetScore();
    }
}
