using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour
{
    public string requiredKeyColor = "yellow";
    public AudioClip doorOpenSound;

    private AudioSource audioSource;
    private bool isOpened = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isOpened) return;

        if (collision.collider.CompareTag("Player") && KeyInventory.instance.HasKey(requiredKeyColor))
        {
            isOpened = true;

            if (doorOpenSound != null && audioSource != null)
            {
                StartCoroutine(DestroyAfterSound(doorOpenSound, 0.2f));
            }
            else
            {
                Destroy(gameObject);
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
