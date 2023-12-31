using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Music_Pause : MonoBehaviour
{
    public AudioSource musicPlayer;
    public Button Music_on;
    bool IsPaused = false;

    private void Start()
    {
        musicPlayer = FindObjectOfType<AudioSource>();
        Music_on.interactable = true;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F7) && IsPaused == false)
        {
            musicPlayer.Stop();
            IsPaused = true;
            Music_on.interactable = false;
        }
        else if (Input.GetKeyDown(KeyCode.F7) && IsPaused == true)
        {
            musicPlayer.Play();
            IsPaused = false;
            Music_on.interactable = true;
        }
    }
}
