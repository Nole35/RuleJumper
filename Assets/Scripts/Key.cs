using UnityEngine;
using System.Collections;

public class Key : MonoBehaviour
{
    public string keyColor; // npr. "red", "green", "blue"
    public AudioClip pickupSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // koristi AudioSource iz Inspectora
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            KeyInventory.instance.AddKey(keyColor);

            if (pickupSound != null && audioSource != null)
            {
                StartCoroutine(PlayShortSound(pickupSound, 0.2f));
            }

            Destroy(gameObject, 0.25f); // sačekaj da se zvuk završi
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
