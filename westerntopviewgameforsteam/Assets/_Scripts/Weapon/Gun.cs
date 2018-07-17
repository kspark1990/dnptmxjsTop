using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

	public eGunType gunType = eGunType.Revolver;


	public ParticleSystem gunShotSmoke;
	public Transform RightGrabPosition;
	public Transform LeftGrabPosition;

	public Transform muzzle;
    public Projectile projectile;


	public float damage = 1;
	public float msBetweenShots = 100;
    public float muzzleVelocity = 35;
	public float Accuarcy = 2;

	Vector3 shootAccuarcy;


    float nextShotTime;

	public void Shoot()
    {
        if (Time.time > nextShotTime)
        {
			nextShotTime = Time.time + msBetweenShots / 1000;

			if (gunType == eGunType.Revolver || gunType == eGunType.Rifle)
			{
				//Projectile newProjectile = Instantiate(projectile, muzzle.position, muzzle.rotation) as Projectile;
				Projectile newProjectile = Instantiate(projectile, muzzle.position,muzzle.rotation ) as Projectile;

				newProjectile.transform.Rotate(newProjectile.transform.up, GetRandomRange());
				newProjectile.SetSpeed(muzzleVelocity);
				newProjectile.damage = damage;
				//gunShotSmoke.Play();
			}
			else if(gunType == eGunType.Shotgun)
			{
				for(int i = 0; i < 6; i++)
				{
					
					Projectile newProjectile = Instantiate(projectile, muzzle.position, muzzle.rotation) as Projectile;
					newProjectile.transform.Rotate(newProjectile.transform.up, GetRandomRange());
					newProjectile.SetSpeed(muzzleVelocity+GetRandomRange());
					newProjectile.damage = damage;
					gunShotSmoke.Play();
					gunShotSmoke.Play();
				}
			}

			gunShotSmoke.Play();

        }

    }



	float GetRandomRange()
	{
		return Random.Range(0 - Accuarcy, 0+Accuarcy);
	}


	//test
	Actor actor;
	Vector3 aimPos;
	

	float rotateAngle = 0;

	private void Start()
	{
		actor = GetComponentInParent<Actor>();
	}

	float mindist = 3f;


	private void Update()
	{

		aimPos = new Vector3(actor.targetPos.x, muzzle.position.y, actor.targetPos.z);

		Quaternion rot = Quaternion.LookRotation(actor.targetPos);

		//fixedAimPos = Vector3.Lerp(transform.forward, aimPos, lerpAim*Time.deltaTime);

		if (Vector3.Distance(actor.transform.position, actor.targetPos) >= mindist)
			transform.LookAt(aimPos);

	}



}
