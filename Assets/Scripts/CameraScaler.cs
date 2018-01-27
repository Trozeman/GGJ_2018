using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScaler : MonoBehaviour {

	// Use this for initialization
	void Start () {

        Camera camera = GetComponent<Camera>();
        float ratio = (float)Screen.width / (float)Screen.height;
        camera.orthographicSize = (-(75.0f / 11.0f) * ratio + 971.0f / 44.0f) + 0.2f;		
	}	
	
}
