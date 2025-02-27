using System.Collections;
using System.IO;
using UnityEngine;

public class SaveData
{
    public Color[] pixelColors;
}


public class SaveLoad : MonoBehaviour
{
    private string path;

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
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        path = Application.persistentDataPath + "/testImage.png";
        Debug.Log(path);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            StartCoroutine(RecordFrame());
        }
    }

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
}
