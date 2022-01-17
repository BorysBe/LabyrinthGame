using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemainsSpawner : MonoBehaviour
{
    public GameObject[] _RemainsPrefab;
    [SerializeField] int numberOfRemains = 100;
    [SerializeField][Range(-5f, 0f)] float xMinRange = -5f;
    [SerializeField] [Range(0f, -5f)] float xMaxRange = 5f;
    [SerializeField] [Range(-5f, -2f)] float yMinRange = -5f;
    [SerializeField] [Range(-2f, 0f)] float yMaxRange = -1f;
    [SerializeField] [Range(-360f, 0f)] float rotationNegative = 0f;
    [SerializeField] [Range(0f, 360f)] float rotationPositive = 0f;
    GameObject fragment;

    void Start()
    {
        SpillRemains();
    }

    void SpillRemains()
    {
        for(int i = 0; i < numberOfRemains; i ++)
        {
            fragment = Instantiate(_RemainsPrefab[Random.Range(0, _RemainsPrefab.Length)],
                new Vector3(Random.Range(xMinRange, xMaxRange)+ this.transform.position.x, Random.Range(yMinRange, yMaxRange), 1f),
                Quaternion.identity);
            fragment.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(rotationNegative, rotationPositive)));
            fragment.transform.SetParent(this.GetComponentInParent<Transform>());
        }
    }
}
