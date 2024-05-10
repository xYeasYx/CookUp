using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreCountetr : MonoBehaviour
{

    public GameObject currentCanvas;
    public GameObject PauseCanvas;
    public static int score = 0;
    public Text scoreText;
    public float distanceToRim;
     //public Slider healthSlider;
    //public float currHealth = 10000f;

    public Transform player; // Reference to the rim object
    public float threePointDistance = 9f;
    // Start is called before the first frame update


    // This method is called whenever another collider enters the trigger collider attached to this object.
    public void OnTriggerEnter(Collider other)
    {
        // Check if the collider belongs to the ball
        if (other.CompareTag("BBall"))
        {
            if(distanceToRim < threePointDistance){
                
            score += 2;
            UpdateScoreText();
            //DefenderAI.DefenderHealth= DefenderAI.DefenderHealth - 1f;
        }
        if(distanceToRim >= threePointDistance){
                
            score += 3;
            UpdateScoreText();
            //DefenderAI.DefenderHealth= DefenderAI.DefenderHealth - 1f;
        }
        }
    }
     private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
    }
    void Start()
    {
        UpdateScoreText();
        //healthSlider.value = currHealth;
    }
     public void PauseButton()
    {
        // Activate the about canvas and deactivate the current canvas
        PauseCanvas.SetActive(true);
        currentCanvas.SetActive(false);
        Time.timeScale = 0f;
    }
     public void BackButton()
    {
        // Activate the about canvas and deactivate the current canvas
        currentCanvas.SetActive(true);
        PauseCanvas.SetActive(false);
        
    }
    public void ResumeGame()
    {   PauseCanvas.SetActive(true);
        currentCanvas.SetActive(false);
        Time.timeScale = 1f; // Set time scale back to normal to resume time
        //isPaused = false;
    }

    public void QuitButton()
    {
        // Quit the application
        Application.Quit();
    }
    // Update is called once per frame
    void Update()
    {   //healthSlider.value = currHealth;
        //Debug.Log(currHealth);
        distanceToRim = Vector3.Distance(transform.position, player.position);
       // Debug.Log(distanceToRim);
    }
    public void PlayButton()
    {
        // Load the next scene (you can adjust the scene name or index accordingly)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
