using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Person : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    private Rigidbody2D personBody;

    [SerializeField] float timeTillNextSfx = 3.5f;
    float currentTime;
    [SerializeField] AudioClip yell;
    AudioSource audioSource;

    private void Awake()
    {
        personBody = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        personBody.velocity = Vector3.left * moveSpeed * Time.fixedDeltaTime;

        if (currentTime <= 0) 
        {
            audioSource.Play();
            currentTime = Random.Range(timeTillNextSfx - 2f, timeTillNextSfx + 2f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "HighArea":
                GameManager.gameManager.UpdateScore(30);
                SpawnFireSplash(this.gameObject);
                break;
            case "MidArea":
                GameManager.gameManager.UpdateScore(20);
                SpawnFireSplash(this.gameObject);
                break;
            case "LowArea":
                GameManager.gameManager.UpdateScore(10);
                SpawnFireSplash(this.gameObject);
                break;
        }
    }

    public void SpawnFireSplash(GameObject TargetPosition)
    {
        GameObject splash = Instantiate(GameManager.gameManager.fireSplash, TargetPosition.transform.position, Quaternion.Euler(0f, 0f, 190f));
        Destroy(splash, 1f);
        gameObject.SetActive(false);
    }
}
