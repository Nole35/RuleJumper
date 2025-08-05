using UnityEngine;

public class Key : MonoBehaviour
{
    public string keyColor; // npr. "red", "green", "blue"

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            KeyInventory.instance.AddKey(keyColor);
            Destroy(gameObject);
        }
    }
}

