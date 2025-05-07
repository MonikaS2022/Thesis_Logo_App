using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameManager : MonoBehaviour
{
    //extra for timer
    private int previousSeconds = -1;

    public bool isMiniGameTimerActive = true;
    public float miniGameTimerCountdown = 5f;

    public Button stopMiniGameBtn;

    public FruitSpawner fallingfruitsStop;
    public PlayerMovementMiniGame1 basketStop;


    private void Update()
    {
        //CheckingTimer();

        if (isMiniGameTimerActive)
        {
            miniGameTimerCountdown -= Time.deltaTime;
        }

        if(miniGameTimerCountdown <= 0 )
        {
            isMiniGameTimerActive = false;
            stopMiniGameBtn.gameObject.SetActive(true);
            fallingfruitsStop.gameObject.SetActive(false);
            basketStop.gameObject.SetActive(false);
        }

    }




    void CheckingTimer()
    {
        int currentSeconds = Mathf.FloorToInt(miniGameTimerCountdown);
        if (currentSeconds != previousSeconds)
        {
            previousSeconds = currentSeconds;
            Debug.Log("Timer: " + currentSeconds);
        }
    }


}
