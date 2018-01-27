using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionAnimation : MonoBehaviour {

	// Use this for initialization
	void Start () {
        DoScale();
	}

    void DoScale()
    {
        float maxRadius = 3.0f;
        transform.localScale = Vector3.one * maxRadius;
        LeanTween.scale(gameObject, Vector3.one / 2.0f, 1.0f)
                 .setOnComplete(() =>
                 {
                                LeanTween.scale(gameObject, Vector3.one * maxRadius, 1.0f)
                              .setOnComplete(() =>
                              {
                                  DoScale();
                              });
                 });
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
