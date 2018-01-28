using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLoader : MonoBehaviour
{

	public AudioClip[] _Audio;
	private AudioSource s;
	private int r ;
	

	public void AudioPlayRandom()
	{
		r = Random.Range(0, _Audio.Length -1);
		DontDestroyOnLoad(gameObject);

		s = GetComponent<AudioSource>();
		s.clip = _Audio[r];
		s.Play();
	}
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
