using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Kretanje platforme")]
    public float moveDistance = 2f;     // Koliko daleko platforma ide
    public float moveSpeed = 2f;        // Brzina kretanja
    public bool vertical = true;        // Da li se kreće gore-dole (true) ili levo-desno (false)

    [Header("Nestajanje platforme")]
    public bool disappearsOnTouch = true;     // Da li platforma nestaje kada igrač stane na nju
    public float disappearDelay = 3f;         // Koliko sekundi nakon kontakta da nestane

    private Vector3 startPos;
    private bool isTriggered = false;

    void Start()
    {
        startPos = transform.position;

        // Ako se platforma ne pomera, možeš automatski onemogućiti nestajanje (opcionalno)
        if (moveDistance <= 0.01f)
        {
            disappearsOnTouch = false;
        }
    }

    void Update()
    {
        Vector3 direction = vertical ? Vector3.up : Vector3.right;
        float displacement = Mathf.PingPong(Time.time * moveSpeed, moveDistance);
        transform.position = startPos + direction * displacement;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isTriggered && disappearsOnTouch && collision.collider.CompareTag("Player"))
        {
            isTriggered = true;
            Invoke("Disappear", disappearDelay);
        }
    }

    void Disappear()
    {
        Destroy(gameObject);
    }
}


