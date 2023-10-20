using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class fruitSpawner : MonoBehaviour
{
    public GameObject[] fruitsToSpawn;
    public GameObject bomb;
    public Transform[] spawnPlaces;
    public float minWaitTime = .3f;
    public float maxWaitTime = 1f;
    public float minForce = 15f;
    public float maxForce = 20f;

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

            GameObject fruit = Instantiate(gameObject, t.position, Quaternion.Euler(-90,0,0));

            fruit.GetComponent<Rigidbody2D>().AddForce(t.transform.up * Random.Range(minForce, maxForce), ForceMode2D.Impulse);
            Destroy(fruit, 5f);
        }
    }
}
