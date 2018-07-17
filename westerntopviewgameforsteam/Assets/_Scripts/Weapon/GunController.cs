﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {

//	List<Gun> gunList = new List<Gun>();



    public Transform weaponHold;
    public Gun startingGun;
	public Gun nextGun;
    public Gun equippedGun;
	IKControl IK;


    void Start()
    {
		IK = GetComponent<IKControl>();
		
	}

	public void EquipGun(eGunType type)
    {
		Gun gunToEquip = WeaponManager.Instance.GetGunPrefab(type).GetComponent<Gun>();



		if (equippedGun != null)
        {
            Destroy(equippedGun.gameObject);
        }



		//weaponHold transform have Gun's position and rotation.(not local)
		Transform gunPosRot = weaponHold.GetChild((int)type);

		equippedGun = Instantiate(gunToEquip, gunPosRot.position, gunPosRot.rotation) as Gun;
        equippedGun.transform.parent = this.transform;

		IK.rightHandObj = equippedGun.RightGrabPosition;
		if(type == eGunType.Rifle || type == eGunType.Shotgun)
		{
			IK.leftHandObj = equippedGun.LeftGrabPosition;
		}


    }

    public void Shoot()
    {
        if(equippedGun!=null)
        {
            equippedGun.Shoot();
        }
    }
}