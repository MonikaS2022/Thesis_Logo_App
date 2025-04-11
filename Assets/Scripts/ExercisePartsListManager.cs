using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ExercisePartsListManager : MonoBehaviour
{

    [SerializeField]
    GameObject panel;

    List<TextCreator> entries = new List<TextCreator>();

    public static event Action OnDeactivateRequest;
    public static void RaiseDeactivateRequest() => OnDeactivateRequest?.Invoke();


    private void OnEnable()
    {
        OnDeactivateRequest += Deactivate;

     
    }
    private void OnDisable()
    {
        OnDeactivateRequest -= Deactivate;
 
      
    }
      

    private void Deactivate()
    {
        entries = panel.GetComponentsInChildren<TextCreator>().ToList();
        foreach (TextCreator e in entries)
        {
            e.isActive = false;
        }
    }

}
