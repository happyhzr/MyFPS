using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeatherData
{
    public Clouds clouds;
}

[System.Serializable]
public class Clouds
{
    public int all;
}
