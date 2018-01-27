using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPoints : MonoBehaviour
{	
	public GameObject Prefab;

    private GameField _data;
    private List<StationView> _stations;

    // Use this for initialization
    void Start ()
	{
        _data = GetComponent<GameFieldManager>().GetGameField();
        _stations = new List<StationView>();

        foreach (var station in _data.Points)
		{
            Vector3 pos = new Vector3(station.x, station.y, 0.0f);
            StationView stationObj = Instantiate(Prefab, pos, Quaternion.identity, gameObject.transform).GetComponent<StationView>();
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
        int index = 0;
        foreach(var station in _stations)
        {
            Station stationData = _data.Points[index];
            station.PointSprite.transform.localScale = Vector3.one *  (1.0f + stationData.Radiation * GameBalanceConst.IntensityToRadiusRatio);

            Color color = Color.Lerp(Color.yellow, Color.red, Mathf.Min(1.0f, stationData.Radiation * 0.01f));
            if (stationData.Radiation == 0.0f)
            {
                color = new Color(0, 0.9f, 1.0f);
            }
            station.PointSprite.color = color;
            index++;
        }

	}
}