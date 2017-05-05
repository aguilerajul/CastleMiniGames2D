using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MessageBoxMenu : MonoBehaviour
{
    public const string MESSAGE_BOX_SCENE = "MessageBox";

    [SerializeField]
    Text _contentText;

    private static string _message;
    private static float _seconds;
    private static bool _isVisible;

    public static bool IsMessageFinished;

    public static void ShowMessage(string message, float seconds)
    {
        if (_isVisible) return;

        _isVisible = true;

        MessageBoxMenu._message = message;
        MessageBoxMenu._seconds = seconds;

        SceneManager.LoadScene(MESSAGE_BOX_SCENE, LoadSceneMode.Additive);
    }

    void Start()
    {       
        StartCoroutine(ShowMessage_Corrutine(MessageBoxMenu._message, MessageBoxMenu._seconds));
        IsMessageFinished = true;
    }

    IEnumerator ShowMessage_Corrutine(string message, float seconds)
    {
        _contentText.text = string.Empty;

        float delayPerChar = seconds / message.Length;
        WaitForSeconds wait = new WaitForSeconds(delayPerChar);

        foreach (char c in message)
        {
            _contentText.text += c;
            yield return wait;
        }
        
        yield return new WaitForSeconds(2);
        yield return SceneManager.UnloadSceneAsync(MESSAGE_BOX_SCENE);
    }

    void OnDestroy()
    {
        MessageBoxMenu._isVisible = false;
    }
}
