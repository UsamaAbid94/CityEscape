using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Person : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    private Rigidbody2D personBody;
    private void Awake()
    {
        personBody = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        personBody.velocity = Vector3.left * moveSpeed * Time.fixedDeltaTime;
    }

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
    }
}
