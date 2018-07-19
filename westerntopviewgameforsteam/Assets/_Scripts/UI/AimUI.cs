using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimUI : MonoBehaviour {


	public Player player;
	public Transform muzzle;

	public float acc;
	float dis;
	Vector3 viewPort;


	RectTransform RT;
	float size;

	Vector3 pos;

	private void Start()
	{
		Cursor.visible = false;
		acc = player.Acc;
		RT = GetComponent<RectTransform>();

	}

	void WorldToUIPosition()
	{
		//pos vector3 is targetPos with gun's muzzle position.y
		//because of difrence between world space vector and UI's Vector
		pos = new Vector3(player.TargetPos().x, muzzle.position.y, player.TargetPos().z);
		viewPort = Camera.main.WorldToScreenPoint(pos);

		this.transform.position = new Vector3(viewPort.x, viewPort.y, 0);
	}

	void SetAimSize()
	{
		dis = Vector3.Distance(pos, muzzle.position);


		size = dis * -Mathf.Tan(acc);


		//Debug.Log("size " + size);
		//Debug.Log("dis " + dis);
		//Debug.Log("tanAcc " + Mathf.Tan(acc));
		//Debug.Log("Acc " + acc);



		RT.localScale = Vector3.one * size;

		if (RT.localScale.x <= 8f)
		{
			RT.localScale = Vector3.one * 8f;
		}

	}


	void FixedUpdate()
	{
		WorldToUIPosition();
		SetAimSize();
	}
}
