using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{

	public GameObject Aim;
	GameObject canvas;
	public Player player;


	private void Awake()
	{
		canvas = GameObject.FindGameObjectWithTag("canvas");


		AimPrefabLoad();
	}


	public void AimPrefabLoad()
	{
		GameObject go = Resources.Load("Prefabs/UI/Aim") as GameObject;

		if (go == null)
			Debug.LogError("AIM loading fail");
		else
			Debug.Log("AIM loading success");

		Aim = Instantiate(go);
		Aim.SetActive(false);

		Aim.transform.SetParent(canvas.transform);

		AimUI aimUI= Aim.GetComponent<AimUI>();
		aimUI.player = player;
		Debug.Log( player);
		
	}

	public void OnAim()
	{
		Aim.SetActive(true);
	}
	public void OffAim()
	{
		Aim.SetActive(false);
	}


	




}
