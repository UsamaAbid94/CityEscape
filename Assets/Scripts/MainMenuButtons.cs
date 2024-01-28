using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    [SerializeField] GameObject creditsScreen;
    [SerializeField] string startingSceneName;

    public void StartButtonPressed()
    {
        SceneManager.LoadScene(startingSceneName);
    }

    public void CreditsButtonPressed()
    {
        creditsScreen.SetActive(true);
    }
    public void BackButtonPressed()
    {
        creditsScreen.SetActive(false);
    }
}
