using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

//https://gamedevbeginner.com/how-to-make-countdown-timer-in-unity-minutes-seconds/#:~:text=Making%20a%20countdown%20timer%20in%20Unity%20involves%20storing%20a%20float#:~:text=Making%20a%20countdown%20timer%20in%20Unity%20involves%20storing%20a%20float
public class Timer : MonoBehaviour
{
    public UnityEvent GameOver;
    public float timeRemaining;
    public float TimePickupValue;
    public bool IsRunning = false;
    public TMP_Text timerText;
    public int GearsRecieved;
    

    void Start()
    {
        //IsRunning = true;
    }
    void Update()
    {
        if (IsRunning == true)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                Displaytime(timeRemaining);
            }
            else
            {
                timeRemaining = 0;
                IsRunning = false;
                //gameover
            }
        }
        else
        {
            //hud.SetLoseScreen();
            //GameOver.Invoke();

        }

        
    }
    public void StartTime()
    {
        IsRunning = true;
        
    }

    public void EndTime()
    {
        IsRunning = false; 
        //store timetremaining to gamemanager;
    }
    void Displaytime(float timeDisplay)
    {
        timeDisplay += 1;
        float min = Mathf.FloorToInt(timeDisplay / 60);
        float sec = Mathf.FloorToInt(timeDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", min, sec);
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Find the player GameObject and get the PlayerController component
            Player playerController = other.GetComponent<Player>();
            if(playerController.Gear != 0)
            {
                playerController.UseItem();
                AddTime();
            }
            else
            {
                //get interact script popupmessage to display error 
                Debug.Log("Print error: no time to add");
            }
        }
    }

    public void AddTime()
    {
            timeRemaining += TimePickupValue;
            GearsRecieved += 1;

        //Displaytime(timeRemaining);
    }
}
