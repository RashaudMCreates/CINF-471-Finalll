using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void BootIn()
    {
        SceneManager.LoadScene(2);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!!!");
        Application.Quit();
    }
}
