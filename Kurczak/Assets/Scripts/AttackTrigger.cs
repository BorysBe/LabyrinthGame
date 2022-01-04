using UnityEngine;

public class AttackTrigger : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        var chav = GameObject.FindGameObjectWithTag("Chav");
        Animator _animator = this.GetComponentInParent<Animator>();
        _animator.SetBool("Shooting", true);
    }
}
