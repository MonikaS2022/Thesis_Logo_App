using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextCreator: MonoBehaviour 
{
    [SerializeField]
    private TextMeshProUGUI text;

    public bool isActive;
    Sprite picSprite;


 
    public void SetText(string inputText)
    {
        text.text = inputText;
    }

    public void SetPicture(Sprite picture)
    {
        picSprite = picture;
    }

    public void OnClick()
    {
        RunTimeImageChanger.RaiseImageRequest(picSprite);
        ExercisePartsListManager.RaiseDeactivateRequest();
        isActive = true;
    }

    void Update()
    {
        if (isActive)
        {
            text.color = Color.black;
        }
        else
        {
            text.color = Color.white;
        }
    }


}
