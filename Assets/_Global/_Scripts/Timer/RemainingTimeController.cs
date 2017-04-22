using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RemainingTimeController : MonoBehaviour
{

    [SerializeField]
    Text _remainingTimeText;

    int _currentRemainingTime;

    private void Start()
    {
        _currentRemainingTime = GameManager.GetTimePerGame();
        StartCoroutine(TimeCourutine());
    }

    private void Update()
    {
        if (_currentRemainingTime <= 0)
        {
            StopCoroutine(TimeCourutine());
            GameManager.Instance.StartNextGame();
        }
    }

    IEnumerator TimeCourutine()
    {
        while (_currentRemainingTime > 0)
        {
            _currentRemainingTime--;
            yield return new WaitForSeconds(1f);
            _remainingTimeText.text = "Remaining Time: " + _currentRemainingTime;
        }
    }
}
