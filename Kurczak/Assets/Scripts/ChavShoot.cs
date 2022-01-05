using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChavShoot : StateMachineBehaviour
{
    GameObject _chav;

    public Action ActiveSound { get; private set; }

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _chav = GameObject.FindGameObjectWithTag("Chav");
        var spriteRenderer = _chav.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = _chav.GetComponent<Chavs>()._loader.spriteDic["Chav_attack"];
        ActiveSound = delegate { };
        //_chav.GetComponent<Chavs>().Shot();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{


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
