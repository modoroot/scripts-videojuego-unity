using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats {
    private Player player;

    protected override void Start() {
        base.Start();
        player = GetComponent<Player>();
    }

    public override void TakeDamage(int _damage) {
        base.TakeDamage(_damage);
    }

    protected override void Die() {
        base.Die();
        player.Die(); 
        GameManager.instance.lostSoulsAmount = PlayerManager.instance.souls;
        PlayerManager.instance.souls = 0;
        GetComponent<PlayerItemDrop>()?.GenerateDrop();
    }

    protected override void DecreaseHealthBy(int _damage) {
        base.DecreaseHealthBy(_damage);

        ItemData_Equipment currentArmor = Inventory.instance.GetEquipment(EquipmentType.Armadura);

        if (currentArmor != null)
            currentArmor.Effect(player.transform);
    }

    public void CloneDoDamage(CharacterStats _targetStats, float _multiplier) {

        int totalDamage = damage.GetValue() + strength.GetValue();

        if (_multiplier > 0)
            totalDamage = Mathf.RoundToInt(totalDamage * _multiplier);

        if (CanCrit()) {
            totalDamage = CalculateCriticalDamage(totalDamage);
        }

        totalDamage = CheckTargetArmor(_targetStats, totalDamage);
        _targetStats.TakeDamage(totalDamage);

        // borrar si no quiero al final que el primer ataque aplique daño mágico
        DoMagicalDamage(_targetStats);
    }
}
