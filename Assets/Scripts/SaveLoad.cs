using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering;

public class SaveData
{
    public Color[] pixelColors;
}

public class SaveLoad : MonoBehaviour
{
    [SerializeField] UIController uiController;
    [SerializeField] DrawingRenderer drawingRenderer;
    private static string path = "/SaveData/";
    private static string fileName = "SaveData.txt";

    // Singleton creation
    public static SaveLoad Instance { get; private set; }

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        uiController.OnSaveButtonClicked += Save;
        uiController.OnLoadButtonClicked += Load;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // path = Application.persistentDataPath + "/testImage.png";
        Debug.Log(path);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Save()
    {
        string directory = Application.persistentDataPath + path;
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        string json = JsonUtility.ToJson(drawingRenderer.data);
        File.WriteAllText(directory + fileName, json);
        Debug.Log("Woo yeah we savin");
        // drawingRenderer.data;
    }

    public void Load()
    {
        string loadPath = Application.persistentDataPath + path + fileName;

        if (File.Exists(loadPath))
        {
            string json = File.ReadAllText(loadPath);
            drawingRenderer.data = JsonUtility.FromJson<SaveData>(json);
            drawingRenderer.UpdateRenderTexture();
        }
        else { Debug.Log("woops there's nothing there man"); }


        Debug.Log("Wuh-oh we're loadin");
        // drawingRenderer.data;
    }

    private void SetPixels()
    {

    }

    /*
     * 
    IEnumerator RecordFrame()
    {
        yield return new WaitForEndOfFrame();
        var texture = ScreenCapture.CaptureScreenshotAsTexture();
        // do something with texture
        SaveImage(texture);

        // cleanup
        Object.Destroy(texture);
    }

    public void SaveImage(Texture2D texture)
    {
        Debug.Log("Saving");
        byte[] data = ImageConversion.EncodeToPNG(texture);
        File.WriteAllBytes(path, data);
    }

    public Texture2D LoadImage()
    {
        byte[] data = File.ReadAllBytes(path);
        Texture2D texture = new Texture2D(1, 1); // contents will be replaced

        bool success = ImageConversion.LoadImage(texture, data, false);
        if (!success)
        {
            Debug.Log("Failed to decode image from \"" + path + "\"");
            return null;
        }
        return texture;
    }
    */
}
