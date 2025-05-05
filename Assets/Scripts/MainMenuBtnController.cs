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
            text.color = new Color(0.4901961f * 0.8f, 0.5960785f * 0.8f, 0.4745098f * 0.8f);
            text.fontStyle = FontStyles.Bold;
        }
        else
        {
            text.color = Color.black;
            text.fontStyle = FontStyles.Normal;
        }
    }
}
