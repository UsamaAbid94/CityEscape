using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneTimer : MonoBehaviour
{
    [SerializeField] bool timerActive;
    [SerializeField] float timeTillChangeScene;
    [SerializeField] string sceneToLoadTo;

    void Update()
    {
        if (timerActive)
        {
            timeTillChangeScene -= Time.deltaTime;

            if (timeTillChangeScene <= 0)
            {
                SceneManager.LoadScene(sceneToLoadTo);
            }
        }
    }
}
