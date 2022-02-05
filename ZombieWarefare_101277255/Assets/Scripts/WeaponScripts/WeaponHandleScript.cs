using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandleScript : MonoBehaviour
{
    [Header("Weapon To Hold"), SerializeField]
    GameObject weaponToSpawn;

    PlayerController playerController;
    Animator playerWeaponAnimator;
    Sprite aimCrossSprite;
    WeaponComponentScript equippedWeapon;

    [SerializeField]
    GameObject weaponSocket;
    [SerializeField]
    Transform gripSocketLocationIK;

    // Start is called before the first frame update
    void Start()
    {
        playerWeaponAnimator = GetComponent<Animator>();
        GameObject spawnedWeapon = Instantiate(weaponToSpawn, weaponSocket.transform.position, weaponSocket.transform.rotation, weaponSocket.transform);
        equippedWeapon = spawnedWeapon.GetComponent<WeaponComponentScript>();
        equippedWeapon.Initialize(this);
        gripSocketLocationIK = equippedWeapon.weaponGripLocation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnAnimatorIK(int layerIndex)
    {
        playerWeaponAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
        playerWeaponAnimator.SetIKPosition(AvatarIKGoal.LeftHand, gripSocketLocationIK.transform.position);
    }
}
