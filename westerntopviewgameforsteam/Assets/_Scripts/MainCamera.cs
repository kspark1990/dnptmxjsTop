using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour {

	public Actor player;


	private void LateUpdate()
	{
		transform.position = new Vector3(player.transform.position.x, player.transform.position.y+15, player.transform.position.z-7f);
		transform.rotation = Quaternion.Euler(60, 0, 0);
	}


}
