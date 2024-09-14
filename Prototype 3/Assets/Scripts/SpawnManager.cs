using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;

    //private Vector3 spawnPosition = new Vector3(25, 0, 0);

    private float startDelay = 2;
    private float spawnInterval = 1.5f;

    private PlayerController playerControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnObjects", startDelay, spawnInterval);
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnObjects ()
    {
        Vector3 spawnLocation = new Vector3(30, Random.Range(3, 11), 0); 
        int index = Random.Range(0, obstaclePrefabs.Length);

        if (!playerControllerScript.gameOver)
        {
            Instantiate(obstaclePrefabs[index], spawnLocation, obstaclePrefabs[index].transform.rotation);
        }
    }
}
