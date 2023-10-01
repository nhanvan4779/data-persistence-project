#if UNITY_EDITOR
using TMPro;
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUIHandler : MonoBehaviour
{
    public TMP_Text BestScoreText;

    private void OnEnable()
    {
        DataManager.Instance.OnSaveDataLoaded += UpdateBestScoreText;
    }

    private void OnDisable()
    {
        DataManager.Instance.OnSaveDataLoaded -= UpdateBestScoreText;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void NameEntered(string name)
    {
        DataManager.Instance.PlayerName = name;
    }

    public void UpdateBestScoreText()
    {
        BestScoreText.text = $"Best Score : {DataManager.Instance.BestScorePlayerName} : {DataManager.Instance.BestScore}";
    }
}
