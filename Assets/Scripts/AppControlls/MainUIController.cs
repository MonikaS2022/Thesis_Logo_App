using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;  // To load scenes

public class MainUIController : MonoBehaviour
{
    
    public Button startButton;
    public GameObject startBtnCanvas;

    public bool isTimerActive = false;
    private float timer = 0f;  // Timer to track app run time
    public bool applicationStart = false;
    public bool applicationEnd = false;
    public float endApplicationTime = 30f;
    public float gameStartTime = 10f;  
    public float gameEndedTime = 20f;  
    
    private bool miniGameStarted = false;  
    private bool miniGameAttention = false; 
    private bool miniGameEnded = false; 
  
    public Button buttonAttention;

    public GameObject mainCamera;
    public GameObject mainCanvas;
    public EventSystem mainEvent;

    public Camera miniGameCamera;
    public EventSystem miniGameEvent;
    public AudioListener miniGameAudioListener;


    void Start()
    {
        startButton.onClick.AddListener(StartTimer);

    }
    void Update()
    {
        

        // Track elapsed time
        if (isTimerActive)
       {
            timer += Time.deltaTime;

       }
        if(applicationStart && timer >= endApplicationTime && !applicationEnd)
        {
            applicationEnd = true;
            EndApplication();
        }
        
       
        if (timer >= gameStartTime-5f && !miniGameAttention)
        {
            miniGameAttention = true;
            StartAttention();
        }
        if (miniGameAttention && ((Mathf.FloorToInt(timer) % 1) == 0) && !miniGameStarted)
        {
            buttonAttention.GetComponent<Image>().color = new Color(Random.value, Random.value, Random.value);
        }

        if (timer >= gameStartTime && !miniGameStarted)
        {
            miniGameStarted = true; 
            StartMiniGame();
        }
        if (timer >= gameEndedTime && !miniGameEnded)
        {
            miniGameEnded = true;
            EndMiniGame();
        }

        
        
    }

    void StartTimer()
    {
        isTimerActive = true;
        startButton.interactable = false;
        applicationStart = true;
        startBtnCanvas.gameObject.SetActive(false);
        Debug.Log("timer activated");
    }

    void StartAttention()
    {
        Debug.Log("Starting attention");
        buttonAttention.GetComponent<Image>().color = Color.yellow;
    }

    void StartMiniGame()
    {
           
        Debug.Log("Starting Mini-Game Scene...");
        mainEvent.gameObject.SetActive(false);
        //mainListener ???
        SceneManager.LoadScene("MiniGame1", LoadSceneMode.Additive);
        StartCoroutine(AssignMiniGameAudioListenerandEvent());

        mainCamera.gameObject.SetActive(false);
        mainCanvas.gameObject.SetActive(false);
    }

    private IEnumerator AssignMiniGameAudioListenerandEvent()
    {
        Debug.Log("async assigminigamelistenerandEvent...");

        while (!SceneManager.GetSceneByName("MiniGame1").isLoaded)
        {
            yield return null;
        }
        miniGameCamera = GameObject.Find("MiniGame1Camera").GetComponent<Camera>();
        miniGameAudioListener = miniGameCamera.GetComponent<AudioListener>();
        miniGameEvent = GameObject.Find("MiniEventSystem").GetComponent<EventSystem>();

        if (miniGameAudioListener != null)
        {
            miniGameAudioListener.gameObject.SetActive(true);
            miniGameEvent.gameObject.SetActive(true);
            Debug.Log("Mini-Game Audio Listener and Event assigned and active.");
        }
    }


    void EndMiniGame()
    {
        
        Debug.Log("endin minigame...");
        mainCamera.gameObject.SetActive(true);
        mainCanvas.gameObject.SetActive(true);
        StartCoroutine(UnloadMiniGameSceneAsync());
        miniGameAudioListener.gameObject.SetActive(false);
        miniGameEvent.gameObject.SetActive(false);
        mainEvent.gameObject.SetActive(true);
        buttonAttention.GetComponent<Image>().color = Color.white;

    }

    private IEnumerator UnloadMiniGameSceneAsync()
    {
        Debug.Log("async unloadminigame...");
        AsyncOperation unloadOperation = SceneManager.UnloadSceneAsync("MiniGame1");
        while(!unloadOperation.isDone)
        {
            yield return null;
        }
        Debug.Log("Mini-Game Scene has been unloaded.");
    }

    void EndApplication()
    {
        string buttonText = "END";
        startBtnCanvas.GetComponentInChildren<TextMeshProUGUI>().text = buttonText;

        startBtnCanvas.gameObject.SetActive(true);
        Debug.Log("app ended");
    }




}