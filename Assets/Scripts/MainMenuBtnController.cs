using UnityEngine;

public class MainMenuBtnController : MonoBehaviour
{
    public string JsonFileName; //write in editor per btn
    public string ExerciseName; //write in editor per btn

    public void OnClick()
    {
        RunTimeImageChanger.RaiseImageClear();
        RunTimeImageSpawner.RaiseImageRequest(JsonFileName);
        NameCreator.RaiseExerciseName(ExerciseName);
        SubmenuSetActive.RaiseChangeColor();
       
    }
}
