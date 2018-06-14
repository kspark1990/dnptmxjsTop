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


}
