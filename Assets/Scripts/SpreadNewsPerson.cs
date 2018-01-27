using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadNewsPerson : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowSpreadingNews(Vector3 pos)
    {
        transform.position = pos;
        gameObject.SetActive(true);
        Invoke("Hide", 1.0f);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
