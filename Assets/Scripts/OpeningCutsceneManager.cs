using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class OpeningCutsceneManager : MonoBehaviour
{
    [SerializeField] VideoClip clip;
    [SerializeField] VideoPlayer player;
    [SerializeField] string sceneToLoad;

    void Start()
    {
        // begin playing the opening cutscene video
        player.clip = clip;
        player.Play();
    }

    void Update()
    {
        // if cutscene video is finished
        // we want to check if user clicks, and if so go to next scene

        if (!player.isPlaying)
        {
            if (Input.GetMouseButton(0))
            {
                SceneManager.LoadScene(sceneToLoad);
            }
        }
    }
}
