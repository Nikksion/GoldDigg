using System.IO;
using UnityEngine;
public class SaveMeneger : MonoBehaviour {
    private string SavePath => Path.Combine(Application.persistentDataPath, "GoldDiggSave.json");
    public void SaveGame(SaveData saveData) {
        Debug.Log("Saving data...");
        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(SavePath, json);
        Debug.Log("Data saved!");
    }
    public SaveData LoadGame() {
        Debug.Log("Loading data...");
        if (!File.Exists(SavePath)) {
            Debug.LogWarning("File not found!");
            return null;
        }
        string json = File.ReadAllText(SavePath);
        SaveData loadedData = JsonUtility.FromJson<SaveData>(json);
        Debug.Log("Data loaded!");
        return loadedData;
    }
    public void DeleteSaveFile() {
        if (File.Exists(SavePath)) {
            File.Delete(SavePath);
            Debug.Log("Save file deleted.");
        }
        else
            Debug.LogWarning("File not found!");
    }
}
