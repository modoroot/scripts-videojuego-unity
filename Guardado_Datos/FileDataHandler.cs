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

    public async Task SaveUnityCloudService(GameData _data) {
        await UnityServices.InitializeAsync();
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
        var data = new Dictionary<string, object> { { "key", _data } };
        await CloudSaveService.Instance.Data.ForceSaveAsync(data);
    }

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


    public void DeleteData() {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        if (File.Exists(fullPath))
            File.Delete(fullPath);
    }

    private string EncryptDecrypt(string _data) {
        string modifiedData = "";
        for (int i = 0; i < _data.Length; i++) {
            modifiedData += (char)(_data[i] ^ codeWord[i % codeWord.Length]);
        }
        return modifiedData;
    }
}
