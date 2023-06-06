using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  Clase que se utiliza para almacenar pares clave-valor 
///  y se puede serializar para guardar y cargar datos
/// </summary>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TValue"></typeparam>
[System.Serializable]
public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver {
    // Variables privadas serializadas
    [SerializeField] private List<TKey> keys = new List<TKey>();
    [SerializeField] private List<TValue> values = new List<TValue>();

    /// <summary>
    /// Método que se ejecuta antes de la serialización
    /// </summary>    
    public void OnBeforeSerialize() {
        // Limpia las listas de claves y valores
        keys.Clear();
        values.Clear();

        // Recorre el diccionario y añadir las claves y valores a las listas correspondientes
        foreach (KeyValuePair<TKey, TValue> pair in this) {
            keys.Add(pair.Key);
            values.Add(pair.Value);
        }
    }

    /// <summary>
    ///  Método que se ejecuta después de la deserialización
    /// </summary>
    public void OnAfterDeserialize() {
        this.Clear();

        if (keys.Count != values.Count)
            throw new System.Exception(string.Format("El número de claves ({0}) no coincide con el número de valores ({1}).", keys.Count, values.Count));

        for (int i = 0; i < keys.Count; i++) {
            this.Add(keys[i], values[i]);
        }
    }
}