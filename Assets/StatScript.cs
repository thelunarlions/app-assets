using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatScript : MonoBehaviour
{
    public string fileName = "data.json";
    private List<DataPoint> dataPoints;
    private int currentIndex = 0;
    [SerializeField]
    private TMP_Text _title;
    [SerializeField]
    private TMP_Text _status;
    [SerializeField]
    private TMP_Text _antennas;

    void Start()
    {
        SLoadData();
    }

    void Update()
    {
        if (dataPoints != null && currentIndex < dataPoints.Count)
        {
            DataPoint currentData = dataPoints[currentIndex];
            _title.text = $"Mission Time: {currentData.Time}\nRocket Mass: {currentData.M}\nRocket X: {currentData.Rx}\nRocket Y: {currentData.Ry}\nRocket Z: {currentData.Rz}";
            _antennas.text = "";
            if (currentData.Wpsa == 1)
            {
                _antennas.text += $"DS54 Antenna detected: ${currentData.WpsaBudget}\n";
            }
            if (currentData.Ds54 == 1)
            {
                _antennas.text += $"DS54 Antenna detected: ${currentData.Ds54Budget}\n";
            }
            if (currentData.Ds24 == 1)
            {
                _antennas.text += $"DS24 Antenna detected: ${currentData.Ds24Budget}\n";
            }
            if (currentData.Ds34 == 1)
            {
                _antennas.text += $"DS34 Antenna detected: ${currentData.Ds34Budget}\n";
            }
            if (currentData.Wpsa == 0 && currentData.Ds54 == 0 && currentData.Ds24 == 0 && currentData.Ds34 == 0)
            {

                _antennas.text = "No antennas detected";
            }


            if (currentData.Time < 1825)
            {
                _status.text = "Mission Progress: Orbiting Earth";
            }
            else if (currentData.Time < 7305)
            {
                _status.text = "Mission Progress: On the way to the moon";
            } else if (currentData.Time < 12784){
                _status.text = "Mission Progress: Returning to Earth";
            } else {
                _status.text = "Mission Progress: Entry, descent, and landing (EDL)";
            }
            currentIndex++;
        }
    }

    void SLoadData()
    {
        try
        {
            TextAsset jsonFile = Resources.Load<TextAsset>(Path.GetFileNameWithoutExtension(fileName));
            dataPoints = JsonConvert.DeserializeObject<List<DataPoint>>(jsonFile.text);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error loading data: {ex.Message}");
        }
    }
}
