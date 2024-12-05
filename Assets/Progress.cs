using Newtonsoft.Json;
using System;  // Add this for Exception
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;  // Add this for Path


public class Progress : MonoBehaviour
{
    public Slider progressBar;
    private int currentIndex = 0;
    private List<DataPoint> dataPoints;  // Ensure you have this variable defined
    private string fileName = "data.json";  // Define the filename variable

    void Start()
    {
        LoadData();
        progressBar.value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(dataPoints[0]);
        if (dataPoints != null && currentIndex < dataPoints.Count)
        {
            DataPoint currentData = dataPoints[currentIndex];

            if (progressBar.value < 12983)
            {
                progressBar.value = (float)(currentData.Time);
            }
            currentIndex++;
        }
    }

    void LoadData()
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
