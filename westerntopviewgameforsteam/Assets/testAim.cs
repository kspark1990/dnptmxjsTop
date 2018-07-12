using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testAim : MonoBehaviour {

	Player player;
	public GameObject camera;
	float dis;

	private void Awake()
	{
		player = GetComponentInParent<Player>();
	}

	

	void FixedUpdate () {

		//this.transform.LookAt(camera.transform);

		//dis = Vector3.Distance(player.transform.position, player.targetPos);

		this.transform.localPosition = new Vector3(0, 0, 5f);



	}
}
