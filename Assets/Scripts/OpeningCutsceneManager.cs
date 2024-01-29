using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class OpeningCutsceneManager : MonoBehaviour
{
    [SerializeField] VideoPlayer videoPlayer;
    [SerializeField] GameObject rawImage;
    [SerializeField] AudioSource source;

    bool cutsceneStarted;
    bool musicActive;

    void OnEnable()
    {
        Time.timeScale = 1.0f;
        source.Stop();

        StartCoroutine(InitCutscene());
    }

    void Update()
    {
        // if cutscene video is finished
        // we want to check if user clicks, and if so go to next scene

        if (cutsceneStarted)
        {
            if (!videoPlayer.isPlaying && !musicActive)
            {
                videoPlayer.gameObject.SetActive(false);
                rawImage.gameObject.SetActive(false);
                source.Play();
                musicActive = true;
            }
        }
        
    }

    IEnumerator InitCutscene()
    {
        yield return new WaitForSeconds(4);
        cutsceneStarted = true;
    }
}
