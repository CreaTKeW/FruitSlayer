using UnityEngine;

public class Spin : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(0f, 40f * Time.deltaTime, .5f, Space.Self);
    }
}
