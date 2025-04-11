using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Headers : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;
    [SerializeField]
    private string id;

    public bool isActive;

    public static event Action<string> OnSetActiveRequest;
    public static void RaiseSetActiveRequest(string JsonFileName) => OnSetActiveRequest?.Invoke(JsonFileName);

    private void OnEnable()
    {
       OnSetActiveRequest += SetActive;

    }
    private void OnDisable()
    {
   
        OnSetActiveRequest -= SetActive;


    }
   

    public void SetActive(string JSONFileName)
    {
        if (JSONFileName[1].ToString() == id)
        {
            isActive = true;
        }
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
