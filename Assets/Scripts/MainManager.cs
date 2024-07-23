using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance
    { get; private set; }
    public Color TeamColor
    { get; set; }

    private void Awake()
    {
        // If an Instanced MainManager Class already exists and is assigned to the static Instance variable, destroy the currently awakening gameObject and end Awake function.
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else 
        {
            // Allows assignment of the current instanced MainManager Class to the static Instance variable (as long as prior If Statement reads that no pre-existing Instance was assigned).
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        LoadColor();
    }

    [System.Serializable]
    class SaveData
    {
        public Color teamColor;
    }

    public void SaveColor()
    {
        SaveData newData = new SaveData();
        newData.teamColor = TeamColor;

        string json = JsonUtility.ToJson(newData);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadColor()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);

            SaveData newData = JsonUtility.FromJson<SaveData>(json);

            TeamColor = newData.teamColor;
        }
    }
}
