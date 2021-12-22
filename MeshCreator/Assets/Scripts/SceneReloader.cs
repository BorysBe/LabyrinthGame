using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneReloader : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 1f;

    private void OnTriggerEnter(Collider other)
    {
        Invoke("ReloadLevel", levelLoadDelay);
    }
    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        //It assures that current scene will be realoaded.
        SceneManager.LoadScene(currentSceneIndex);
    }
}
