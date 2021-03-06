﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameFieldManager : MonoBehaviour
{
    public Text NewsCounterText;
    public GameObject WinWindow;
    public Image YellowProgress;
    public Image BlueProgress;
    public Text PerentText;
    public TransmitionEmitterPool EmittersPool;
    public SpreadNewsPerson[] SpreadNewsPersons;
    public SpreadNewsPerson[] SpammedPersons;
    public GameObject Selection;
    public Vector3 Size;
    private GameField _gameField;
    private int _selectedPointIndex;
    public int NewsCounter;

	// Use this for initialization
	void Awake () {

        LeanTween.init(800);
        _gameField = new GameField();
        _gameField.Init(Size);
        _gameField.GenerateField(GameBalanceConst.InitalPointsCount);
        _selectedPointIndex = -1;

        foreach(var person in SpreadNewsPersons)
        {
            person.gameObject.SetActive(false);
        }

        foreach (var person in SpammedPersons)
        {
            person.gameObject.SetActive(false);
        }
        _selectedPointIndex = Random.Range(0, _gameField.Points.Count);
        NewsCounter = 10;
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
#if UNITY_STANDALONE || UNITY_WEBGL || UNITY_EDITOR
        isOverUI = EventSystem.current.IsPointerOverGameObject();
#else

        if (Input.touchCount > 0)
        {
            isOverUI = EventSystem.current.IsPointerOverGameObject(0);
        }
#endif
        if (Input.GetMouseButtonDown(0) && !isOverUI)
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

        float percent = _gameField.Update(Time.deltaTime, EmittersPool, SpammedPersons);
        YellowProgress.fillAmount = percent;
        BlueProgress.fillAmount = 1.0f - percent;

        if (percent >= 0.9f)
        {
            Time.timeScale = 0.0f;
            WinWindow.gameObject.SetActive(true);
        }

        PerentText.text = (percent * 100) + "%";
        NewsCounterText.text = NewsCounter.ToString();
	}

    public GameField GetGameField() {
        return _gameField;
    }

    public void OnTramsitButtonClicked()
    {
        if (_selectedPointIndex >= 0 && NewsCounter > 0)
        {
            Station station = _gameField.Points[_selectedPointIndex];
            station.Increase(1, GameBalanceConst.Intensity, 0.0f);
            SpreadNewsPerson person = SpreadNewsPersons[Random.Range(0, SpreadNewsPersons.Length)];
            person.ShowSpreadingNews(station.GetPosition());
            NewsCounter--;
        }
    }
}
