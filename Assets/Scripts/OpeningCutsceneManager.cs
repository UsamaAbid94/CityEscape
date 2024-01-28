using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class OpeningCutsceneManager : MonoBehaviour
{
    [SerializeField] VideoClip clip;
    [SerializeField] VideoPlayer player;
    [SerializeField] GameObject rawImage;

    bool cutsceneStarted;
    bool musicActive;

    void Start()
    {

        GetComponent<AudioSource>().Stop();

        // begin playing the opening cutscene video
        //player.clip = clip;
        //player.Play();

        StartCoroutine(InitCutscene());
    }

    void Update()
    {
        // if cutscene video is finished
        // we want to check if user clicks, and if so go to next scene

        if (cutsceneStarted)
        {
            if (!player.isPlaying && !musicActive)
            {

                player.gameObject.SetActive(false);
                rawImage.gameObject.SetActive(false);
                GetComponent<AudioSource>().Play();
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
