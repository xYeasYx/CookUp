using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayButton()
    {
        // Load the next scene (you can adjust the scene name or index accordingly)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
