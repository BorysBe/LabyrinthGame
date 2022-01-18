using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChavDie : StateMachineBehaviour
{
    public GameObject _chavCorpseFragments;
    GameObject chavSpawn;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        EnemyFactory.Instance.SetSpawnPointFor("BloodyExplosion", animator.transform.position);
        EnemyFactory.Instance.SetSpawnPointFor("ChavGrave", animator.transform.position);
        Instantiate(_chavCorpseFragments, animator.transform.position, Quaternion.identity);
        animator.GetComponent<CharacterStateAnimation>().move.Stop();
        animator.GetComponent<MoveEnemyBehaviour>().Stop();
        animator.SetBool("ReturnToIdleState", true);
        animator.SetBool("Shooting", false);
        FindObjectOfType<Audio>().Play("WilhelmScream");


        chavSpawn = GameObject.FindGameObjectWithTag("ChavSpawn");
        chavSpawn.GetComponent<WaveActivator>().ActivateTrigger();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameObject[] gunshotWounds = GameObject.FindGameObjectsWithTag("GunshotWound");
        foreach (GameObject g in gunshotWounds)
        {
            if(g.transform.IsChildOf(animator.transform))
            {
                Destroy(g);
            }
        }
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
