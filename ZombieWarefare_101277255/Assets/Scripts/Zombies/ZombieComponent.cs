using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieComponent : MonoBehaviour
{
    public float zombieDamage = 5;

    public NavMeshAgent zombieNavmeshAgent;
    public Animator zombieAnimator;
    public ZombieStateMachine zombieSM;
    public GameObject followTarget;

    public AudioSource ZombieGrowlSFX;
    public AudioSource ZombieDeathSFX;

    bool hasZombieDied = false;

    private void Awake()
    {
        zombieAnimator = GetComponent<Animator>();
        zombieNavmeshAgent = GetComponent<NavMeshAgent>();
        zombieSM = GetComponent<ZombieStateMachine>();
    }

    private void Start()
    {
        followTarget = GameObject.FindGameObjectWithTag("Player");
        Initialize(followTarget);
    }

    private void Update()
    {
        updateSFXStates();
    }

    void updateSFXStates()
    {
        if (zombieSM.currentState.GetType() == typeof(ZombieFollow))
        {
            if (!ZombieGrowlSFX.isPlaying)
            {
                ZombieGrowlSFX.PlayDelayed(Random.Range(0.0f, 5.0f));
            }
        }
        else if (zombieSM.currentState.GetType() == typeof(ZombieDeath))
        {
            if (!hasZombieDied)
            {
                ZombieDeathSFX.Play();
                hasZombieDied = true;
            }
        }
    }

    public void Initialize(GameObject _followTarget)
    {
        followTarget = _followTarget;

        ZombieIdle idlestate = new ZombieIdle(this, zombieSM);
        zombieSM.AddState(ZombieStateType.IDLE, idlestate);

        ZombieFollow followState = new ZombieFollow(followTarget, this, zombieSM);
        zombieSM.AddState(ZombieStateType.FOLLOW, followState);

        ZombieAttack attackState = new ZombieAttack(followTarget, this, zombieSM);
        zombieSM.AddState(ZombieStateType.ATTACK, attackState);

        ZombieDeath deadState = new ZombieDeath(this, zombieSM);
        zombieSM.AddState(ZombieStateType.DEAD, deadState);

        zombieSM.Initialize(ZombieStateType.FOLLOW);
    }
}
