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

<<<<<<< HEAD
    [SerializeField] GameObject instructions;
    bool gameStarted = false;

    [SerializeField] GameObject gameOverImage;
    bool gameFinished = false;
    [SerializeField] GameObject youWinImage;

=======
    public GameObject fireSplash;
    
>>>>>>> dfb9fda (New Changes 1:08 PM)
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
            SceneManager.LoadScene(1); // reload the scene to start again
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
        }
    }
   
}
