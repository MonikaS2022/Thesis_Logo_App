using System;
using UnityEngine;
using UnityEngine.UI;

public class RunTimeImageChanger : MonoBehaviour
{
    [SerializeField]
    private Image picture;

    public static event Action<Sprite> OnImageRequest;
    public static void RaiseImageRequest(Sprite pictureFileName) => OnImageRequest?.Invoke(pictureFileName);

    public static event Action OnImageClear;
    public static void RaiseImageClear() => OnImageClear?.Invoke();

    private void OnEnable()
    {
        OnImageRequest += SpriteChanger;
        OnImageClear += ImageClearer;
    }
    private void OnDisable()
    {
        OnImageRequest -= SpriteChanger;
        OnImageClear -= ImageClearer;
    }

    void SpriteChanger(Sprite pictureFileName)
    {
        Color newColor = picture.color;
        newColor.a = 1;
        picture.color = newColor;
        picture.sprite = pictureFileName;
    }

    void ImageClearer()
    {
        picture.sprite = null;
        Color newColor = picture.color;
        newColor.a = 0.2f;
        picture.color = newColor;
    }



}
