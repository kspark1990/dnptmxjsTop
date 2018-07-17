using UnityEngine;

public class testAim : MonoBehaviour {

	public Player player;
	public Transform muzzle;

	Vector3 viewPort;

	private void Awake()
	{
		Cursor.visible = false;
	}

	void WorldToUIPosition()
	{
		//pos vector3 is targetPos with gun's muzzle position.y
		//because of difrence between world space vector and UI's Vector
		Vector3 pos = new Vector3(player.TargetPos().x, muzzle.position.y, player.TargetPos().z);
		viewPort = Camera.main.WorldToScreenPoint(pos);

		this.transform.position = new Vector3(viewPort.x, viewPort.y, 0);
	}


	void FixedUpdate () {
		WorldToUIPosition();
	}
}
