using System;
using UnityEngine;

public class ChavMoveWithShooting : StateMachineBehaviour
{

    public Action ActiveSound { get; private set; }

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var chav = GameObject.FindGameObjectWithTag("Chav");
        chav.GetComponent<EnemyShootCommand>().Execute();
        chav.GetComponent<CharacterStateAnimation>().move.Play();
        chav.GetComponent<MoveEnemyBehaviour>().Play();
        chav.GetComponent<MoveEnemyBehaviour>().OnStop += ChangeStateToIdle;
    }

    private void ChangeStateToIdle()
    {
        var chav = GameObject.FindGameObjectWithTag("Chav");
        var animator = chav.GetComponent<Animator>();
        animator.SetBool("ReturnToIdleState", true);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var chav = GameObject.FindGameObjectWithTag("Chav");
        chav.GetComponent<CharacterStateAnimation>().attack.Stop();
        chav.GetComponent<EnemyShootCommand>().Stop();
        EnemyFactory.Instance.SetSpawnPointFor("BloodyExplosion", chav.transform.position);
        EnemyFactory.Instance.SetSpawnPointFor("ChavGrave", chav.transform.position);
        chav.GetComponent<CharacterStateAnimation>().move.Stop();
        chav.GetComponent<MoveEnemyBehaviour>().Stop();
        chav.GetComponent<MoveEnemyBehaviour>().OnStop -= ChangeStateToIdle;
        animator.SetBool("Shooting", false);
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
