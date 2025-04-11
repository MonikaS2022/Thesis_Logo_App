using TMPro;
using UnityEngine;

public class MainMenuBtnController : MonoBehaviour
{
    public string JsonFileName; //write in editor per btn
    public string ExerciseName; //write in editor per btn
    public TextMeshProUGUI text;
    public bool isActive;

    public void OnClick()
    {
        RunTimeImageChanger.RaiseImageClear();
        RunTimeImageSpawner.RaiseImageRequest(JsonFileName);
        NameCreator.RaiseExerciseName(ExerciseName);
        MainMenuListManager.RaiseDeactivateRequest();
        isActive = true;
        MainMenuPartsListManager.RaiseDeactivateRequest();
        Headers.RaiseSetActiveRequest(JsonFileName);

    }

    void Update()
    {
        if (isActive)
        {
            text.color = Color.red;
        }
        else
        {
            text.color = Color.black;
        }
    }
}
