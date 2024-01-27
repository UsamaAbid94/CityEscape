using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreTracker : MonoBehaviour
{
    static int currentScore;
    [SerializeField] int pointsToAddWhenHit;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        // check to make sure we should give points, see if person is still moving

        Person person = collision.gameObject.GetComponent<Person>();

        if (person != null)
        {
            if (!person.IsMoving)
            {
                GameManager.gameManager.UpdateScore(pointsToAddWhenHit);
                Destroy(person.gameObject);
            }
        }
    }

}
