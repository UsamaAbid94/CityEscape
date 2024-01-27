using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Person : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;
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
