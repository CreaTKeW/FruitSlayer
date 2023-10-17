using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeScript : MonoBehaviour
{
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        BladeAtMouse();
    }

    private void BladeAtMouse()
    {
        var mousePosition = Input.mousePosition;
        mousePosition.z = 10; // distanced 10 units from z default value
        rb.position = Camera.main.ScreenToWorldPoint(mousePosition);
    }
}
