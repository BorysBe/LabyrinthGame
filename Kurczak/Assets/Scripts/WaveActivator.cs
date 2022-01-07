using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveActivator : MonoBehaviour
{
    [SerializeField] WaveConfig _waveConfig;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision!");
        StartCoroutine(SpawnAllEnemiesinWave());
    }

    private IEnumerator SpawnAllEnemiesinWave()
    {
        for (int enemyCount = 0; enemyCount < _waveConfig.GetNumberOfEnemies(); enemyCount++)
        {
            var newEnemy = Instantiate(_waveConfig.GetEnemyPrefab(), _waveConfig.GetWaypoints()[0].transform.position, Quaternion.identity);
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(_waveConfig);
            Animator _animator = newEnemy.GetComponentInParent<Animator>();
            _animator.SetBool("Shooting", true);
            yield return new WaitForSeconds(_waveConfig.GetTimeBetweenSpawns());
        }
    }
}
