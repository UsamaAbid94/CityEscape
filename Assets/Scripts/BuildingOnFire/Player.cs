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
    [SerializeField] float jumpForce;

    Rigidbody2D civilianRB;
     Rigidbody2D playerBody;

    Animator dillAnim;
    AudioSource playerAudio;
    AudioClip pickupSfx;
    AudioClip throwSfx;
    AudioClip jumpSfx;



    


    //Saving GrandMa
    /*
    public float savingTime=2f;
    public float timeToSave;
    */
    private bool isTimeToPick;
    private bool isGrounded;


    public GameObject smallShadow, midShadow, HighShadow;
    private void Awake()
    {
        playerBody = GetComponent<Rigidbody2D>();
        dillAnim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        //timeToSave = savingTime; 
    }

    // Update is called once per frame
    void Update()
    {

        if (isHoldingCivilian)
        {

            civilianRB.transform.position = Vector3.Lerp(civilianRB.transform.position, throwingPos, 0.02f);

            if (isTimeToPick)
            {
                dillAnim.SetBool("aiming", true);
                isTimeToPick = false;

                if (playerAudio != null)
                {
                    playerAudio.clip = pickupSfx;
                    playerAudio.Play();
                }
            }

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
                dillAnim.SetBool("aiming", isAiming);

                if (playerAudio != null)
                {
                    playerAudio.clip = throwSfx;
                    playerAudio.Play();
                }
                
                playerBody.gravityScale = 1f;
                if (!isGrounded)
                {
                    dillAnim.Play("DillJumpThrow");
                }
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


            JumpPlayer(jumpForce);
            StartCoroutine(ShadowChanges());
            isGrounded = false;

            if (playerAudio != null)
            {
                playerAudio.clip = jumpSfx;
                playerAudio.Play();
            }

        }
    }

    void JumpPlayer(float jumpForce)
    {
        playerBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        dillAnim.Play("DillJump");
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
        civilianRB.gameObject.transform.parent = transform;

        Vector3 launchDir = Input.mousePosition - Camera.main.WorldToScreenPoint(civilianRB.transform.position);
        civilianRB.AddForce(-launchDir.normalized * throwForce);

        isHoldingCivilian = false;
        civilianRB = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("FireBall"))
        {
            GameManager.gameManager.HurtPlayer();
            Debug.Log("Collding With balls");
        }
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

                civilianRB.gameObject.transform.parent = transform;
                civilianRB.gameObject.GetComponent<Person>().enabled = false;
             
             
                isTimeToPick = true;
            }
        }

        isGrounded = true;
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
}
