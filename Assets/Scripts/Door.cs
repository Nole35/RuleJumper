using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour
{
    public string requiredKeyColor = "yellow";
    public AudioClip doorOpenSound;
    public GameObject bangEffectPrefab;
    public bool isFinalDoor = false;

    private AudioSource audioSource;
    private bool isOpened = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isOpened || GameManager.instance.IsGameEnded()) return;

        if (collision.collider.CompareTag("Player"))
        {
            if (KeyInventory.instance.HasKey(requiredKeyColor))
            {
                isOpened = true;
                GameManager.instance.AddScore(10);

                // 🔥 Bang efekat odmah pri otvaranju vrata
                if (bangEffectPrefab != null)
                {
                    GameObject bang = Instantiate(bangEffectPrefab, transform.position, Quaternion.identity);
                    Destroy(bang, 1f); // traje 1 sekundu
                }

                if (doorOpenSound != null && audioSource != null)
                {
                    audioSource.Play();
                }

                // Ako su finalna vrata → Victory
                if (isFinalDoor)
                {
                    GameManager.instance.EndGame(true); // konfete će biti tamo
                }

                // Uništi vrata
                Destroy(gameObject, 0.2f);
            }
            else
            {
                GameManager.instance.AddScore(-5);

                // Odbaci igrača ako nema ključ
                Rigidbody2D rb = collision.collider.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.linearVelocity = new Vector2(-5f, 3f);
                }
            }
        }
    }
}

