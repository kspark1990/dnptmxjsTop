using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour {

	[SerializeField]
	eTeamType _TeamType;
	public eTeamType TeamType
	{
		get { return _TeamType; }
	}

	protected GunController gunController;
	protected Animator anim;
	public Gun gun;
	public Vector3 targetPos;

	protected IKControl IK;

	protected void Init()
	{
		Debug.Log("Actor Awake()");
		gunController = GetComponent<GunController>();
		anim = GetComponent<Animator>();
		gun = GetComponentInChildren<Gun>();
		IK = GetComponent<IKControl>();
	}




	public virtual Vector3 TargetPos()
	{




		return targetPos;
	} 


}
