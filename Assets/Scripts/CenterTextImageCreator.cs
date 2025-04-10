using UnityEngine;
using UnityEngine.UI;

public class CenterTextImageCreator : MonoBehaviour
{
    [SerializeField]
    private Image imageText;
    private Sprite spritePicture;

    public void SetImageText(Sprite sprite)
    {
        imageText.sprite = sprite;
    }

    public void OnClick()
    {
        RunTimeImageChanger.RaiseImageRequest(spritePicture);
    }

    public void SetSpritePicture(Sprite sprite)
    {
        spritePicture = sprite;
    }

}
