using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonBoundary : MonoBehaviour
{
    [SerializeField] int pointsToReduce;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Person person = collision.gameObject.GetComponent<Person>();

        if (person != null)
        {
            GameManager.gameManager.UpdateScore(-pointsToReduce);
            Destroy(person.gameObject);
        }
    }
}
