using UnityEngine;

public class BladeScript : MonoBehaviour
{
    private AudioSource sliceSound;
    private Camera mainCamera;
    private Collider2D bladeCollider;
    private TrailRenderer trailRenderer;
    private bool slicing;

    public Vector3 direction {  get; private set; }
    public float minSliceVelocity = 0.01f;

    private void Awake()
    {
        mainCamera = Camera.main;
        sliceSound = GetComponentInChildren<AudioSource>();
        trailRenderer = GetComponent<TrailRenderer>();        
        bladeCollider = GetComponent<Collider2D>();
    }
    private void OnEnable()
    {
        StopSlicing();
    }

    private void OnDisable()
    {
        StopSlicing();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartSlicing();
        } 
        else if (Input.GetMouseButtonUp(0))
        {
            StopSlicing();
        }
        else if (slicing)
        {
            ContinueSlicing();
        }
    }

    private void StartSlicing()
    {
        var mousePosition = Input.mousePosition;
        mousePosition.z = 10f;
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(mousePosition);

        transform.position = newPosition;

        slicing = true;
        bladeCollider.enabled = true;
        trailRenderer.enabled = true;
        trailRenderer.Clear();
    }

    private void StopSlicing()
    {
        slicing = false;
        bladeCollider.enabled = false;
        trailRenderer.enabled = false;
    }

    private void ContinueSlicing()
    {
        var mousePosition = Input.mousePosition;
        mousePosition.z = 10f;
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(mousePosition);

        direction = newPosition - transform.position;

        float velocity = direction.magnitude / Time.deltaTime;
        bladeCollider.enabled = velocity > minSliceVelocity;

        if (velocity > 1f && !sliceSound.isPlaying)
        {
            sliceSound.Play();
        }

        transform.position = newPosition;
    }
}
