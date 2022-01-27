using UnityEngine;


public class ChavDie : StateMachineBehaviour
{
    GameObject chavSpawn;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<CharacterStateAnimation>().SetPositionOfAnimation(animator.transform.position);
        var position = animator.GetComponent<CharacterStateAnimation>().ReturnPositionOfAnimation();
        var bodyFragments = EnemyFactory.Instance.Spawn(PrefabType.ChavCorpseFragments, position, null);
        bodyFragments.Play();
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
