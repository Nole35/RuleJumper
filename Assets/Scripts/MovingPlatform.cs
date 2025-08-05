using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float moveDistance = 2f;     // Koliko daleko platforma ide
    public float moveSpeed = 2f;        // Brzina kretanja
    public bool vertical = true;        // Da li se kreće gore-dole (true) ili levo-desno (false)

    private Vector3 startPos;
    private bool movingUp = true;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        Vector3 direction = vertical ? Vector3.up : Vector3.right;
        float displacement = Mathf.PingPong(Time.time * moveSpeed, moveDistance);
        transform.position = startPos + direction * displacement;
    }
}

