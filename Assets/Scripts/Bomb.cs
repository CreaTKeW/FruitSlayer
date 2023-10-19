using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        BladeScript blade = collision.GetComponent<BladeScript>();
        if(!blade) return;

        FindObjectOfType<GameManager>().OnBombCollision();
    }
}
