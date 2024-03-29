using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject[] peopleToSpawn;
    [SerializeField]
    private GameObject  fireBall;
    [SerializeField]
    private GameObject spawnPoint,fireballSpawnPoint;
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
             people = Instantiate(peopleToSpawn[Random.Range(0,peopleToSpawn.Length)], spawnPoint.transform
            .position, spawnPoint.transform.rotation);
            timeToSpawn = Random.Range(spawnTime - 1f, spawnTime + 1f);
        }

        if (timeToSpawnBall <= 0f)
        {
            Instantiate(fireBall, fireballSpawnPoint.transform.position, Quaternion.identity);
            timeToSpawnBall = Random.Range(6f, 7f);
        }

    }
}
