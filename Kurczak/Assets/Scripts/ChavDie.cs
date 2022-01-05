using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChavDie : StateMachineBehaviour
{

    GameObject _chav;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _chav = GameObject.FindGameObjectWithTag("Chav");
        var chavToucherBody = GameObject.FindGameObjectWithTag("ChavToucherBody");
        var chavToucherHead = GameObject.FindGameObjectWithTag("ChavToucherHead");
        chavToucherBody.GetComponent<ChavBodyHitbox>().enabled = false;
        chavToucherHead.GetComponent<ChavBodyHitbox>().enabled = false;
        _chav.GetComponent<EnemyPathing>().enabled = false;
        _chav.GetComponent<SpriteRenderer>().enabled = false;
        GameObject[] gunshotWounds = GameObject.FindGameObjectsWithTag("GunshotWound");
        foreach (GameObject g in gunshotWounds)
        {
            g.SetActive(false);
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
