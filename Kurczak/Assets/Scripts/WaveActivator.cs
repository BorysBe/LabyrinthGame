 using System.Collections.Generic;
 using UnityEngine;
 
 public class WaveActivator : MonoBehaviour
{
    [SerializeField] int timeToActivateWave = 100;
    [SerializeField] Vector3 spawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision!");
        CoroutineTimer coroutineTimer = new CoroutineTimer(timeToActivateWave, this);
        coroutineTimer.Tick += delegate ()
        {
            SpawnEnemy(coroutineTimer);
        };
        coroutineTimer.Play();
    }
 
    private void SpawnEnemy(CoroutineTimer coroutineTimer)
    {
        var newEnemy = EnemyFactory.Instance.Spawn("Chav", spawnPoint, Quaternion.identity);
        Animator _animator = newEnemy.GetComponentInParent<Animator>();
        _animator.SetBool("Shooting", true);
        coroutineTimer.Stop();
    }
}
