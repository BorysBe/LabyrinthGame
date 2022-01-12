using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChavMove : StateMachineBehaviour
{
    GameObject _chav;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _chav = GameObject.FindGameObjectWithTag("Chav");
        _chav.GetComponent<CharacterStateAnimation>().move.enabled = true;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _chav.GetComponent<CharacterStateAnimation>().move.enabled = false;
        _chav.GetComponent<MoveEnemyBehaviour>().Stop();
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
