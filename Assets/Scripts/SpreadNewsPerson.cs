using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadNewsPerson : MonoBehaviour
{
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
