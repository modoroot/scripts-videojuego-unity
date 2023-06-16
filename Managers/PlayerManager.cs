using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Clase que se encarga de administrar las almas del jugador.
/// También crea un singleton para acceder a la instancia desde cualquier script.
/// </summary>
public class PlayerManager : MonoBehaviour, ISaveManager {
    public static PlayerManager instance;
    public Player player;

    public int souls;
    private void Awake() {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
    }
    public bool HaveEnoughSouls(int _price) {
        if (_price > souls) {
            Debug.Log("Te faltan almas");
            return false;
        }

        souls -= _price;
        return true;
    }

    public int GetSouls() => souls;

    public void LoadData(GameData _data) {
        this.souls = _data.souls;
    }

    public void SaveData(ref GameData _data) {
        _data.souls = this.souls;
    }
}
