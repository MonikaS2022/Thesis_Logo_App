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
    public Button stopExerBtn;

    //public string[] miniGameScenes= new string[3] { "MiniGame1", "MiniGame2", "MiniGame3" };
    public string[] miniGameScenes= new string[1] { "MiniGame1"};
    int sceneToLoad = 0;
    public bool wasLastMiniGame = false;

    public bool isExercTimerActive = false;
    public float exerciseTimerCountdown = 10f;
    public float resetExerciseTimerCountdown = 10f;

    public bool isAttentionActive = false;
    public float timeFoAttention = 5f;

    public bool applicationStart = false;

    public GameObject background;

    public GameObject mainCamera;
    public EventSystem mainEvent;

    public Camera miniGameCamera;
    public EventSystem miniGameEvent;
    public AudioListener miniGameAudioListener;

    public Button fillButton;          
    public Image fillButtonImage;           

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
        if(!isAttentionActive && exerciseTimerCountdown < timeFoAttention )
        {
            isAttentionActive = true;
            StartAttention();
        }

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
        isAttentionActive = false;
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
        stopExerBtn.gameObject.SetActive(true);
    }


    void StartAttention()
    {
        fillButtonImage.type = Image.Type.Filled;
        fillButtonImage.fillMethod = Image.FillMethod.Vertical;
        fillButtonImage.fillAmount = 0f;  // Start with no fill
        fillButtonImage.color = Color.green;

        // Start the filling effect
        StartCoroutine(FillButton(timeFoAttention));
    }

    IEnumerator FillButton(float fillDuration)
    {
        float timeElapsed = 0f;

        while (timeElapsed < fillDuration)
        {
            // Calculate the fill amount based on the time elapsed
            float fillAmount = timeElapsed / fillDuration;
            fillButtonImage.fillAmount = fillAmount;

            // Gradually change color from green to red based on fill progress
            fillButtonImage.color = Color.Lerp(Color.grey, Color.green, fillAmount);

            // Wait for the next frame
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // Ensure the button is fully filled and red at the end
        fillButtonImage.fillAmount = 1f;
        fillButtonImage.color = Color.grey;
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