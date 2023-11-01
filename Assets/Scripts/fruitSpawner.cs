using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class fruitSpawner : MonoBehaviour
{
    [Header("GameObjects to spawn")]
    [SerializeField] private GameObject fruit;
    [SerializeField] private GameObject[] fruitsToSpawn;
    [SerializeField] private GameObject bomb;

    [Header("Places from which object spawns")]
    [SerializeField] private Transform[] spawnPlaces;

    [Header("Values that control spawning objects time")]
    [SerializeField] private float minWaitTime = .3f;
    [SerializeField] private float maxWaitTime = 1f;

    [Header("Values that control force that push fruits from spawnplaces")]
    [SerializeField] private float minForce = 15f;
    [SerializeField] private float maxForce = 20f;

    public IEnumerator SpawnFruits()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));  

            Transform t = spawnPlaces[Random.Range(0, spawnPlaces.Length)];

            GameObject gameObject = null;
            float randomizer = Random.Range(0, 100);

            if (randomizer < 10f)
            { 
                gameObject = bomb;
            }
            else { gameObject = fruitsToSpawn[Random.Range(0, fruitsToSpawn.Length)]; }

            fruit = Instantiate(gameObject, t.position, Quaternion.Euler(-90,0,0));
            fruit.GetComponent<Rigidbody2D>().AddForce(t.transform.up * Random.Range(minForce, maxForce), ForceMode2D.Impulse);

            Destroy(fruit, 5f);            
        }
    }
}
