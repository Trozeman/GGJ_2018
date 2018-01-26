using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPoints : MonoBehaviour
{
	public List<Vector3> SpawnPoints;
	public GameObject Prefab;
	
	// Use this for initialization
	void Start ()
	{
		foreach (var pnt in SpawnPoints)
		{
			Instantiate(Prefab, pnt, Quaternion.identity, gameObject.transform);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
