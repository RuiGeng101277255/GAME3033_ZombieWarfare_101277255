using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthScript : HealthScript
{
    public AudioSource playerHurtSFX;
    public AudioSource playerDeathSFX;

    private bool hasPlayerDied = false;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        PlayerEvents.InvokeOnHealthInitialized(this);
    }

    public override void TakeDamage(float damage)
    {
        if (!hasPlayerDied)
        {
            if (!playerHurtSFX.isPlaying)
            {
                playerHurtSFX.Play();
            }
            base.TakeDamage(damage);
        }
    }
    public override void Destroy()
    {
        if (!hasPlayerDied)
        {
            playerDeathSFX.Play();
            hasPlayerDied = true;
            StartCoroutine(delayDeath(1.0f));
        }
    }

    IEnumerator delayDeath(float delay)
    {
        yield return new WaitForSeconds(delay);
        base.Destroy();
        GameManager.Instance().GameOverCondition(false);
    }
}
