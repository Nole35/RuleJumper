using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour
{
    public string requiredKeyColor = "yellow";
    public AudioClip doorOpenSound;
    public bool isFinalDoor = false; // ✓ čekiraj u Inspectoru za poslednja vrata

    private AudioSource audioSource;
    private bool isOpened = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isOpened) return;

        if (collision.collider.CompareTag("Player"))
        {
            if (KeyInventory.instance.HasKey(requiredKeyColor))
            {
                isOpened = true;
                GameManager.instance.AddScore(10); // ✓ poeni za otvaranje

                if (isFinalDoor)
                {
                    GameManager.instance.EndGame(true); // ✓ Victory ekran
                }

                if (doorOpenSound != null && audioSource != null)
                {
                    StartCoroutine(DestroyAfterSound(doorOpenSound, 0.2f));
                }
                else
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                GameManager.instance.AddScore(-5); // ✗ poeni ako nemaš ključ
            }
        }
    }

    private IEnumerator DestroyAfterSound(AudioClip clip, float delay)
    {
        audioSource.clip = clip;
        audioSource.Play();
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
