using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieDeath : ZombieStates
{
    int movementHash = Animator.StringToHash("Movement");
    int isDeadHash = Animator.StringToHash("isDead");
    bool deathCounted = false;
    public ZombieDeath(ZombieComponent zombie, ZombieStateMachine zombieSM) : base(zombie, zombieSM)
    {

    }
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        ownerZombie.zombieNavmeshAgent.isStopped = true;
        ownerZombie.zombieNavmeshAgent.ResetPath();

        ownerZombie.zombieAnimator.SetFloat(movementHash, 0);
        ownerZombie.zombieAnimator.SetBool(isDeadHash, true);

        if (!deathCounted)
        {
            GameManager.Instance().addToTotalZombiesKilled();
            deathCounted = true;
        }
    }

    public override void Exit()
    {
        base.Exit();
        ownerZombie.zombieNavmeshAgent.isStopped = false;
        ownerZombie.zombieAnimator.SetBool(isDeadHash, false);
    }
}
