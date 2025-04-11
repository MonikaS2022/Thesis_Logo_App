using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuPartsListManager : MonoBehaviour
{
    [SerializeField]
    GameObject panel;

    List<Headers> entries = new List<Headers>();

    public static event Action OnDeactivateMenuRequest;
    public static void RaiseDeactivateRequest() => OnDeactivateMenuRequest?.Invoke();


    private void OnEnable()
    {
        OnDeactivateMenuRequest += Deactivate;

    }
    private void OnDisable()
    {
        OnDeactivateMenuRequest -= Deactivate;

    }

    private void Deactivate()
    {
        entries = panel.GetComponentsInChildren<Headers>().ToList();
        foreach (Headers e in entries)
        {
            e.isActive = false;
        }
    }
}
