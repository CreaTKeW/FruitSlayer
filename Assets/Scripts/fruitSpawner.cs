using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class fruitSpawner : MonoBehaviour
{
    public GameObject fruitToSpawn;
    public Transform[] spawnPlaces;
    public float minWaitTime = .3f;
    public float maxWaitTime = 1f;
    public float minForce = 15f;
    public float maxForce = 20f;

    void Start()
    {
        StartCoroutine(SpawnFruits());    
    }

    private IEnumerator SpawnFruits()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));

            Transform t = spawnPlaces[Random.Range(0, spawnPlaces.Length)];
            GameObject fruit = Instantiate(fruitToSpawn, t.position, t.rotation);

            fruit.GetComponent<Rigidbody2D>().AddForce(t.transform.up * Random.Range(minForce, maxForce), ForceMode2D.Impulse);
            Destroy(fruit, 5f);
        }
    }
}
