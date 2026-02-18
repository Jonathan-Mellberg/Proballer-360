using System;
using System.IO;
using UnityEngine;

public class SaveToFile : MonoBehaviour
{
    [SerializeField]
    private Save save = new Save();

    private string fileName = "Save.json";

    
    private void SaveGame()
    {
        string json = JsonUtility.ToJson(save);
        string path = Application.persistentDataPath + "/" + fileName;
        File.WriteAllText(path, json);
    }

    private void LoadGame()
    {
        string path = Path.Combine(Application.persistentDataPath, fileName);

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            save = JsonUtility.FromJson<Save>(json);
        }
        else
        {
            save = new Save();
        }
    }

    [Serializable]
    public class Save
    {
        private bool[] levelsDone = new bool[4];
        private int[] levelScores = new int[4];
    }
}
