using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    //public static bool _gameIsPaused = false;

    public GameObject _pauseMenuUI;
    public GameObject _mainCamera;

    private void Start()
    {
        _pauseMenuUI.SetActive(false);
    }

    public void Pause()
    {
        // _gameIsPaused = true;
        _pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        _mainCamera.GetComponent<Movement>().enabled = false;
    }

    public void Resume()
    {
        //_gameIsPaused = true;
        _pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        _mainCamera.GetComponent<Movement>().enabled = true;
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
