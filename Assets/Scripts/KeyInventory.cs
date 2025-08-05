using System.Collections.Generic;
using UnityEngine;

public class KeyInventory : MonoBehaviour
{
    public static KeyInventory instance;

    private HashSet<string> keys = new HashSet<string>();

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void AddKey(string keyColor)
    {
        keys.Add(keyColor);
        Debug.Log("Picked up key: " + keyColor);
    }

    public bool HasKey(string keyColor)
    {
        return keys.Contains(keyColor);
    }
}
