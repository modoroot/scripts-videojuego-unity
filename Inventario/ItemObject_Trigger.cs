using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject_Trigger : MonoBehaviour {
    private ItemObject MyItemObject => GetComponentInParent<ItemObject>();

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.GetComponent<Player>() != null) {
            if (collision.GetComponent<CharacterStats>().IsDead)
                return;

            Debug.Log("Item recogido");
            MyItemObject.PickupItem();
        }
    }
}
