using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Clase que define y configura los puntos de control del juego.
/// </summary>
public class Checkpoint : MonoBehaviour {
    private Animator anim;
    public string id;
    public bool isActivated;

    private void Awake() {
        anim = GetComponent<Animator>();
    }

    private void Start() {
        
    }
    /// <summary>
    /// Genera un ID al checkpoint
    /// </summary>
    [ContextMenu("Generar ID checkpoint")]
    private void GenerateId() {
        id = System.Guid.NewGuid().ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.GetComponent<Player>() != null) {
            ActivateCheckpoint();
        }
    }

    public void ActivateCheckpoint() {
        isActivated = true;
        anim.SetBool("active", true);
    }
}
