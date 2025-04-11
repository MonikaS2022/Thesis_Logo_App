using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuListManager : MonoBehaviour
{
    [SerializeField]
    GameObject panel;

    List<MainMenuBtnController> entries = new List<MainMenuBtnController>();

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
        entries = panel.GetComponentsInChildren<MainMenuBtnController>().ToList();
        foreach (MainMenuBtnController e in entries)
        {
            e.isActive = false;
        }
    }
}
