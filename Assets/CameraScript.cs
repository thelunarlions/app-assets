using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public string fileName = "data.json";
    private List<DataPoint> dataPoints;
    private int currentIndex = 0;

    void Start()
    {
        CLoadData();
        transform.position = new Vector3(3691, 4217, 3036);
    }

    void Update()
    {
        if (dataPoints != null && currentIndex < dataPoints.Count)
        {
            DataPoint currentData = dataPoints[currentIndex];
            Vector3 targetPosition = new Vector3((float)currentData.Rx + 1, (float)currentData.Ry - 3, (float)currentData.Rz + 6);
            transform.position = targetPosition;
            currentIndex++;
        }
    }

    void CLoadData()
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
