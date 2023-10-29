using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    GameManager gameManager;
    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        BladeScript blade = collision.GetComponent<BladeScript>();
        if(!blade) return;

        gameManager.OnBombCollision();
        gameManager.explosionSound();
    }
}
