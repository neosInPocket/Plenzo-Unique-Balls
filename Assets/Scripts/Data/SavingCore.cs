using System.IO;
using UnityEngine;

public class SavingCore : MonoBehaviour
{
    [SerializeField] private bool clearPreferences;
    private static string saveFilePath => Application.persistentDataPath + "/PlayerPreferences.json";
    public static DataTransferObject Data { get; private set; }

    private void Awake()
    {
        if (clearPreferences)
        {
            Data = new DataTransferObject();
            SaveData();
        }
        else
        {
            GetData();
        }
    }

    public static void SaveData()
    {
        if (!File.Exists(saveFilePath))
        {
            CreateNewSaveFile();
        }
        else
        {
            WriteDataFile();
        }
    }

    public static void GetData()
    {
        if (!File.Exists(saveFilePath))
        {
            CreateNewSaveFile();
        }
        else
        {
            string text = File.ReadAllText(saveFilePath);
            Data = JsonUtility.FromJson<DataTransferObject>(text);
        }
    }

    private static void CreateNewSaveFile()
    {
        Data = new DataTransferObject();
        File.WriteAllText(saveFilePath, JsonUtility.ToJson(Data, prettyPrint: true));
    }

    private static void WriteDataFile()
    {
        File.WriteAllText(saveFilePath, JsonUtility.ToJson(Data, prettyPrint: true));
    }
}
