using UnityEngine;

public class ChartBtnOnOff : MonoBehaviour
{
    [SerializeField] private GameObject chart;

    public void OpenChart()
    {
        chart.SetActive(true);
    }
    public void CloseChart()
    {
        chart.SetActive(false);
    }
}
