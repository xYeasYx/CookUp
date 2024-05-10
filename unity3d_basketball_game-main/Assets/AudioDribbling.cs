using UnityEngine;

public class AudioDribbling : MonoBehaviour
{
    public AudioClip audioClip; // Assign your audio clip in the Inspector
    private AudioSource audioSource;

    void Start()
    {
        // Get the AudioSource component attached to this GameObject
        audioSource = GetComponent<AudioSource>();

        // Make sure an audio clip is assigned
        if (audioClip == null)
        {
            Debug.LogError("Audio clip is not assigned!");
        }
    }

    void Update()
    {
        // Check if any arrow key is pressed
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) ||
            Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            // Play the audio clip
            if (audioClip != null && !audioSource.isPlaying)
            {
                audioSource.PlayOneShot(audioClip);
            }
        }
    }
}