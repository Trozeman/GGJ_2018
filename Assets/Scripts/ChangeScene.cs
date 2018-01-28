using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Debug = System.Diagnostics.Debug;

public class ChangeScene : MonoBehaviour
{

	public int _ID;

	public void Change(int i)
	{
		if (i < 0)
		{
			Application.Quit();
		}
		else
		{
			SceneManager.LoadScene(i);
		}
	}


	// Use this for initialization


	void Start ()
	{
		
	}

	// Update is called once per frame
	void Update () {
		
	}
}
