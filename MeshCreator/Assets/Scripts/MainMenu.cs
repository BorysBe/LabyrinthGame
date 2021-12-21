using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text highScoreText;

    private void Start()
    {
        int highScore = PlayerPrefs.GetInt(ScoreHandler.highScoreKey, 0);

        highScoreText.text = $"HIGH SCORE: {highScore}";
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
