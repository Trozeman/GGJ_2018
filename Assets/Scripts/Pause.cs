using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
	private bool _Pause = false;

	public void TogglePause()
	{
		if (_Pause)
		{
			Time.timeScale = 1;
			_Pause = !_Pause;
		}
		else
		{
			Time.timeScale = 0;
			_Pause = !_Pause;
		}
	}
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space))
		{
			TogglePause();
		}
	}
}
