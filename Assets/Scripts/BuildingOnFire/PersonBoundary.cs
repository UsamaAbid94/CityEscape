using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonBoundary : MonoBehaviour
{
    [SerializeField] int pointsToReduce;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Person"))
        {
            GameManager.gameManager.UpdateScore(-pointsToReduce);
        }

        Destroy(collision.gameObject);
    }
}
