using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataPoint
{
    public double Time { get; set; }
    public double Rx { get; set; }
    public double Ry { get; set; }
    public double Rz { get; set; }
    public double Vx { get; set; }
    public double Vy { get; set; }
    public double Vz { get; set; }
    public double M { get; set; }
    public double Wpsa { get; set; }
    public double? WpsaBudget { get; set; }
    public double Ds54 { get; set; }
    public string Ds54Budget { get; set; }
    public double Ds24 { get; set; }
    public string Ds24Budget { get; set; }
    public double Ds34 { get; set; }
    public string Ds34Budget { get; set; }
}
public class RocketScript : MonoBehaviour
{
    public string fileName = "data.json";
    private List<DataPoint> dataPoints;
    public Vector3 customUp = new Vector3(0, 1, 30);
    private int currentIndex = 0;

    void Start()
    {
        LoadData();
        transform.position = new Vector3(3690, 4220, 3030);
    }

    void Update()
    {
        if (dataPoints != null && currentIndex < dataPoints.Count)
        {
            DataPoint currentData = dataPoints[currentIndex];
            DataPoint nextData = dataPoints[currentIndex + 1];
            Vector3 targetPosition = new Vector3((float)currentData.Rx, (float)currentData.Ry, (float)currentData.Rz);
            transform.position = targetPosition;
            Vector3 velocity = new Vector3((float)currentData.Vx, (float)currentData.Vy, (float)currentData.Vz);
            if (velocity != Vector3.zero)
            {
                Quaternion rotation = Quaternion.LookRotation(velocity, customUp);
                transform.rotation = rotation;
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
