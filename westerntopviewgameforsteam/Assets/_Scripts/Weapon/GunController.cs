using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {

	List<Gun> gunList = new List<Gun>();



    public Transform weaponHold;
    public Gun startingGun;
	public Gun nextGun;
    public Gun equippedGun;

    void Start()
    {
        for(int i= 0; i <(int)eGunType.MAX; i++)
		{
			Gun go = Resources.Load("Prefabs/Weapon/" + ((eGunType)i).ToString("F")) as Gun;

			if (go == null)
			{
				Debug.LogError(((eGunType)i).ToString("F") + "Load Failed.");
				continue;
			}
			else
			{
				gunList.Add(go);
				Debug.Log(((eGunType)i).ToString("F") + " load succese");
			}


		}




	}

	public void EquipGun(Gun gunToEquip)
    {
        if (equippedGun != null)
        {
            Destroy(equippedGun.gameObject);
        }
        equippedGun = Instantiate(gunToEquip, weaponHold.position, weaponHold.rotation)as Gun;
        equippedGun.transform.parent = weaponHold;
    }

    public void Shoot()
    {
        if(equippedGun!=null)
        {
            equippedGun.Shoot();
        }
    }
}
