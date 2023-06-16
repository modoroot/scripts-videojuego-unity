using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Clase que controla el comportamiento de las almas perdidas
/// </summary>
public class LostSoulsController : MonoBehaviour {
    public int souls;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.GetComponent<Player>() != null) {
            PlayerManager.instance.souls += souls;
            Destroy(gameObject);
        }
    }
}
