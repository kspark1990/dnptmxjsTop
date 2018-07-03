using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

	public eGunType gunType = eGunType.Revolver;


	public ParticleSystem gunShotSmoke;
	public Transform RightGrabPosition;
    public Transform muzzle;
    public Projectile projectile;


	public float damage = 1;
	public float msBetweenShots = 100;
    public float muzzleVelocity = 35;
	public float Accuarcy = 0;

	Vector3 shootAccuarcy;


    float nextShotTime;


    public void Shoot()
    {
        if (Time.time > nextShotTime)
        {
			nextShotTime = Time.time + msBetweenShots / 1000;

			if (gunType == eGunType.Revolver || gunType == eGunType.Rifle)
			{
				Projectile newProjectile = Instantiate(projectile, muzzle.position, muzzle.rotation) as Projectile;
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
					newProjectile.SetSpeed(muzzleVelocity+GetRandomRange()*10);
					newProjectile.damage = damage;
				}
			}

			gunShotSmoke.Play();



        }

    }



	float GetRandomRange()
	{
		return Random.Range(0 - Accuarcy, 0+Accuarcy);
	}






}
