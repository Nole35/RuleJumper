using UnityEngine;

public class Door : MonoBehaviour
{
    public string requiredKeyColor;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (KeyInventory.instance != null && KeyInventory.instance.HasKey(requiredKeyColor))
            {
                Debug.Log("Door unlocked with key: " + requiredKeyColor);
                Destroy(gameObject);
            }
            else
            {
                Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.linearVelocity = new Vector2(-rb.linearVelocity.x * 0.5f, rb.linearVelocity.y);
                    Debug.Log("Player does not have the correct key! Door remains closed.");
                }
            }
        }
    }
}
