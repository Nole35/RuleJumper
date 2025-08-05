using UnityEngine;
using System.Collections;

public class Key : MonoBehaviour
{
    public string keyColor; // npr. "red", "green", "blue"
    public AudioClip pickupSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            KeyInventory.instance.AddKey(keyColor);
            GameManager.instance.AddScore(5); // ➤ Dodaj 5 poena za ključ

            if (pickupSound != null && audioSource != null)
            {
                StartCoroutine(PlayShortSound(pickupSound, 0.2f));
            }

            Destroy(gameObject, 0.25f); // sačekaj da se čuje zvuk
        }
    }

    private IEnumerator PlayShortSound(AudioClip clip, float duration)
    {
        audioSource.clip = clip;
        audioSource.Play();
        yield return new WaitForSeconds(duration);
        audioSource.Stop();
    }
}
