using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NameCreator : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    TextMeshProUGUI text;
   

    public static event Action<string> OnImageRequest;
    public static void RaiseExerciseName(string exerciseName) => OnImageRequest?.Invoke(exerciseName);
    private void OnEnable()
    {
        OnImageRequest += ChangeExerciseName;
       


    }
    private void OnDisable()
    {
        OnImageRequest -= ChangeExerciseName;


    }

    public void ChangeExerciseName(string exerciseName)
    {
        text.text = exerciseName.ToLower();
    }
}
