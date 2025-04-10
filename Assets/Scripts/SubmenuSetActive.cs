using UnityEngine;
using TMPro;
using System;

public class SubmenuSetActive : MonoBehaviour
{
    public bool active = false;
    [SerializeField]
    GameObject menu;
    [SerializeField]
    TextMeshProUGUI textComponent;

    public static event Action OnChangeColor;
    public static void RaiseChangeColor () => OnChangeColor?.Invoke();
    private void OnEnable()
    {

        OnChangeColor += ChangeColor;


    }
    private void OnDisable()
    {

        OnChangeColor -= ChangeColor;


    }

    public void OpenOrClose()
    {
        active = !active;
        menu.SetActive(active);

        if(active)
        {
            textComponent.fontStyle = FontStyles.Bold;
        }
        if (!active)
        {
            textComponent.fontStyle = FontStyles.Normal;
        }
    }

    public void ChangeColor()
    {
     
        textComponent.color = Color.red;
    }

  
}
