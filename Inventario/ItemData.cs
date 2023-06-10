using System.Text;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public enum ItemType {
    Material,
    Equipment
}

[CreateAssetMenu(fileName = "New Item Data", menuName = "Data/Item")]
public class ItemData : ScriptableObject {
    public ItemType itemType;
    public string itemName;
    public Sprite itemIcon;
    public string itemId;

    [Range(0, 100)]
    public float dropChance;

    protected StringBuilder sb = new StringBuilder();

    public virtual string GetDescription() {
        return "";
    }

    /// <summary>
    /// Se ejecuta automáticamente cuando se valida un objeto de script en Unity
    /// </summary>
    private void OnValidate() {
        // Directiva de preprocesador para compilar solo en el editor de Unity
#if UNITY_EDITOR 
        // Obtiene la ruta del archivo del objeto de script
        string path = AssetDatabase.GetAssetPath(this);
        // Convierte la ruta del archivo en un identificador único global (GUID)
        itemId = AssetDatabase.AssetPathToGUID(path);
#endif
    }
}
