using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransmitionEmitterPool : MonoBehaviour {

    public GameObject EmitterPrefab;

    public GameObject InstantiateEmitter()
    {
        return Instantiate(EmitterPrefab, Vector3.zero, Quaternion.identity, transform);
    }
}
