using UnityEngine;

// Implemented from video "How to draw Pixel Art in Unity 6" by Sunny Valley Studio, https://youtu.be/_Y0GqcF5qqM
// Minor formatting changes applied to follow style guide.
public class DrawingRenderer : MonoBehaviour
{
    [SerializeField] RenderTexture renderTexture;
    private Texture2D drawingTexture;
    public SaveData data = new SaveData();

    private void Start()
    {   
        //Prepare the drawing texture
        drawingTexture = new(renderTexture.width, renderTexture.height, TextureFormat.RGBA32, false);
        drawingTexture.filterMode = FilterMode.Point;
        data.pixelColors = new Color[renderTexture.width * renderTexture.height];
        ClearCanvas();
    }

    public void DrawOnTexture(Vector2 normalizedPixelPosition, Color color)
    {
        //convert normalized position to RenderTexture position
        Vector2Int pixelPositionOnTexture = new((int)(normalizedPixelPosition.x * (renderTexture.width)),
            (int)(normalizedPixelPosition.y * (renderTexture.height)));

        // Calculate 1D array index (row-major order)
        int pixelIndex = pixelPositionOnTexture.y * renderTexture.width + pixelPositionOnTexture.x;

        if (data.pixelColors[pixelIndex] == color)
            return;
        data.pixelColors[pixelIndex] = color;
        UpdateRenderTexture();
    }

    public void ClearCanvas()
    {
        for (int i = 0; i < data.pixelColors.Length; i++)
        {
            data.pixelColors[i] = Color.black;
        }
        UpdateRenderTexture();
    }

    public void UpdateRenderTexture()
    { 
        RenderTexture currentActiveRT = RenderTexture.active;
        RenderTexture.active = renderTexture;

        drawingTexture.SetPixels(data.pixelColors);
        drawingTexture.Apply();

        Graphics.Blit(drawingTexture, renderTexture);
        RenderTexture.active = currentActiveRT;
    }
}
