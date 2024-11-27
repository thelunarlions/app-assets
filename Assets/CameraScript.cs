using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class CameraScript : MonoBehaviour
{
    public Transform target;

    public string fileName = "data.json"; // Name of the file in the Resources folder
    public float speed = 1.0f;           // Speed of the rocket movement
    private List<DataPoint> dataPoints;
    private int currentIndex = 0;

    void Start()
    {
        CLoadData();
        transform.position = new Vector3(3691, 4217, 3036);
    }

    void Update()
    {
        // Move the object only if data points exist and the index is valid
        if (dataPoints != null && currentIndex < dataPoints.Count)
        {
            // Get the target position from the current data point
            DataPoint currentData = dataPoints[currentIndex];
            Vector3 targetPosition = new Vector3((float)currentData.Rx+1, (float)currentData.Ry-3, (float)currentData.Rz+6);

            // Instantly move the rocket to the target position
            // Move to the target position
            transform.position = targetPosition;





            // Move to the next data point
            currentIndex++;
        }
    }


    void CLoadData()
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
