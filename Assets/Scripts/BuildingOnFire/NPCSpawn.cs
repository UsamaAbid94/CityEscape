using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject peopleToSpawn,fireBall;
    [SerializeField]
    private GameObject spawnPoint;
    [SerializeField]
    private float moveSpeed, ballMoveSpeed;

    public float spawnTime = 2f, maxTime=4f;
    private float timeToSpawn;
    private float timeToSpawnBall;
    GameObject people;
    GameObject ball;
    // Start is called before the first frame update
    void Start()
    {
        timeToSpawn = spawnTime;
        timeToSpawnBall = Random.Range(4f,6f);
    }

    // Update is called once per frame
    void Update()
    {
        timeToSpawn -= Time.deltaTime;
        timeToSpawnBall -= Time.deltaTime;
        if (timeToSpawn <= 0f)
        {
             people = Instantiate(peopleToSpawn, spawnPoint.transform
            .position, spawnPoint.transform.rotation);
            timeToSpawn = spawnTime;
        }

        if (timeToSpawnBall <= 0f)
        {
            Instantiate(fireBall, spawnPoint.transform.position, Quaternion.identity);
            timeToSpawnBall = Random.Range(4f,6f);
        }
       
    }
}
