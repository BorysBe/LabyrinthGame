using UnityEngine;

public class ChavAttack : MonoBehaviour
{
    private CoroutineTimer timer;
    int shot = 0;

    private void OnTriggerEnter(Collider other)
    {
        var chav = GameObject.FindGameObjectWithTag("Chav");
        //chav.GetComponent<Chavs>().SwitchStateTo(Chavs.State.Move);
        Debug.Log("Attack!");
        if (shot > 0)
            return;

        timer = new CoroutineTimer(100, this);
        timer.Tick = TimeOfShootng;
        timer.Start();

    }
    void TimeOfShootng()
    {
        var chav = GameObject.FindGameObjectWithTag("Chav");
        chav.GetComponent<Chavs>().SwitchStateTo(Chavs.State.Shooting);
        shot++;
        if (shot % 2 == 0)
            chav.GetComponent<Chavs>().SwitchStateTo(Chavs.State.Move);
    }
}
