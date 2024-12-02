using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class StatScript : MonoBehaviour
{
    public Transform target;

    public string fileName = "data.json"; // Name of the file in the Resources folder
    private List<DataPoint> dataPoints;
    private int currentIndex = 0;
    [SerializeField]
    private TMP_Text _title;

    void Start()
    {
        SLoadData();
    }


    void Update()
    {
        // Move the object only if data points exist and the index is valid
        if (dataPoints != null && currentIndex < dataPoints.Count)
        {
            // Get the target position from the current data point
            DataPoint currentData = dataPoints[currentIndex];


            _title.text = $"Mission Time: {currentData.Time}\nRocket X: {currentData.Rx}\nRocket Y: {currentData.Ry}\nRocket Z: {currentData.Rz}";  // Change the text





            // Move to the next data point
            currentIndex++;
        }
    }



    void SLoadData()
    {
        try
        {
            // Load JSON from the Resources folder
            TextAsset jsonFile = Resources.Load<TextAsset>(Path.GetFileNameWithoutExtension(fileName));
            if (jsonFile == null)
            {
                Debug.LogError($"File {fileName} not found in Resources.");
                return;
            }

            // Deserialize JSON array into a list of DataPoint objects
            dataPoints = JsonConvert.DeserializeObject<List<DataPoint>>(jsonFile.text);

            if (dataPoints == null || dataPoints.Count == 0)
            {
                Debug.LogWarning("No data points found in JSON.");
            }
            else
            {
                Debug.Log($"Loaded {dataPoints.Count} data points.");
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error loading data: {ex.Message}");
        }
    }
}
