using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// 
/// </summary>
public class SaveManager : MonoBehaviour {
    public static SaveManager instance;
    private GameData gameData;
    private FileDataHandler fileDataHandler;
    [SerializeField] private string fileName;
    [SerializeField] private bool encryptData;


    private List<ISaveManager> saveManagers;


    [ContextMenu("Borrar archivo de guardado")]
    public void DeleteSavedData() {
        fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileName, encryptData);
        fileDataHandler.DeleteData();
    }

    private void Awake() {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
    }


    private void Start() {
        fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileName, encryptData);
        saveManagers = FindAllSaveManagers();
        LoadGame();
    }

    public void NewGame() {
        gameData = new GameData();
    }

    public void LoadGame() {
        gameData = fileDataHandler.Load();

        if (this.gameData == null) {
            NewGame();
        }

        foreach (ISaveManager saveManager in saveManagers) {
            saveManager.LoadData(gameData);
        }
    }


    public async void SaveGame() {
        foreach (ISaveManager saveManager in saveManagers) {
            saveManager.SaveData(ref gameData);
        }
        fileDataHandler.Save(gameData);
        await fileDataHandler.SaveUnityCloudService(gameData);
    }

    private void OnApplicationQuit() {
        SaveGame();
    }

    private List<ISaveManager> FindAllSaveManagers() {
        IEnumerable<ISaveManager> saveManagers = FindObjectsOfType<MonoBehaviour>().OfType<ISaveManager>();
        return new List<ISaveManager>(saveManagers);
    }

    public bool HasSaveData() {
        if (fileDataHandler.Load() != null)
            return true;

        return false;
    }
}
