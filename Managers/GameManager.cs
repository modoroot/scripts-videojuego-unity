using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 
/// </summary>
public class GameManager : MonoBehaviour, ISaveManager {

    public static GameManager instance;
    private Transform player;
    [SerializeField] private Checkpoint[] checkpoints;

    [Header("Almas perdidas")]
    [SerializeField] private GameObject lostSoulsPrefab;
    public int lostSoulsAmount;
    [SerializeField] private float lostSoulsX;
    [SerializeField] private float lostSoulsY;

    private void Awake() {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;

        checkpoints = FindObjectsOfType<Checkpoint>();
    }

    private void Start() {
        player = PlayerManager.instance.player.transform;
    }

    public void RestartScene() {
        SaveManager.instance.SaveGame();
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void LoadData(GameData _data) => StartCoroutine(LoadDelayed(_data));

    private void LoadCheckpoints(GameData _data) {
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

    private void LoadLostSouls(GameData _data) {
        lostSoulsAmount = _data.lostSoulsAmount;
        lostSoulsX = _data.lostSoulsX;
        lostSoulsY = _data.lostSoulsY;

        if (lostSoulsAmount > 0) {
            GameObject newLostSouls = Instantiate(lostSoulsPrefab, new Vector3(lostSoulsX, lostSoulsY), Quaternion.identity);
            newLostSouls.GetComponent<LostSoulsController>().souls = lostSoulsAmount;
        }
        lostSoulsAmount = 0;
    }

    /// <summary>
    /// Interfaz para asegurarme de que se carguen los datos de los checkpoints y las almas perdidas.
    /// </summary>
    /// <param name="_data"></param>
    /// <returns></returns>
    IEnumerator LoadDelayed(GameData _data) { 
        yield return new WaitForSeconds(.1f);
        LoadLostSouls(_data);
        LoadCheckpoints(_data);
    }

    /// <summary>
    /// Guarda los datos de los checkpoints y el checkpoint más cercano al jugador.
    /// </summary>
    /// <param name="_data"></param>
    public void SaveData(ref GameData _data) {
        _data.lostSoulsAmount = lostSoulsAmount;
        _data.lostSoulsX = player.position.x;
        _data.lostSoulsY = player.position.y;

        if (FindClosestCheckpoint() != null)
            _data.closestCheckpointId = FindClosestCheckpoint().id;
        else
            _data.closestCheckpointId = null;

        _data.checkpoints.Clear();
        foreach (Checkpoint checkpoint in checkpoints) {
            _data.checkpoints.Add(checkpoint.id, checkpoint.isActivated);
            //Debug.Log("GUARDANDO " + checkpoint.id + " COMO " + checkpoint.isActivated);
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
            float distanceToCheckpoint = Vector2.Distance(player.position, checkpoint.transform.position);
            if (distanceToCheckpoint < closestDistance && checkpoint.isActivated == true) {
                closestDistance = distanceToCheckpoint;
                closestCheckpoint = checkpoint;
            }
        }
        return closestCheckpoint;
    }

    public void PauseGame(bool _pause) {
        if (_pause)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;   
    }
}
