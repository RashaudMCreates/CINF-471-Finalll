using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioSceneSwap : MonoBehaviour
{
    // I ain't got time to figure shit out. Lesssss COPY AND PAST THIS SCRIPT FOR EVERY CUTSCENE
    public AudioSource audioSource;
    

    void Update()
    {
        if (!audioSource.isPlaying && audioSource.time > 0)
        {
            SceneManager.LoadScene(3);
        }
    }

}

