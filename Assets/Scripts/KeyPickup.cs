//using UnityEngine;
//using System.Collections;

//public class KeyPickup : MonoBehaviour
//{
//    public AudioClip pickupSound;
//    private AudioSource audioSource;

//    void Start()
//    {
//        audioSource = GetComponent<AudioSource>(); // koristi postojeći AudioSource u Inspectoru
//    }

//    void OnTriggerEnter2D(Collider2D other)
//    {
//        if (other.CompareTag("Player"))
//        {
//            if (pickupSound != null && audioSource != null)
//            {
//                StartCoroutine(PlayShortSound(pickupSound, 0.2f));
//            }

//            PlayerInventory inventory = other.GetComponent<PlayerInventory>();
//            if (inventory != null)
//            {
//                inventory.hasKey = true;
//            }

//            Destroy(gameObject, 0.25f); // sačekaj zvuk pa uništi objekat
//        }
//    }

//    private IEnumerator PlayShortSound(AudioClip clip, float duration)
//    {
//        audioSource.clip = clip;
//        audioSource.Play();
//        yield return new WaitForSeconds(duration);
//        audioSource.Stop();
//    }
//}
