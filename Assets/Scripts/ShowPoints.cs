using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPoints : MonoBehaviour
{	
	public GameObject Prefab;

    private GameField _data;
    private List<GameObject> _stations;

    // Use this for initialization
    void Start ()
	{
        _data = GetComponent<GameFieldManager>().GetGameField();
        _stations = new List<GameObject>();

        foreach (var station in _data.Points)
		{
            Vector3 pos = new Vector3(station.x, station.y, 0.0f);
            GameObject stationObj = Instantiate(Prefab, pos, Quaternion.identity, gameObject.transform);
            _stations.Add(stationObj);
            LineRenderer lineRend = stationObj.GetComponent<LineRenderer>();
            List<Vector3> _lines = new List<Vector3>();
            foreach(var stationNaighbour in station.Neighbours)
            {
                _lines.Add(pos);
                _lines.Add(new Vector3(stationNaighbour.x, stationNaighbour.y, 0.0f));
            }
            lineRend.positionCount = _lines.Count;
            lineRend.SetPositions(_lines.ToArray());
		}
    }
	
	// Update is called once per frame
	void Update ()
    {
        //foreach(var station in _stations)
        //{
        //    station.transform.position = _data.
        //}
	}
}
