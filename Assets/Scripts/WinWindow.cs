using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinWindow : MonoBehaviour {

	public void OnMenuButtonPressed()
    {
        SceneManager.LoadScene(0);
    }
}
