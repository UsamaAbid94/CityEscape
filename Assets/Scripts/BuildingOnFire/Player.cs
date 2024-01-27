using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum PlayerMissions
    {
        SavingPeople,
        SavingGrandMa,
        CallingPhoneCall
    }

    public PlayerMissions playerMissions;

    bool isHoldingCivilian = false;
    bool isAiming = false;
    [SerializeField] float throwForce;
    [SerializeField] Vector3 throwingPos;
    Rigidbody2D civilianRB;
    Collider2D playerCollider;


    //Saving GrandMa
    public float savingTime=2f;
    public float timeToSave;



    void Start()
    {
        playerCollider = GetComponent<Collider2D>();
        timeToSave = savingTime; 
    }

    void Update()
    {
        if (playerMissions == PlayerMissions.SavingPeople)
        {
            // if we're holding a person
            // allow mouse aim
            // fling in aimed direction when you click
            if (isHoldingCivilian)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    isAiming = true;

                }
                if (Input.GetMouseButtonUp(0))
                {
                    Launch();
                    isAiming = false;
                    GetComponent<BoxCollider2D>().enabled = true;
                }
            }

            if (isAiming)
            {
                UpdateAim();
                GetComponent<BoxCollider2D>().enabled = false;
            }
        }

        if (playerMissions == PlayerMissions.SavingGrandMa)
        {
            timeToSave -= Time.deltaTime;
            if (timeToSave <= 0f)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(2f, -1.72f, 0f), 0.02f);
            }
        }
    }

    void UpdateAim()
    {
        // change forward trajectory of rb

        //Vector3 mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);

        Vector3 direction = Input.mousePosition - Camera.main.WorldToScreenPoint(civilianRB.transform.position); // calculate dist from mouse
        float angle = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg; // determine angle of change
        civilianRB.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void Launch()
    {
        civilianRB.isKinematic = false;

        Vector3 launchDir = Input.mousePosition - Camera.main.WorldToScreenPoint(civilianRB.transform.position);
        civilianRB.AddForce(-launchDir * throwForce);
        civilianRB.gameObject.GetComponent<Person>().StartDespawnTimer();

        isHoldingCivilian = false;
        civilianRB = null;
        playerCollider.enabled = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // if a person walks into us, and we're not already holding a person,
        // pick the person up, disabling their walk script and making them immobile

        if (playerMissions == PlayerMissions.SavingPeople)
        {
            if (!isHoldingCivilian)
            {
                isHoldingCivilian = true;

                civilianRB = collision.rigidbody;
                civilianRB.isKinematic = true;
                civilianRB.gameObject.GetComponent<Person>().enabled = false;
                civilianRB.transform.position = throwingPos;
            }

        }

    }
}
