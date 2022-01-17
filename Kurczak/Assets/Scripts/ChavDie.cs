using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChavDie : StateMachineBehaviour
{
    GameObject chav;
    public GameObject _chavCorpseFragments;
    GameObject chavSpawn;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        chav = GameObject.FindGameObjectWithTag("Chav");
        FindObjectOfType<Audio>().Play("WilhelmScream");
        Vector3 instantionPosition = chav.transform.position;
        Instantiate(_chavCorpseFragments, instantionPosition, Quaternion.identity);
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
            Destroy(g);
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
