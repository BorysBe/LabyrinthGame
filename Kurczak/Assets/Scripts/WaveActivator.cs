using System.Collections.Generic;
using UnityEngine;

public class WaveActivator : MonoBehaviour
{
    [SerializeField] int timeToActivateWave = 1;
    [SerializeField] Vector3 spawnPoint; 

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision!");
        CoroutineTimer coroutineTimer = new CoroutineTimer(timeToActivateWave, this);
        coroutineTimer.Tick += delegate ()
        {
            SpawnEnemy(coroutineTimer);
        };
        coroutineTimer.Start();
    }

    private void SpawnEnemy(CoroutineTimer coroutineTimer)
    {
        var newEnemy = ObjectPooler.Instance.SpawnFromPool("Chav", spawnPoint, Quaternion.identity);
        Animator _animator = newEnemy.GetComponentInParent<Animator>();
        _animator.SetBool("Moving", true);
        _animator.SetBool("Shooting", true);
        newEnemy.GetComponent<MoveEnemyBehaviour>().Play();
        coroutineTimer.Stop();
    }
}
