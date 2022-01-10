using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChavCorpseFragments : MonoBehaviour
{
    public GameObject[] _corpseFragmentsPrefab;
    [SerializeField] int numberOfCorpseFragments = 100;
    GameObject corpseFragment;

    void Start()
    {
        SplashCorpseFragments();
    }

    void SplashCorpseFragments()
    {
        for(int i = 0; i < numberOfCorpseFragments; i ++)
        {
            corpseFragment = Instantiate(_corpseFragmentsPrefab[Random.Range(0, _corpseFragmentsPrefab.Length)], new Vector3(Random.Range(-5f, 5f)+ this.transform.position.x, Random.Range(-5f, -1f), 0f), Quaternion.identity);
            corpseFragment.transform.SetParent(this.GetComponentInParent<Transform>());
        }
    }
}
