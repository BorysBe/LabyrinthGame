using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GraveAnimation : StateMachineBehaviour
{
    public GameObject chavGrave;
    List<GameObject> animations;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        animator.GetComponent<EnemyLifeCycle>().Stop();
        animations = animator.GetComponent<CharacterStateAnimation>().attachedAnimations;
        GameObject grave = animations.Where(obj => obj.name == "ChavGrave(Clone)").SingleOrDefault();
        grave.transform.position = animator.GetComponent<CharacterStateAnimation>().ReturnPositionOfAnimation();
        grave.GetComponent<LoopAnimation>().Play();
        grave.GetComponent<OffsetAndOpacity>().transitionActivated = true;
        grave.GetComponent<OffsetAndOpacity>().CoroutineTimerReset();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<Animator>().SetBool("isDead", false);
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
