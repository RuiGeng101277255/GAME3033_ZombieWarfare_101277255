using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceShield : MonoBehaviour
{
    public float shieldDuration;

    private bool isShieldActive = false;
    private float currentShieldDuration = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isShieldActive)
        {
            UpdateShieldPosition();
        }
    }

    void UpdateShieldPosition()
    {
        if (currentShieldDuration > 0.0f)
        {
            currentShieldDuration -= Time.deltaTime;
        }
        else
        {
            resetShield();
        }
    }

    void resetShield()
    {
        isShieldActive = false;
        gameObject.SetActive(false);
    }
    public void ActivateShield()
    {
        if (!isShieldActive)
        {
            gameObject.SetActive(true);
            isShieldActive = true;
            currentShieldDuration = shieldDuration;
        }
    }
}
