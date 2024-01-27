using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    [SerializeField]
    private Text scoreText;

    private int gameScore;
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
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScore(int gameScore) {

        this.gameScore = this.gameScore + gameScore;
        scoreText.text =this.gameScore.ToString();
    }

   
}
