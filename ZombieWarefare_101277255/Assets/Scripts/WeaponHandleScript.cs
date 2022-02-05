using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandleScript : MonoBehaviour
{
    [Header("Weapon To Hold"), SerializeField]
    GameObject weaponToSpawn;

    PlayerController playerController;
    Sprite aimCrossSprite;

    [SerializeField]
    GameObject weaponSocket;

    // Start is called before the first frame update
    void Start()
    {
        GameObject spawnedWeapon = Instantiate(weaponToSpawn, weaponSocket.transform.position, weaponSocket.transform.rotation, weaponSocket.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
