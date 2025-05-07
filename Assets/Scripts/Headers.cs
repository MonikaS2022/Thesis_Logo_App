using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Headers : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;
    [SerializeField]
    private Image btnImage;
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
            text.color = new Color(0.4901961f * 0.8f, 0.5960785f * 0.8f, 0.4745098f * 0.8f);
            text.fontStyle = FontStyles.Bold;
            btnImage.color = new Color(0.6419708f / 0.8f, 0.745283f / 0.8f, 0.5870861f / 0.8f); ;


        }
        else
        {
            text.color = Color.black;
            text.fontStyle = FontStyles.Normal;
            btnImage.color = Color.white;
        }
    }
}
