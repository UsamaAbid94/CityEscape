using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Person : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    private Rigidbody2D personBody;
    private Collider2D personCollider;

    bool despawnTimerActive = false;
    [SerializeField] float timeTillDespawn = 5;

    bool isMoving = true;
    public bool IsMoving { get { return isMoving; } }

    [SerializeField] float timeTillNextSfx = 3.5f;
    float currentTime;
    [SerializeField] AudioClip yell;
    AudioSource audioSource;

    private void OnEnable()
    {
        personBody = GetComponent<Rigidbody2D>();
        personCollider = GetComponent<Collider2D>();

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = yell;
        }

        personBody.gravityScale = 0f;
        personCollider.isTrigger = true; // essentially makes them intangible,
                                         // so they can just pass through the videoPlayer
    }
    
    void FixedUpdate()
    {
        if (isMoving)
        {
            personBody.velocity = Vector3.left * moveSpeed * Time.fixedDeltaTime;
        }

        currentTime -= Time.fixedDeltaTime;

        if (currentTime <= 0) 
        {
            audioSource.Play();
            currentTime = Random.Range(timeTillNextSfx - 2f, timeTillNextSfx + 2f);
        }

        if (despawnTimerActive)
        {
            timeTillDespawn -= Time.deltaTime;

            if (timeTillDespawn <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }

    public void SetGrabState()
    {
        isMoving = false; // this will also be used to determine whether we should give points 
        personBody.velocity = Vector3.zero;
    }

    public void SetThrowState()
    {
        personBody.gravityScale = 1f;
        personCollider.isTrigger = false;
        despawnTimerActive = true;
    }

    /*
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
    }*/

    public void SpawnFireSplash()
    {
        // realistically it's bad to be creating and destroying objects constantly,
        // but this is a game jam so i'm not concerned about performance much lol

        GameObject splash = Instantiate(GameManager.gameManager.fireSplash, transform.position, Quaternion.Euler(0f, 0f, 190f));
        GameObject splash2 = Instantiate(GameManager.gameManager.fireSplash, transform.position, Quaternion.Euler(0f, 0f, 145f));
        Destroy(splash, 0.55f);
        Destroy(splash2, 0.55f);
        Destroy(this.gameObject);
    }
}
