using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class GameManager : MonoBehaviour
{
    public Text timeText;
    public Text itText;
    public Text itCountText;
    public GameObject pauseScreen;
    public GameObject endScreen;
    public InputField nameInput;
    public TextAsset scores;

    bool isPaused = false;
    public bool timerRunning = false;
    public float timeRemaining = 20;
    float seconds;

    //Game scores
    public int itCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        timerRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && timeRemaining > 0)
        {
            isPaused = !isPaused;

            if(isPaused)
            {
                //Set timer to false
                timerRunning = false;
                Time.timeScale = 0;
                pauseScreen.SetActive(true);

                
            }
            else
            {
                timerRunning = true;
                Time.timeScale = 1;
                pauseScreen.SetActive(false);
            }
        }


        if(timerRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                seconds = Mathf.FloorToInt(timeRemaining % 60);
                DisplayTime();
            }
            else
            {
                Debug.Log("Timer finished. Round over");
                timeRemaining = 0;
                DisplayTime();
                timerRunning = false;

                Time.timeScale = 0;
                itCountText.text = itCount.ToString() + " times";
                endScreen.SetActive(true);
                Cursor.lockState = CursorLockMode.None;

                

            }
        }
        
    }

    public void UpdateStatus(bool status)
    {
        itText.text = "IT status: " + status;
        
    }

    void DisplayTime()
    {
        timeText.text = "Time Remaining: " + seconds + "s";
    }

    public void MainMenu()
    {
        Time.timeScale = 1; //Set time back to start
        SceneManager.LoadScene(0); //load the main menu scene
    }

    public void SaveScore()
    {
        string name = nameInput.text;

        if(name == "")
        {
            name = "NULL";
        }


        //save to text file -- using application.datapath
        File.AppendAllText(Application.dataPath + "/Resources/scores.txt", "\n");
        File.AppendAllText(Application.dataPath + "/Resources/scores.txt", name + "-" + itCount);
        
    }
}
