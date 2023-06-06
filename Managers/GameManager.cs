using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 
/// </summary>
public class GameManager : MonoBehaviour, ISaveManager {

    public static GameManager instance;
    [SerializeField] private Checkpoint[] checkpoints;
    private void Awake() {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;

        checkpoints = FindObjectsOfType<Checkpoint>();
    }

    private void Start() {
        
    }

    public void RestartScene() {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void LoadData(GameData _data) {
        foreach (KeyValuePair<string, bool> pair in _data.checkpoints) {
            foreach (Checkpoint checkpoint in checkpoints) {
                if (checkpoint.id == pair.Key && pair.Value == true) {
                    Debug.Log("se está activando: " + checkpoint.id);
                    checkpoint.ActivateCheckpoint();
                }
            }
        }

        foreach (Checkpoint checkpoint in checkpoints) {
            if (_data.closestCheckpointId == checkpoint.id) {
                PlayerManager.instance.player.transform.position = checkpoint.transform.position;
            }
        }
    }
    /// <summary>
    /// Guarda los datos de los checkpoints y el checkpoint más cercano al jugador.
    /// </summary>
    /// <param name="_data"></param>
    public void SaveData(ref GameData _data) {
        if (FindClosestCheckpoint() != null)
            _data.closestCheckpointId = FindClosestCheckpoint().id;
        else
            _data.closestCheckpointId = null;
        _data.checkpoints.Clear();
        foreach (Checkpoint checkpoint in checkpoints) {
            _data.checkpoints.Add(checkpoint.id, checkpoint.isActivated);
            Debug.Log("GUARDANDO AL HIJO DE LA GRAN PUTA " + checkpoint.id + " COMO " + checkpoint.isActivated);
        }
    }

    /// <summary>
    /// Método que busca el checkpoint más cercano al jugador una vez que este muere o guarda la partida.
    /// </summary>
    /// <returns>checkpoint</returns>
    private Checkpoint FindClosestCheckpoint() {
        float closestDistance = Mathf.Infinity;
        Checkpoint closestCheckpoint = null;
        foreach (Checkpoint checkpoint in checkpoints) {
            float distanceToCheckpoint = Vector2.Distance(PlayerManager.instance.player.transform.position, checkpoint.transform.position);
            if (distanceToCheckpoint < closestDistance && checkpoint.isActivated == true) {
                closestDistance = distanceToCheckpoint;
                closestCheckpoint = checkpoint;
            }
        }
        return closestCheckpoint;
    }
}
