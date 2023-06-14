using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.CloudSave;
using System.Threading.Tasks;

/// <summary>
/// Clase para manejar la lectura y escritura de datos en un archivo. 
/// También se encarga de encriptar y desencriptar los datos.
/// </summary>
public class FileDataHandler {
    // Ruta del directorio de datos
    private string dataDirPath = "";
    // Nombre del archivo de datos
    private string dataFileName = "";
    // Indica si los datos se deben encriptar
    private bool encryptData = false;
    // Palabra clave para encriptar los datos
    private string codeWord = "bloodbornepc";


    /// <summary>
    /// Constructor de la clase
    /// </summary>
    /// <param name="_dataDirPath">ruta del directorio</param>
    /// <param name="_dataFileName">nombre del archivo json</param>
    /// <param name="_encryptData">boolean encriptación</param>
    public FileDataHandler(string _dataDirPath, string _dataFileName, bool _encryptData) {
        dataDirPath = _dataDirPath;
        dataFileName = _dataFileName;
        encryptData = _encryptData;
    }

    /// <summary>
    /// Método para guardar los datos en un archivo
    /// </summary>
    /// <param name="_data"></param>
    public void Save(GameData _data) {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        try {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            string dataToStore = JsonUtility.ToJson(_data, true);
            if (encryptData)
                dataToStore = EncryptDecrypt(dataToStore);

            using (FileStream stream = new(fullPath, FileMode.Create)) {
                using (StreamWriter writer = new(stream)) {
                    writer.Write(dataToStore);
                }
            }

        } catch (Exception e) {
            Debug.Log("Error al guardar datos: " + fullPath + "\n" + e);
        }
    }

    /// <summary>
    /// Método que guarda los datos del jugador en la nube
    /// </summary>
    /// <param name="_data">objeto que contiene los datos de juego a guardar</param>
    public async Task SaveUnityCloudService(GameData _data) {
        await UnityServices.InitializeAsync();

        if (!AuthenticationService.Instance.IsSignedIn)
            await AuthenticationService.Instance.SignInAnonymouslyAsync();

        string playerId = AuthenticationService.Instance.PlayerId;
        // Obtiene el número de secuencia único para el nuevo guardado
        int saveNumber = await GetNextSaveNumber(playerId);
        string saveKey = playerId + "_" + saveNumber;
        // Utiliza la clave del guardado para guardar los datos en la nube
        var data = new Dictionary<string, object> { { saveKey, _data } };
        await CloudSaveService.Instance.Data.ForceSaveAsync(data);
    }

    /// <summary>
    /// Método que obtiene el último número de guardado, le suma 1 y lo devuelve
    /// </summary>
    /// <param name="playerId">id del jugador dentro de Unity Cloud</param>
    /// <returns>número de guardado</returns>
    private async Task<int> GetNextSaveNumber(string playerId) {
        // Obtiene los datos guardados en la nube
        var cloudData = await CloudSaveService.Instance.Data.LoadAsync();
        int saveNumber = 1;
        foreach (var item in cloudData) {
            // Verifica si la clave del elemento comienza con el id del jugador
            if (item.Key.StartsWith(playerId + "_")) {
                // Obtiene el número de secuencia del guardado existente
                int itemSaveNumber = int.Parse(item.Key.Split('_')[1]);
                if (itemSaveNumber >= saveNumber) {
                    // Actualiza el número de secuencia si hace falta
                    saveNumber = itemSaveNumber + 1;
                }
            }
        }
        return saveNumber;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public GameData Load() {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        GameData loadData = null;

        if (File.Exists(fullPath)) {
            try {
                string dataToLoad = "";
                using (FileStream stream = new(fullPath, FileMode.Open)) {
                    using (StreamReader reader = new(stream)) {
                        dataToLoad = reader.ReadToEnd();
                    }
                }
                if (encryptData)
                    dataToLoad = EncryptDecrypt(dataToLoad);

                loadData = JsonUtility.FromJson<GameData>(dataToLoad);
            } catch (Exception e) {
                Debug.Log("Error al cargar datos: " + fullPath + "\n" + e);
            }
        }
        return loadData;
    }

    /// <summary>
    /// 
    /// </summary>
    public void DeleteData() {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        if (File.Exists(fullPath))
            File.Delete(fullPath);
    }

    /// <summary>
    /// Encripta y desencripta los datos utilizando una palabra clave
    /// </summary>
    /// <param name="_data">json encriptado/desencriptado</param>
    /// <returns>json encriptado/desencriptado</returns>
    private string EncryptDecrypt(string _data) {
        string modifiedData = "";
        for (int i = 0; i < _data.Length; i++) {
            modifiedData += (char)(_data[i] ^ codeWord[i % codeWord.Length]);
        }
        return modifiedData;
    }
}
