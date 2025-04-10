using UnityEngine;
using System;


public class RunTimeImageSpawner : MonoBehaviour
{
    [SerializeField]
    private CenterTextImageCreator prefabOfEntry;
    [SerializeField] 
    private RectTransform contentPanel;
    [SerializeField]
    private TextCreator prefabOfText;
    [SerializeField]
    private RectTransform textPanel;
    [SerializeField]

  


    public static event Action<string> OnImageRequest;
    public static void RaiseImageRequest(string jsonFileName ) => OnImageRequest?.Invoke( jsonFileName );
  
   

    private void OnEnable()
    {
       
        OnImageRequest += FileLoaderText;
       
        
    }
    private void OnDisable()
    {
     
        OnImageRequest -= FileLoaderText;
      

    }




    private void FileLoaderText(string jsonFileName)
    {
        foreach (Transform child in textPanel)
        {
            Destroy(child.gameObject);
        }


        TextAsset jsonFile = Resources.Load<TextAsset>(jsonFileName);
        if (jsonFile == null)
        {
            Debug.LogError("JSON file not found: " + jsonFileName);
            return;
        }
        EntryListWrapper data = JsonUtility.FromJson<EntryListWrapper>(jsonFile.text);
        if (data.entries == null || data.entries.Length == 0)
        {
            Debug.LogWarning("No entrie found in JSON");
            return;
        }

        foreach (var entry in data.entries)
        {
            CreateEntryText(entry);
        }
    }

    void CreateEntryText(Entry entry)
    {
     
        Sprite pictureSprite = Resources.Load<Sprite>("Pictures/" + entry.imageName);
        if (pictureSprite == null)
        {
            Debug.LogWarning("Image not found: " + entry.imageName);
            return;
        }
        TextCreator entryObject = Instantiate(prefabOfText, textPanel);
        entryObject.SetText(entry.textName);
        entryObject.SetPicture(pictureSprite);

    }

  }


  












