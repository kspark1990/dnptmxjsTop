
using UnityEngine;


//[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(GunController))]
public class Player : Actor
{

    public enum MovementType
    {
        Type1,Type2
    }


    public MovementType moveType = MovementType.Type1;
    //private Animator anim;

	//[HideInInspector]
    //public Vector3 targetPos;

	//GunController gunController;

	Camera viewCamera;

	//Gun gun;


    float rotateAngle = 0f;
    Vector3 rotateVector;
    private void Awake()
    {

		base.Init();
		//gun = GetComponentInChildren<Gun>();
		
        //anim = GetComponent<Animator>();

        if (moveType == MovementType.Type1)
            anim.SetInteger("MoveType", 0);
        else if(moveType == MovementType.Type2)
            anim.SetInteger("MoveType", 1);

		//gunController = GetComponent<GunController>();

		viewCamera = Camera.main;

		CameraManager.Instance.player = this.gameObject;



		//Cursor.visible = false;
    }

    private float GetrotateAngle()
    {
        Vector3 forwordDir = transform.forward.normalized;
        Vector3 changedDIr = (targetPos - transform.position).normalized;
            
        rotateAngle = Quaternion.FromToRotation(forwordDir, changedDIr).eulerAngles.y ;
        if (rotateAngle > 180f)
            rotateAngle -= 360f;

        return rotateAngle/180f;
    }

	//public void LookAt(Vector3 lookPoint)
	//{
	//	Vector3 heightCorrectedPoint = new Vector3(lookPoint.x, transform.position.y, lookPoint.z);
	//	transform.LookAt(heightCorrectedPoint);
	//}
	Vector3 heightCorrectedPoint;
	//Vector3 point;
	public override Vector3 TargetPos()
	{
		// 바라보는 방향

		Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);

		//Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

		Plane groundPlane = new Plane(Vector3.up, Vector3.up * 1.5f);

		float rayDistance;

		if (groundPlane.Raycast(ray, out rayDistance))
		{
			//point = ray.GetPoint(rayDistance-gun.muzzle.position.y);
			Vector3 point = ray.GetPoint(rayDistance);
			heightCorrectedPoint = new Vector3(point.x, transform.position.y, point.z);
			//Debug.DrawLine(ray.origin, point, Color.red);
			//controller.LookAt(point);


		}


		targetPos = heightCorrectedPoint;
		//targetPos = point;

		return base.TargetPos();
	}



	//public Vector3 SetMousePos()
 //   {
	//	// 바라보는 방향
		
	//	Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);

	//	//Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

	//	Plane groundPlane = new Plane(Vector3.up, Vector3.up * 1.5f);
		
	//	float rayDistance;

	//	if (groundPlane.Raycast(ray, out rayDistance))
	//	{
	//		//point = ray.GetPoint(rayDistance-gun.muzzle.position.y);
	//		Vector3 point = ray.GetPoint(rayDistance);
	//		heightCorrectedPoint = new Vector3(point.x, transform.position.y, point.z);
	//		//Debug.DrawLine(ray.origin, point, Color.red);
	//		//controller.LookAt(point);


	//	}




	//	targetPos = heightCorrectedPoint;
	//	//targetPos = point;

	//	return targetPos;

	//	//targetPos = Input.mousePosition;
	//	//targetPos.z = 10f;
	//	//targetPos = Camera.main.ScreenToWorldPoint(targetPos);
	//	//targetPos.y = 1.7f;
	//	////find 

	//	////if (Vector3.Distance(targetPos,transform.position) >= 5f)


	//	//	// 바라보는 방향
	//	//return targetPos;

	//}

    void FixedUpdate()
    {

        if(moveType == MovementType.Type1)
        {
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");
            Vector3 dir = new Vector3(x, 0, y);

			Vector3 moveDir = transform.InverseTransformDirection(dir);

            //TODO: change this value 
                
            // dir = Quaternion.FromToRotation(dir, transform.forward) * dir;
            transform.Rotate(new Vector3(0, rotateAngle * 10f * Time.fixedDeltaTime, 0));

			

            //Quaternion rot = Quaternion.FromToRotation(dir, transform.forward);
                
            //dir = rot * dir;
            //Debug.Log(dir);
                
            //actual moving is working with root anim
            UpdateAnim(moveDir.x, moveDir.z);
        }
        else if(moveType == MovementType.Type2)
        {
            float y = Input.GetAxis("Vertical");
            float rotation = Input.GetAxis("Horizontal");
            UpdateAnim(rotation,y);
        }



    }
        


    void UpdateAnim(float x, float y)
    {
        if(moveType == MovementType.Type1)
        {
			//SetMousePos();
			TargetPos();
            anim.SetFloat("X", x, 0.1f, Time.deltaTime);
            anim.SetFloat("Y", y, 0.1f, Time.deltaTime);
            anim.SetFloat("rotation", GetrotateAngle());	
        }
        else if (moveType == MovementType.Type2)
        {
            anim.SetFloat("rotation", x);
            anim.SetFloat("Y", y, 0.1f, Time.deltaTime);
        }
    }


	private void Update()
	{
		if (Input.GetMouseButton(0))
		{
			gunController.Shoot();
		}



		if (Input.GetKeyDown(KeyCode.F1))
		{
			gunController.EquipGun(eGunType.Revolver);
			IK.gunType = eGunType.Revolver;
			gun = GetComponentInChildren<Gun>();
		}
		if (Input.GetKeyDown(KeyCode.F2))
		{
			gunController.EquipGun(eGunType.Rifle);
			IK.gunType = eGunType.Rifle;
			gun = GetComponentInChildren<Gun>();
		}
		if (Input.GetKeyDown(KeyCode.F3))
		{
			gunController.EquipGun(eGunType.Shotgun);
			IK.gunType = eGunType.Shotgun;
			gun = GetComponentInChildren<Gun>();
		}



		//test
		/*
		if (isGround == false)
		{
			this.transform.Translate(new Vector3(0, -9.8f * Time.deltaTime, 0));
		}

        */
	}


	//test
    /*
	bool isGround = false;

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.tag == "ground")
		{
			isGround = true;
		}
	}

	private void OnCollisionExit(Collision collision)
	{
		if (collision.collider.tag == "ground")
		{
			isGround = false;
		}
	}

    */


}

	
