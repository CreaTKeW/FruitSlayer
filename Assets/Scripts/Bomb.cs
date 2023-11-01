using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private BladeScript blade;
    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        blade = collision.GetComponent<BladeScript>();
        if(!blade) return;

        gameManager.OnBombCollision();
        gameManager.explosionSound();
    }
}
