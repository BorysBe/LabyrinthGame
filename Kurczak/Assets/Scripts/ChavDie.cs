using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChavDie : StateMachineBehaviour
{

    GameObject _chav;
    public GameObject _chavCorpseFragments;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _chav = GameObject.FindGameObjectWithTag("Chav");
        FindObjectOfType<Audio>().Play("WilhelmScream");
        GameObject[] gunshotWounds = GameObject.FindGameObjectsWithTag("GunshotWound");
        Vector3 instantionPosition = _chav.transform.position;
        Instantiate(_chavCorpseFragments, instantionPosition, Quaternion.identity);
        foreach (GameObject g in gunshotWounds)
        {
            Destroy(g);
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
