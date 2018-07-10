using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoSingleton<WeaponManager>
{

	//List<GameObject> gunList = new List<GameObject>();
	Dictionary<eGunType, GameObject> DicGunPrefab = new Dictionary<eGunType, GameObject>();


	private void Awake()
	{
		WeaponPrefabsLoad();
	}

	void WeaponPrefabsLoad()
	{
		for (int i = 0; i < (int)eGunType.MAX; i++)
		{
			GameObject go = Resources.Load("Prefabs/Weapon/" + ((eGunType)i).ToString("F")) as GameObject;

			if (go == null)
			{
				Debug.LogError(((eGunType)i).ToString("F") + "Load Failed.");
				continue;
			}
			else
			{
				DicGunPrefab.Add((eGunType)i,go);
				Debug.Log(((eGunType)i).ToString("F") + " load succese");
			}
		}
	}

	public GameObject GetGunPrefab(eGunType type)
	{
		if (DicGunPrefab.ContainsKey(type) == true)
		{
			return DicGunPrefab[type];
		}
		else
		{
			Debug.LogError(type.ToString() + " prefab missing");
			return null;
		}
	}



}
