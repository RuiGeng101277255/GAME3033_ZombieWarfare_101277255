using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AK47ComponentScript : WeaponComponentScript
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void FireWeapon()
    {
        Vector3 HitLocation;

        if (weaponStats.bulletsInClip > 0 && !isReloading && !weaponHandle.playerController.isRunning)
        {
            base.FireWeapon();
            Ray screenRay = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2.0f, Screen.height / 2.0f, 0.0f));

            if (Physics.Raycast(screenRay, out RaycastHit hit, weaponStats.fireDistance, weaponStats.weaponHitLayers))
            {
                HitLocation = hit.point;

                Vector3 hitDirection = hit.point - mainCamera.transform.position;

                Debug.DrawRay(mainCamera.transform.position, hitDirection.normalized * weaponStats.fireDistance, Color.red, 1.0f);
            }
            print("Bullet in clip: " + weaponStats.bulletsInClip);
        }
        else if (weaponStats.bulletsInClip <= 0)
        {
            weaponHandle.StartReloading();
        }
    }
}
