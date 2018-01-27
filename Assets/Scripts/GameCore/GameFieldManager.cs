using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFieldManager : MonoBehaviour
{
    public Vector3 Size;
    private GameField _gameField;

	// Use this for initialization
	void Awake () {
        _gameField = new GameField();
        _gameField.Init(Size);
        _gameField.GenerateField(GameBalanceConst.InitalPointsCount);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public GameField GetGameField() {
        return _gameField;
    }
}
