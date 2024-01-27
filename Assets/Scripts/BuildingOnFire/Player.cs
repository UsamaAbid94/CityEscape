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
    [SerializeField] float jumpForce;
     Rigidbody2D playerBody;
    

    //Saving GrandMa
    public float savingTime=2f;
    public float timeToSave;

    private bool isGrounded;


    private void Awake()
    {
        playerBody = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        timeToSave = savingTime; 
    }

    // Update is called once per frame
    void Update()
    {
       
            if (isHoldingCivilian)
            {

      

            if (Input.GetMouseButtonDown(0))
                {
                    isAiming = true;

                GetComponent<BoxCollider2D>().enabled = false;
                playerBody.gravityScale = 0f;

            }
                if (Input.GetMouseButtonUp(0))
                {
                    Launch();
                    isAiming = false;

                GetComponent<BoxCollider2D>().enabled = true;
               
                playerBody.gravityScale = 1f;
            }
            }    

            if (isAiming)
            {
                UpdateAim();
            
            GetComponent<BoxCollider2D>().enabled = false;
            playerBody.gravityScale = 0f;
            }

            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
              
                playerBody.AddForce(Vector2.up* jumpForce, ForceMode2D.Impulse);
               isGrounded =false;
            }
        

        //if (playerMissions == PlayerMissions.SavingGrandMa)
        //{
        //    timeToSave -= Time.deltaTime;
        //    if (timeToSave <= 0f)
        //    {
        //        transform.position = Vector3.Lerp(transform.position, new Vector3(2f, -1.72f, 0f), 0.02f);
        //    }
        //}
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
        civilianRB.AddForce(-launchDir.normalized * throwForce);
        isHoldingCivilian = false;
        civilianRB = null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // if a person walks into us, and we're not already holding a person,
        // pick the person up, disabling their walk script and making them immobile
        Person person = collision.gameObject.GetComponent<Person>();

        if (person != null)
        {
            if (!isHoldingCivilian)
            {
                isHoldingCivilian = true;
                
                civilianRB = collision.rigidbody;
                civilianRB.isKinematic = true;
               // collision.gameObject.transform.parent = this.gameObject.transform;
                civilianRB.gameObject.GetComponent<Person>().enabled = false;
                civilianRB.transform.position = throwingPos;
            }
        }

        if (collision.gameObject.tag.Equals("Peopl"))
        {
            
        }

        isGrounded = true;

        

    }
}
