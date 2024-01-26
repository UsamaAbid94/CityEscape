using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject peopleToSpawn;
    [SerializeField]
    private GameObject spawnPoint;
    [SerializeField]
    private float moveSpeed;

    public float spawnTime = 2f;
    private float timeToSpawn;
    GameObject people;
    // Start is called before the first frame update
    void Start()
    {
        timeToSpawn = spawnTime;
    }

    // Update is called once per frame
    void Update()
    {
        timeToSpawn -= Time.deltaTime;
        if (timeToSpawn <= 0f)
        {
             people = Instantiate(peopleToSpawn, spawnPoint.transform
            .position, spawnPoint.transform.rotation);
            timeToSpawn = spawnTime;
        }
    
    }
}
