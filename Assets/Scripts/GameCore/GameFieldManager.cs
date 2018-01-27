using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameFieldManager : MonoBehaviour
{
    public GameObject Selection;
    public Vector3 Size;
    private GameField _gameField;
    private int _selectedPointIndex;

	// Use this for initialization
	void Awake () {
        _gameField = new GameField();
        _gameField.Init(Size);
        _gameField.GenerateField(GameBalanceConst.InitalPointsCount);
        _selectedPointIndex = -1;
	}

    void Start()
    {
        Selection.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {

        float lastMinPos = 999999.0f;
        Station stationClicked = null;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        bool isOverUI = false;
#if UNITY_STANDALONE
        isOverUI = EventSystem.current.IsPointerOverGameObject();
#endif
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            _selectedPointIndex = -1;
            int curentIndex = 0;
            foreach(var station in _gameField.Points)
            {
                float curDistance = Vector3.Distance(mousePos, station.GetPosition());
                if (curDistance < lastMinPos)
                {
                    stationClicked = station;
                    lastMinPos = curDistance;
                    _selectedPointIndex = curentIndex;
                }
                curentIndex++;
            }
        }

        if (stationClicked != null)
        {
            Selection.transform.position = stationClicked.GetPosition();
            Selection.SetActive(true);
        }

        _gameField.Update(Time.deltaTime);
	}

    public GameField GetGameField() {
        return _gameField;
    }

    public void OnTramsitButtonClicked()
    {
        if (_selectedPointIndex >= 0)
        {
            _gameField.Points[_selectedPointIndex].Increase(1, GameBalanceConst.Intensity);
        }
    }
}
