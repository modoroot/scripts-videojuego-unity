using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public enum EquipmentType {
    Arma,
    Armadura,
    Amuleto,
    Elixir
}

[CreateAssetMenu(fileName = "New Item Data", menuName = "Data/Equipamiento")]
public class ItemData_Equipment : ItemData {
    public EquipmentType equipmentType;

    [Header("Efecto único")]
    public float itemCooldown;
    public ItemEffect[] itemEffects;


    [Header("Estadísticas principales")]
    public int strength;
    public int agility;
    public int intelligence;
    public int vitality;

    [Header("Estadísticas ofensivas")]
    public int damage;
    public int critChance;
    public int critPower;

    [Header("Estadísticas defensivas")]
    public int health;
    public int armor;
    public int evasion;
    public int magicResistance;

    [Header("Estadísticas mágicas")]
    public int fireDamage;
    public int iceDamage;
    public int lightingDamage;


    [Header("Craftear")]
    public List<InventoryItem> craftingMaterials;

    private int descriptionLength;

    public void Effect(Transform _enemyPosition) {
        foreach (var item in itemEffects) {
            item.ExecuteEffect(_enemyPosition);
        }
    }

    public void AddModifiers() {
        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();

        playerStats.strength.AddModifier(strength);
        playerStats.agility.AddModifier(agility);
        playerStats.intelligence.AddModifier(intelligence);
        playerStats.vitality.AddModifier(vitality);

        playerStats.damage.AddModifier(damage);
        playerStats.critChance.AddModifier(critChance);
        playerStats.critPower.AddModifier(critPower);

        playerStats.maxHealth.AddModifier(health);
        playerStats.armor.AddModifier(armor);
        playerStats.evasion.AddModifier(evasion);
        playerStats.magicResistance.AddModifier(magicResistance);

        playerStats.fireDamage.AddModifier(fireDamage);
        playerStats.iceDamage.AddModifier(iceDamage);
        playerStats.lightingDamage.AddModifier(lightingDamage);
    }

    public void RemoveModifiers() {
        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();

        playerStats.strength.RemoveModifier(strength);
        playerStats.agility.RemoveModifier(agility);
        playerStats.intelligence.RemoveModifier(intelligence);
        playerStats.vitality.RemoveModifier(vitality);


        playerStats.damage.RemoveModifier(damage);
        playerStats.critChance.RemoveModifier(critChance);
        playerStats.critPower.RemoveModifier(critPower);


        playerStats.maxHealth.RemoveModifier(health);
        playerStats.armor.RemoveModifier(armor);
        playerStats.evasion.RemoveModifier(evasion);
        playerStats.magicResistance.RemoveModifier(magicResistance);


        playerStats.fireDamage.RemoveModifier(fireDamage);
        playerStats.iceDamage.RemoveModifier(iceDamage);
        playerStats.lightingDamage.RemoveModifier(lightingDamage);
    }

    public override string GetDescription() {
        sb.Length = 0;
        descriptionLength = 0;

        AddItemDescription(strength, "Fuerza");
        AddItemDescription(agility, "Agilidad");
        AddItemDescription(intelligence, "Inteligencia");
        AddItemDescription(vitality, "Vitalidad");

        AddItemDescription(damage, "Daño");
        AddItemDescription(critChance, "Prob. crítico");
        AddItemDescription(critPower, "Daño crítico");

        AddItemDescription(health, "HP");
        AddItemDescription(evasion, "Evasión");
        AddItemDescription(armor, "Armadura");
        AddItemDescription(magicResistance, "Resis. mágica");

        AddItemDescription(fireDamage, "Daño ígneo");
        AddItemDescription(iceDamage, "Daño helado");
        AddItemDescription(lightingDamage, "Daño eléctrico");





        for (int i = 0; i < itemEffects.Length; i++) {
            if (itemEffects[i].effectDescription.Length > 0) {
                sb.AppendLine();
                sb.AppendLine("Único: " + itemEffects[i].effectDescription);
                descriptionLength++;
            }
        }


        if (descriptionLength < 5) {
            for (int i = 0; i < 5 - descriptionLength; i++) {
                sb.AppendLine();
                sb.Append("");
            }
        }



        return sb.ToString();
    }



    private void AddItemDescription(int _value, string _name) {
        if (_value != 0) {
            if (sb.Length > 0)
                sb.AppendLine();

            if (_value > 0)
                sb.Append("+ " + _value + " " + _name);

            descriptionLength++;
        }


    }
}
