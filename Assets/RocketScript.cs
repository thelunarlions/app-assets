using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

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

    public Text myText;  // Reference to the Text object (can be set in the Inspector)
    public Transform target;

    public string fileName = "data.json"; // Name of the file in the Resources folder
    public float speed = 1.0f;           // Speed of the rocket movement
    private List<DataPoint> dataPoints;
    public Vector3 customUp = new Vector3(0, 1, 30);
    private int currentIndex = 0;

    // Use Awake or Start to initialize camera
    void Start()
    {
        // Make sure the camera is not null before finding it
        LoadData();
        transform.position = new Vector3(3690, 4220, 3030);
    }

    void Update()
    {
        // Move the rocket only if data points exist and the index is valid
        if (dataPoints != null && currentIndex < dataPoints.Count)
        {
            // Get the target position from the current data point
            DataPoint currentData = dataPoints[currentIndex];
            DataPoint nextData = dataPoints[currentIndex + 1];

            Vector3 targetPosition = new Vector3((float)currentData.Rx, (float)currentData.Ry, (float)currentData.Rz);

            // Instantly move the rocket to the target position
            transform.position = targetPosition;


            // Get the velocity vector (replace with your velocity values)
            Vector3 velocity = new Vector3((float)currentData.Vx, (float)currentData.Vy, (float)currentData.Vz);

            // Check if the velocity is not zero to avoid issues with LookRotation
            if (velocity != Vector3.zero)
            {
                // Calculate the rotation to face the direction of the velocity
                Quaternion rotation = Quaternion.LookRotation(velocity, customUp);

                // Set the object's rotation
                transform.rotation = rotation;
            }

            // Move to the next data point
            currentIndex++;
        }
    }

    void LoadData()
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

public class ChangeText : MonoBehaviour
{
    public Text myText;  // Reference to the Text object (can be set in the Inspector)

    void Start()
    {
        // Example of changing the text content when the game starts
        myText.text = "New Text Content!";
    }

    void Update()
    {
        // Example of dynamically changing text during gameplay (e.g., every frame)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            myText.text = "Space key pressed!";
        }
    }
}