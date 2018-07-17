
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

	Camera viewCamera;


    float rotateAngle = 0f;
    Vector3 rotateVector;
    private void Awake()
    {

		base.Init();


        if (moveType == MovementType.Type1)
            anim.SetInteger("MoveType", 0);
        else if(moveType == MovementType.Type2)
            anim.SetInteger("MoveType", 1);

	
		viewCamera = Camera.main;
		CameraManager.Instance.player = this.gameObject;

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

	Vector3 heightCorrectedPoint;

	public override Vector3 TargetPos()
	{
		// 바라보는 방향

		Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);

		Plane groundPlane = new Plane(Vector3.up, Vector3.up * 1.5f);

		float rayDistance;

		if (groundPlane.Raycast(ray, out rayDistance))
		{
			Vector3 point = ray.GetPoint(rayDistance);
			heightCorrectedPoint = new Vector3(point.x, transform.position.y, point.z);
		}
		targetPos = heightCorrectedPoint;
		
		return base.TargetPos();
	}

    void FixedUpdate()
    {

        if(moveType == MovementType.Type1)
        {
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");
            Vector3 dir = new Vector3(x, 0, y);

			Vector3 moveDir = transform.InverseTransformDirection(dir);


            transform.Rotate(new Vector3(0, rotateAngle * 10f * Time.fixedDeltaTime, 0));
                
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

	}


}

	
