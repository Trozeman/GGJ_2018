using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    public GameFieldManager GameManager;
    public GameObject HelpWindow;
    public Text PercentText;
    public Image TransmitProgress;
    public Button TransmitButton;
    public Toggle AutoTransmit;
    private float _counter;

    public void OnToggleChanged()
    {
        if (AutoTransmit.isOn)
        {
            
        }
    }

    void Update()
    {
        TransmitProgress.fillAmount += Time.deltaTime * GameBalanceConst.AutoTransmitButtonInterval;
        if (TransmitProgress.fillAmount >= 1.0f)
        {
            if (GameManager.NewsCounter < 100)
            {
                GameManager.NewsCounter++;
            }
            TransmitProgress.fillAmount = 0.0f;
            //press button
            if (AutoTransmit.isOn)
            {
                TransmitButton.OnSubmit(new UnityEngine.EventSystems.BaseEventData(UnityEngine.EventSystems.EventSystem.current));
            }
        }


    }

    public void OnHelpButtonPressed()
    {
        Time.timeScale = 0;
        HelpWindow.gameObject.SetActive(true);
    }
}
