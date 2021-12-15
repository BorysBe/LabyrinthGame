using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    //PARAMETERS - for tunning, typically set in the editor
    [SerializeField] float levelLoadDelay = 1f;

    //STATE - private instance (member) variables
    CapsuleCollider capsuleCollider;

    bool isTransitioning = false;

    // Start is called before the first frame update
    void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    void OnCollisionEnter(CollisionHandler other)
    {
        switch (other.gameObject.tag)
        {
            case "Finish":
                LoadNextLevelSequence();
                break;
        }
    }

    void LoadNextLevelSequence()
    {
        //audioSource.Stop();
        //audioSource.PlayOneShot(soundOfSuccess);
        //explosionOfSuccess.Play();
        //GetComponent<RigidbodyFirstPersonController>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);
        isTransitioning = true;
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        //It assures that current scene will be realoaded.
        SceneManager.LoadScene(currentSceneIndex);
    }
}
