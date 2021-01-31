using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeySpawner : MonoBehaviour
{
    public GameObject keyPrefab;
    public GameObject[] spawnLocations1;
    public GameObject[] spawnLocations2;
    // Start is called before the first frame update
    void Start()
    {
        int key1 = Random.Range(0, spawnLocations1.Length);
        int key2 = Random.Range(0, spawnLocations2.Length);
        Instantiate(keyPrefab, spawnLocations1[key1].transform.position, Quaternion.identity).transform.Rotate(new Vector3(90, 0, 0));
        Instantiate(keyPrefab, spawnLocations2[key2].transform.position, Quaternion.identity).transform.Rotate(new Vector3(90, 0, 0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
