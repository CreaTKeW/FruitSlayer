using UnityEngine;

public class Spin : MonoBehaviour
{
    public float rotationSpeed = 30.0f; // Adjust the speed as needed

    void Update()
    {
        // Rotate the object around its up axis (Y-axis)
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
