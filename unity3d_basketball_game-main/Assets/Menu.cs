using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject currentCanvas;
    public GameObject aboutCanvas;

    public void PlayButton()
    {
        // Load the next scene (you can adjust the scene name or index accordingly)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void AboutButton()
    {
        // Activate the about canvas and deactivate the current canvas
        aboutCanvas.SetActive(true);
        currentCanvas.SetActive(false);
    }
    public void BackButton()
    {
        // Activate the about canvas and deactivate the current canvas
        currentCanvas.SetActive(true);
        aboutCanvas.SetActive(false);
        
    }

    public void QuitButton()
    {
        // Quit the application
        Application.Quit();
    }
}
