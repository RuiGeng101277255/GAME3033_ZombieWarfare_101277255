using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponComponentScript : MonoBehaviour
{
    public Transform weaponGripLocation;

    protected WeaponHandleScript weaponHandle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialize(WeaponHandleScript handle)
    {
        weaponHandle = handle;
    }
}
