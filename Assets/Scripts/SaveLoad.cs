using System.IO;
using UnityEngine;

public class SaveData
{
    public Color[] pixelColors;
}

public class SaveLoad : MonoBehaviour
{
    [SerializeField] UIController uiController;
    [SerializeField] DrawingRenderer drawingRenderer;
    private static string path = "/SaveData/";
    private static string fileName = "SaveData.json";
    private static string exportName = "PaintImg.png";

    private void Awake()
    {
        uiController.OnSaveButtonClicked += Save;
        uiController.OnLoadButtonClicked += Load;
        uiController.OnExportButtonClicked += ExportToPng;
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
    }

    public void ExportToPng()
    {
        string directory = Application.persistentDataPath + path;
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        byte[] bytes = drawingRenderer.drawingTexture.EncodeToPNG();
        File.WriteAllBytes(directory + exportName, bytes);
        Debug.Log("ex-ex-ex-exporting");
    }
}
