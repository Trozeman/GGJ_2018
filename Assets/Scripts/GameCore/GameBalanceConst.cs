using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBalanceConst
{
    public static int MaximumTransitions = 400;
    public static bool Asynchronous = false;
    // game parameters
    public static float Intensity = 10.0f;
    public static float IntensityToRadiusRatio = 0.05f;
    //public static int SpreadDepth = 1;
    public static int SpreadSize = 2;
    public static float RadiationAbsorption = 2;
    public static float GlobalCensorAbsorption = 3.7f;
    public static int InitalPointsCount = 50;
    public static float AutoTransmitButtonInterval = 0.88f;
    public static float MaxIntensityToDropDown = 150;
    public static float TransmissionsTime = AutoTransmitButtonInterval;
}
