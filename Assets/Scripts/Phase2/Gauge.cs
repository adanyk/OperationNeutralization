using TMPro;
using UnityEngine;

public class Gauge : MonoBehaviour
{
    public GameObject gaugeDisplay;

    public void DisplayMeasurement(int measurement)
    {
        gaugeDisplay.GetComponent<TextMeshProUGUI>().text = measurement.ToString();
    }
    public void DisplayMeasurement()
    {
        gaugeDisplay.GetComponent<TextMeshProUGUI>().text = "---";
    }
}
