using UnityEngine;

public class WaveActivator : MonoBehaviour
{
    [SerializeField] int timeToActivateWave = 100;
    [SerializeField] int timetoRectivateTimer = 5000;
    [SerializeField] PrefabType spawnedEnemy;
    private bool startTrigger = true;

    private void OnTriggerEnter(Collider other)
    {
        if (startTrigger)
        {
            Debug.Log("Collision!");
            CoroutineTimer coroutineTimer = new CoroutineTimer(timeToActivateWave, this);
            coroutineTimer.Tick += delegate ()
            {
                SpawnEnemy(coroutineTimer);
            };
            coroutineTimer.Play();
            DeactivateTrigger();
            CoroutineTimer coroutineToReactivateTrigger = new CoroutineTimer(timetoRectivateTimer, this);
            coroutineToReactivateTrigger.Tick += delegate ()
            {
                ActivateTriggerByCoroutine(coroutineToReactivateTrigger);
            };
            coroutineToReactivateTrigger.Play();
        }
    }
 
    private void SpawnEnemy(CoroutineTimer coroutineTimer)
    {
        var newEnemy = EnemyFactory.Instance.SpawnAtDeveloperRoom(spawnedEnemy, null);
        Animator _animator = newEnemy.GameObject.GetComponentInParent<Animator>();
        _animator.SetBool("Shooting", true);
        coroutineTimer.Stop();
    }

    public void ActivateTrigger()
    {
        startTrigger = true;
    }

    public void DeactivateTrigger()
    {
        startTrigger = false;
    }

    private void ActivateTriggerByCoroutine(CoroutineTimer coroutineTimer)
    {
        startTrigger = true;
        coroutineTimer.Stop();
    }
}
