using UnityEngine;

public class BladeScript : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    private float minVelocity = .1f;
    
    private Rigidbody2D rb;
    private Vector3 lastMousePosition;
    private Collider2D collision;

    void Awake()
    {
        audioSource = GetComponentInChildren<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        collision = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        collision.enabled = IsMouseMoving();
        BladeAtMouse();
    }

    private void BladeAtMouse()
    {
        var mousePosition = Input.mousePosition;
        mousePosition.z = 10; // distanced 10 units from z default value
        rb.position = Camera.main.ScreenToWorldPoint(mousePosition);
    }

    private bool IsMouseMoving()
    {
        Vector3 currentMousePosition = transform.position;
        float travel = (lastMousePosition - currentMousePosition).magnitude;
        lastMousePosition = currentMousePosition;

        if (travel > 1f && !audioSource.isPlaying) audioSource.Play();

        if (travel > minVelocity)
        { return true;} else return false;

    }
}
