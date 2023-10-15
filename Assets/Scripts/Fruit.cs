using UnityEngine;

public class Fruit : MonoBehaviour
{
    public GameObject slicedFruitPrefab;    

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SpawnSlicedFruit();
        }
    }

    public void SpawnSlicedFruit()
    {
        GameObject instance = (GameObject)Instantiate(slicedFruitPrefab, transform.position, transform.rotation);
        Rigidbody[] rbsOnSlice = instance.transform.GetComponentsInChildren<Rigidbody>();

        foreach(Rigidbody r in rbsOnSlice)
        {
            r.transform.rotation = Random.rotation;
            r.AddExplosionForce(Random.Range(500, 750), transform.position, 2.5f);
        }

        Destroy(instance.gameObject, 5f);
        Destroy(this.gameObject);
    }
}
