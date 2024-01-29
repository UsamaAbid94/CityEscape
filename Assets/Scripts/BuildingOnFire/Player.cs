using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    /*public enum PlayerMissions
    {
        SavingPeople,
        SavingGrandMa,
        CallingPhoneCall
    }

    public PlayerMissions playerMissions;*/

    bool isHoldingCivilian = false;

    [SerializeField] float throwForce;
    [SerializeField] Vector3 throwingPosOffset;
    [SerializeField] float jumpForce;

    Person currentHeldPerson;
    Rigidbody2D civilianRB;
    Rigidbody2D playerBody;

    Animator dillAnim;
    AudioSource playerAudio;
    [SerializeField] AudioClip throwSfx;
    [SerializeField] AudioClip jumpSfx;
    [SerializeField] AudioClip hurtSfx;



    private bool isTimeToPickUp;
    private bool isGrounded;


    public GameObject smallShadow, midShadow, HighShadow;
    private void Awake()
    {
        playerBody = GetComponent<Rigidbody2D>();
        dillAnim = GetComponent<Animator>();

        playerAudio = GetComponent<AudioSource>();
    }
    
    void Update()
    {

        if (isHoldingCivilian)
        {
            // bring person to the hold pos
            currentHeldPerson.transform.position = Vector3.Lerp(currentHeldPerson.transform.position, 
                                                                transform.position + throwingPosOffset, 
                                                                0.2f);

            // set grab anim
            if (isTimeToPickUp)
            {
                dillAnim.SetBool("aiming", true);
                isTimeToPickUp = false;
            }

            if (Input.GetMouseButtonDown(0))
            {
                UpdateAim();
            }

            if (Input.GetMouseButtonUp(0))
            {
                Launch();
                dillAnim.SetBool("aiming", false);

                if (playerAudio != null)
                {
                    playerAudio.clip = throwSfx;
                    playerAudio.Play();
                }

                if (!isGrounded)
                {
                    dillAnim.Play("DillJumpThrow");
                }
            }
        }


        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            JumpPlayer(jumpForce);
            StartCoroutine(ShadowChanges());

            if (playerAudio != null)
            {
                playerAudio.clip = jumpSfx;
                playerAudio.Play();
            }
        }
    }

    void JumpPlayer(float jumpForce)
    {
        playerBody.AddForce(Vector2.up * jumpForce);//, ForceMode2D.Impulse);
        dillAnim.Play("DillJump");
    }

    void UpdateAim()
    {
        // have the person rotate to point toward the mouse

        Vector3 direction = Input.mousePosition - Camera.main.WorldToScreenPoint(civilianRB.transform.position); // calculate dist from mouse
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // determine angle of change
        civilianRB.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void Launch()
    {
        currentHeldPerson.SetThrowState();

        Vector3 launchDir = Input.mousePosition - Camera.main.WorldToScreenPoint(civilianRB.transform.position);
        civilianRB.AddForce(-launchDir.normalized * throwForce);

        isHoldingCivilian = false;
        civilianRB = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if a person walks into us, and we're not already holding a person,
        // pick the person up, disabling their walk script and making them immobile
        Person person = collision.gameObject.GetComponent<Person>();

        if (person != null)
        {
            if (!isHoldingCivilian)
            {
                isHoldingCivilian = true;
                isTimeToPickUp = true;

                currentHeldPerson = person;
                civilianRB = collision.attachedRigidbody;

                person.SetGrabState();
            }
        }


        if (collision.gameObject.tag.Equals("FireBall"))
        {
            GameManager.gameManager.HurtPlayer();

            playerAudio.clip = hurtSfx;
            playerAudio.Play();
            StartCoroutine(ChangeColor());
            //Debug.Log("Collding With balls");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }



    IEnumerator ShadowChanges()
    {
        HighShadow.SetActive(false);
        midShadow.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        midShadow.SetActive(false);
        smallShadow.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        smallShadow.SetActive(false);
        HighShadow.SetActive(true);
        dillAnim.Play("DillIdle");
    }

    IEnumerator ChangeColor()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }
}