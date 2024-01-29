using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoringBox : MonoBehaviour
{
    static int currentScore;
    [SerializeField] int pointsToAddWhenHit;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        // check to make sure we should give points, see if person is still moving 
        // (they would be moving if they just spawned in)

        Person person = collision.gameObject.GetComponent<Person>();

        if (person != null)
        {
            if (!person.IsMoving)
            {
                GameManager.gameManager.UpdateScore(pointsToAddWhenHit);
                person.SpawnFireSplash();
                Destroy(person.gameObject);
            }
        }
    }
}
