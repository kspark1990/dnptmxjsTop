using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorManager : MonoSingleton<ActorManager>
{
	// 하이라키 관리용
	Transform ActorTransRoot = null;

	// 모든 엑터 관리
	Dictionary<eTeamType, List<Actor>> DicActor = new Dictionary<eTeamType, List<Actor>>();

	// 에너미 프리팹 관리
	Dictionary<eEnemyType, GameObject> DicEnemyPrefab = new Dictionary<eEnemyType, GameObject>();

	private void Awake() 
	{
		
	}


	void EnemyPrefabLoad()
	{
		for (int i = 0; i < (int)eEnemyType.MAX; i++)
		{
			GameObject go = Resources.Load("Prefabs/Actor/" + ((eEnemyType)i).ToString("F")) as GameObject;

			if (go == null)
			{
				Debug.LogError(((eEnemyType)i).ToString("F") + "Load Failed.");
				continue;
			}
			else
			{
				DicEnemyPrefab.Add((eEnemyType)i, go);
			}

		}
	}

	public GameObject GetEnemyPrefab(eEnemyType type)
	{
		if (DicEnemyPrefab.ContainsKey(type) == true)
		{
			return DicEnemyPrefab[type];
		}
		else
		{
			Debug.LogError(type.ToString() +" 타입의 적 프리팹이 없습니다.");
			return null;
		}
	}




	public Actor PlayerLoad(Vector3 pos)
	{
		// 플레이어 프리펩 로드
		GameObject playerPrefab = Resources.Load("Prefabs/Actor/" + "Player") as GameObject;

		// 플레이어 생성
		GameObject go = Instantiate(playerPrefab, pos, Quaternion.identity);


		return go.GetComponent<Actor>();
	}

	public Actor InstantiateOnce(GameObject prefab, Vector3 pos)
	{
		if (prefab == null)
		{
			Debug.LogError("프리팹이 null 입니다." +	" [ ActorManager.InstantiateOnce()]");
			return null;
		}

		GameObject go = Instantiate(prefab, pos, Quaternion.identity);

		if (ActorTransRoot == null)
		{
			GameObject temp = new GameObject("ActorRoot");
			ActorTransRoot = temp.transform;
		}

		go.transform.SetParent(ActorTransRoot);
		return go.GetComponent<Actor>();

	}


	public void AddActor(Actor actor)
	{
		List<Actor> listActor = null;
		eTeamType teamType = actor.TeamType;

		if (DicActor.ContainsKey(teamType) == false)
		{
			listActor = new List<Actor>();
			DicActor.Add(teamType, listActor);
		}
		else
		{
			// listActor = DicActor[teamType];
			DicActor.TryGetValue(teamType, out listActor);
		}

		listActor.Add(actor);

	}

	public void RemoveActor(Actor actor, bool bDelete = false)
	{
		eTeamType teamType = actor.TeamType;

		if (DicActor.ContainsKey(teamType) == true)
		{
			List<Actor> listActor = null;
			DicActor.TryGetValue(teamType, out listActor);
			listActor.Remove(actor);
		}
		else
		{
			Debug.LogError("존재 하지 않는 엑터를 삭제하려고 합니다.");
		}

		if (bDelete)
			Destroy(actor.gameObject);


	}





}
