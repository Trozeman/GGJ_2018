using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

	public int PlayerHealth;

	public bool enemy;
	
	// Use this for initialization
	void Start ()
	{
		PlayerHealth = 100;
	}
	
	// Update is called once per frame
	void Update () {
		if (enemy = true)
		{
			PlayerHealth -= 1;
		}
	}
}
