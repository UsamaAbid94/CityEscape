using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Person : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;

    bool despawnTimerActive = false;
    [SerializeField] float timeTillDespawn = 5;

    bool isMoving = true;
    public bool IsMoving { get { return isMoving; } }
     
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isMoving) 
        {
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;
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
    }

    public void StartDespawnTimer()
    {
        despawnTimerActive |= Time.deltaTime > 0;
    }
    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "HighArea":
                GameManager.gameManager.UpdateScore(10);
                gameObject.SetActive(false);
                break;
            case "MidArea":
                GameManager.gameManager.UpdateScore(5);
                gameObject.SetActive(false);
                break;
            case "LowArea":
                GameManager.gameManager.UpdateScore(2);
                gameObject.SetActive(false);
                break;
        }
    }*/
}
