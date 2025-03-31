using SFB;
using System.IO;
using UnityEditor;
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
    }

    public void Load()
    {
        string loadPath = Application.persistentDataPath + path + fileName; // Path.combine

        if (File.Exists(loadPath))
        {
            string json = File.ReadAllText(loadPath);
            drawingRenderer.data = JsonUtility.FromJson<SaveData>(json);
            drawingRenderer.UpdateRenderTexture();
        }
        else { Debug.Log("Nothing to load"); }
    }

    public void ExportToPng()
    {
        var savePath = StandaloneFileBrowser.SaveFilePanel(
        "Save texture as PNG",
            "",
            "image.png",
            "png");
        byte[] bytes = drawingRenderer.drawingTexture.EncodeToPNG();
        File.WriteAllBytes(savePath, bytes);
    }
}
