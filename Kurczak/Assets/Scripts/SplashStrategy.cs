using UnityEngine;

interface ISplashStrategy
{
    void Splash(GameObject gameObject, float splashSpeed, Vector3 pos1, Vector3 pos2, Vector3 pos3);

    void SplashStop(GameObject gameObject);
}