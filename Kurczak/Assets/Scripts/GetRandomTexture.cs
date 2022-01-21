using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetRandomTexture : MonoBehaviour
{
    public Texture2D[] textures;

    public Texture2D ReturnTexture()
    {
        return textures[Random.Range(0, textures.Length - 1)];
    }
}
