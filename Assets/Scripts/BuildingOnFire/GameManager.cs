using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    [SerializeField]
    private Text scoreText;

    public Sprite[] pickleSprite;

    public Image PickleImage;

    private int imageCounter;
    private int gameScore;

    [SerializeField] GameObject instructions;
    bool gameStarted;
    [SerializeField] GameObject gameOverImage;

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

        instructions.SetActive(true);
        Time.timeScale = 0.0f;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameStarted && Input.GetMouseButtonDown(0))
        {
            gameStarted = true;
            Time.timeScale = 1.0f;
            instructions.SetActive(false); // close instructions popup
        }
    }

    public void UpdateScore(int gameScore) {

        this.gameScore = this.gameScore + gameScore;
        scoreText.text =this.gameScore.ToString();
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
        }
    }
   
}
