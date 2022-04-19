using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthScript : HealthScript
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        PlayerEvents.InvokeOnHealthInitialized(this);
    }

    public override void Destroy()
    {
        GameManager.Instance().GameOverCondition(false);
        base.Destroy();
    }
}
