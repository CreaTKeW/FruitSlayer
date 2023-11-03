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
        if (collision.tag == "Blade")
        {
            gameManager.OnBombCollision();
            gameManager.explosionSound();
        }        
    }
}
