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
    /// M�todo que se ejecuta antes de la serializaci�n
    /// </summary>    
    public void OnBeforeSerialize() {
        // Limpia las listas de claves y valores
        keys.Clear();
        values.Clear();

        // Recorre el diccionario y a�adir las claves y valores a las listas correspondientes
        foreach (KeyValuePair<TKey, TValue> pair in this) {
            keys.Add(pair.Key);
            values.Add(pair.Value);
        }
    }

    /// <summary>
    ///  M�todo que se ejecuta despu�s de la deserializaci�n
    /// </summary>
    public void OnAfterDeserialize() {
        this.Clear();

        if (keys.Count != values.Count)
            throw new System.Exception(string.Format("El n�mero de claves ({0}) no coincide con el n�mero de valores ({1}).", keys.Count, values.Count));

        for (int i = 0; i < keys.Count; i++) {
            this.Add(keys[i], values[i]);
        }
    }
}