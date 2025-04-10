using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextCreator: MonoBehaviour 
{
    [SerializeField]
    private TextMeshProUGUI text;

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
    }


}
