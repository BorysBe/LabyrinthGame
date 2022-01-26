using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTextureFactory : MonoBehaviour
{
    public Texture2D[] textures;

    public Texture2D CreateRandomTexture()
    {
        return textures[Random.Range(0, textures.Length - 1)];
    }
}
