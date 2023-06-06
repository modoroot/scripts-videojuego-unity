using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  Clase de datos que se utiliza para almacenar el estado del juego 
///  y se puede serializar para guardar y cargar datos.
/// </summary>
[System.Serializable]
public class GameData  {
    public int souls;
    public SerializableDictionary<string, bool> skillTree;
    public SerializableDictionary<string, int> inventory;
    public List<string> equipmentId;
    public SerializableDictionary<string, bool> checkpoints;
    public string closestCheckpointId;

    public GameData() {
        this.souls = 20000;
        skillTree = new SerializableDictionary<string, bool>();
        inventory = new SerializableDictionary<string, int>();
        equipmentId = new List<string>();
        closestCheckpointId = string.Empty;
        checkpoints = new SerializableDictionary<string, bool>();
    }
}
