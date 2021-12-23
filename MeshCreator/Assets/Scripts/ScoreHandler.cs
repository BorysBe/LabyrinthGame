using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private float scoreMultiplier;
    //public bool levelIsCompleted = false;

    SceneReloader _sceneReloader;

    public const string highScoreKey = "HighScore";
    public const string nextLevelPlayedKey = "NextLevelPlayed";
    public const string currentSessionScoreKey = "CurrentSessionScore";
    private int nextLevelPlayedInt;

    private float score = 1000;

    private void Start()
    {
        nextLevelPlayedInt = PlayerPrefs.GetInt(nextLevelPlayedKey);
        _sceneReloader = GameObject.Find("Finish Line").GetComponent<SceneReloader>();
        if (nextLevelPlayedInt == 1)
        {
            score += PlayerPrefs.GetInt(currentSessionScoreKey, 0);
        }
    }

    void Update()
    {
        score -= Time.deltaTime * scoreMultiplier;
        scoreText.text = Mathf.FloorToInt(score).ToString();
    }

    private void OnDestroy()
    {

        int currentHighScore = PlayerPrefs.GetInt(highScoreKey, 0);

        if(_sceneReloader.levelIsCompleted == true)
        {
            PlayerPrefs.SetInt(currentSessionScoreKey, Mathf.FloorToInt(score));
            PlayerPrefs.SetInt(nextLevelPlayedKey, 1);
            if (score > currentHighScore)
            {
                PlayerPrefs.SetInt(highScoreKey, Mathf.FloorToInt(score));
            }
        }
        else
        {
            PlayerPrefs.SetInt(currentSessionScoreKey, 0);
            PlayerPrefs.SetInt(nextLevelPlayedKey, 0);
        }

    }
}
