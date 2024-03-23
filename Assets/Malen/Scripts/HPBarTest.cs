using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBarTest : MonoBehaviour
{

    public Button button;
    public GaugeManager manager;

    public void OnClicked(float ratio)
    {
        manager.UpdateHPGauge(ratio);
    }
}
