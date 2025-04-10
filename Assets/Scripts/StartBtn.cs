using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBtn : MonoBehaviour
{
    [SerializeField]
    private GameObject mainExercisePanel;
  
    public void OnClick()
    {
        mainExercisePanel.SetActive(true);

    }
}
