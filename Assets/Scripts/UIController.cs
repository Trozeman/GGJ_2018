using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    public Text PercentText;
    public Image TransmitProgress;
    public Button TransmitButton;
    public Toggle AutoTransmit;	

    public void OnToggleChanged()
    {
        if (AutoTransmit.isOn)
        {
            
        }
    }

    void Update()
    {
        if (AutoTransmit.isOn)
        {
            TransmitProgress.fillAmount += Time.deltaTime * GameBalanceConst.AutoTransmitButtonInterval;
            if (TransmitProgress.fillAmount >= 1.0f)
            {
                TransmitProgress.fillAmount = 0.0f;
                //press button
                TransmitButton.OnSubmit(new UnityEngine.EventSystems.BaseEventData(UnityEngine.EventSystems.EventSystem.current));
            }
        }
    }
}
