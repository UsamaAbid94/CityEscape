using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text scoreToReachText;
    [SerializeField] int scoreToReach = 200;

    public Sprite[] pickleSprite;

    public Image PickleImage;

    private int imageCounter;
    private int gameScore;


    [SerializeField] GameObject instructions;
    bool gameStarted = false;

    bool gameFinished = false;
    [SerializeField] GameObject gameOverImage;
    [SerializeField] GameObject youWinImage;
    [SerializeField] AudioClip gameOverMusic;
    [SerializeField] AudioClip youWinMusic;
    AudioSource audioSource;

    public GameObject fireSplash;
    

    private void Awake()
    {
        if (gameManager == null)
        {
            gameManager = this;
        }
        else
        {
            Destroy(gameManager);
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        scoreToReachText.text = scoreToReach.ToString();

        instructions.SetActive(true);
        Time.timeScale = 0.0f; // pause for instructions
    }

    void Update()
    {
        if (!gameStarted && Input.GetMouseButtonDown(0))
        {
            gameStarted = true;
            Time.timeScale = 1.0f;
            instructions.SetActive(false); // close instructions popup
        }

        if (gameFinished && Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // reload the scene to start again
        }
    }

    

    public void UpdateScore(int gameScore) {

        this.gameScore = this.gameScore + gameScore;
        scoreText.text =this.gameScore.ToString();

        if (this.gameScore >= scoreToReach)
        {
            youWinImage.SetActive(true);
            gameFinished = true;
            Time.timeScale = 0f; // set up win screen and pause

            audioSource.clip = youWinMusic;
            audioSource.Play();
        }
    }

    public void HurtPlayer()
    {
        if (imageCounter < pickleSprite.Length)
        {
            PickleImage.sprite = pickleSprite[imageCounter];
            imageCounter++;

        }
        else
        {
            PickleImage.gameObject.SetActive(false);

            gameOverImage.SetActive(true);
            gameFinished = true;
            Time.timeScale = 0f; // set up gameover screen and pause

            audioSource.clip = gameOverMusic;
            audioSource.Play();
        }
    }
   
}
