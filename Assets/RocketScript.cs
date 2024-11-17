using System;
using System.IO;
using System.Text.Json;
using UnityEngine;

public class JsonReader : MonoBehaviour
{
    public string jsonFilePath = "SpreadSheetData.json"; // Path to your JSON file

    void Start()
    {
        ReadJson();
    }

    void ReadJson()
    {
        // Load the JSON file and parse it
        if (File.Exists(jsonFilePath))
        {
            string json = File.ReadAllText(jsonFilePath);

            // Deserialize the JSON into the PlayerData object
            PlayerData playerData = JsonSerializer.Deserialize<PlayerData>(json);

            // Use the data (e.g., display it in the console)
            Debug.Log($"Player Name: {playerData.playerName}, Score: {playerData.score}");
            Debug.Log("Inventory: " + string.Join(", ", playerData.inventory));
        }
        else
        {
            Debug.LogError("JSON file not found!");
        }
    }
}