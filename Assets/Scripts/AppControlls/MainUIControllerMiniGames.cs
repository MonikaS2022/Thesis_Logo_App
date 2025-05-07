using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainUIControllerMiniGames : MonoBehaviour
{
    //extra for timer
    private int previousSeconds = -1;

    public Button startExercButton;
    public GameObject startExcerBtnCanvas;
    public Button startMiniGameBtn;

    public string[] miniGameScenes= new string[3] { "MiniGame1", "MiniGame2", "MiniGame3" };
    int sceneToLoad = 0;
    public bool wasLastMiniGame = false;

    public bool isExercTimerActive = false;
    public float exerciseTimerCountdown = 7f;
    public float resetExerciseTimerCountdown = 7f;
    
    public bool applicationStart = false;

    public GameObject background;

    public GameObject mainCamera;
    public EventSystem mainEvent;

    public Camera miniGameCamera;
    public EventSystem miniGameEvent;
    public AudioListener miniGameAudioListener;

    public static event Action OnEndMiniGame;
    public static void RaiseEndMiniGame()=>OnEndMiniGame?.Invoke();
    void Start()
    {
        startExercButton.onClick.AddListener(StartTimer);
        startMiniGameBtn.onClick.AddListener(StartMiniGame);
    }

    private void OnEnable()
    {
        OnEndMiniGame += EndMiniGame;
    }
    private void OnDisable()
    {
        OnEndMiniGame -= EndMiniGame;
    }

    void Update()
    {
        //CheckingTimer();


        if (isExercTimerActive)
       {
          exerciseTimerCountdown -= Time.deltaTime;
       }

       if(exerciseTimerCountdown < 0 && applicationStart)
       {
          applicationStart = false;
          isExercTimerActive = false;

          background.SetActive(true);

            if (!wasLastMiniGame)
            {
                startMiniGameBtn.gameObject.SetActive(true);
            }
            else
            {
                EndExercise();
            }
       }
    }

    void StartTimer()
    {
        exerciseTimerCountdown = resetExerciseTimerCountdown;
        startExercButton.interactable = false;
        startExcerBtnCanvas.gameObject.SetActive(false);
        
        isExercTimerActive = true;
        applicationStart = true;
    }

    void StartMiniGame()
    {
        string miniGameScene = miniGameScenes[sceneToLoad];
        startMiniGameBtn.gameObject.SetActive(false);
        mainEvent.gameObject.SetActive(false);
        mainCamera.gameObject.SetActive(false);
        SceneManager.LoadScene(miniGameScene, LoadSceneMode.Additive);
        StartCoroutine(AssignMiniGameAudioListenerandEvent(miniGameScene));

        
       
    }

    private IEnumerator AssignMiniGameAudioListenerandEvent(string miniGameScene)
    {
        //Debug.Log("async assigminigamelistenerandEvent...");
        
        while (!SceneManager.GetSceneByName(miniGameScene).isLoaded)
        {
            yield return null;
        }
        miniGameCamera = GameObject.Find("MiniGame1Camera").GetComponent<Camera>();
        miniGameAudioListener = miniGameCamera.GetComponent<AudioListener>();
        miniGameEvent = GameObject.Find("MiniEventSystem").GetComponent<EventSystem>();
        
        //stopMiniGameBtn = GameObject.Find("StopMiniGame").GetComponent<Button>();
        

        if (miniGameAudioListener != null)
        {
            miniGameAudioListener.gameObject.SetActive(true);
            miniGameEvent.gameObject.SetActive(true);
            //Debug.Log("Mini-Game Audio Listener and Event assigned and active.");
        }
    }


    void EndMiniGame()
    {
        
        background.SetActive(false);
        mainCamera.gameObject.SetActive(true);
        StartCoroutine(UnloadMiniGameSceneAsync(miniGameScenes[sceneToLoad]));
        sceneToLoad++;
        miniGameAudioListener.gameObject.SetActive(false);
        miniGameEvent.gameObject.SetActive(false);
        mainEvent.gameObject.SetActive(true);
        StartTimer();
        if (sceneToLoad >= miniGameScenes.Length)
        {
            wasLastMiniGame = true;
        }
    }

    private IEnumerator UnloadMiniGameSceneAsync(string miniGameScene)
    {
        AsyncOperation unloadOperation = SceneManager.UnloadSceneAsync(miniGameScene);
        while(!unloadOperation.isDone)
        {
            yield return null;
        }
    }

    void EndExercise()
    {
        string buttonText = "END";
        startExcerBtnCanvas.GetComponentInChildren<TextMeshProUGUI>().text = buttonText;

        startExcerBtnCanvas.gameObject.SetActive(true);
    }


    void CheckingTimer()
    {
        int currentSeconds = Mathf.FloorToInt(exerciseTimerCountdown);
        if (currentSeconds != previousSeconds)
        {
            previousSeconds = currentSeconds;
            Debug.Log("Timer: " + currentSeconds);
        }
    }



}