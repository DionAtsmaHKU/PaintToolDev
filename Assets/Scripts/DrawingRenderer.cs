using UnityEngine;

// Implemented from video "How to draw Pixel Art in Unity 6" by Sunny Valley Studio, https://youtu.be/_Y0GqcF5qqM
// Minor formatting changes applied to follow style guide.
public class DrawingRenderer : MonoBehaviour
{
    [SerializeField] RenderTexture renderTexture;
    private Texture2D drawingTexture;
    public SaveData data = new SaveData();
    private int gridLengthWidth = 16;

    private void Start()
    {
        //Prepare the drawing texture
        drawingTexture = new(renderTexture.width, renderTexture.height, TextureFormat.RGBA32, false);
        drawingTexture.filterMode = FilterMode.Point;
        data.pixelColors = new Color[renderTexture.width * renderTexture.height];
        ClearCanvas();
    }

    public void DrawOnTexture(Vector2 _normalizedPixelPosition, Color _color)
    {
        int pixelIndex = ConvertPosToPixel(_normalizedPixelPosition);

        if (data.pixelColors[pixelIndex] == _color)
            return;
        data.pixelColors[pixelIndex] = _color;
        UpdateRenderTexture();
    }

    public void EraseOnTexture(Vector2 _normalizedPixelPosition)
    {
        int pixelIndex = ConvertPosToPixel(_normalizedPixelPosition);

        if (data.pixelColors[pixelIndex] == Color.clear)
            return;
        data.pixelColors[pixelIndex] = Color.clear;
        UpdateRenderTexture();
    }

    public void FillOnTexture(Vector2 _normalizedPixelPosition, Color _color)
    {
        int pixelIndex = ConvertPosToPixel(_normalizedPixelPosition);
        int[] directions = new int[] { 1, -1, -gridLengthWidth, gridLengthWidth };

        Color oldColor = data.pixelColors[pixelIndex];

        DfsFloodFill(pixelIndex, _color, oldColor, directions);
        UpdateRenderTexture();
    }

    private void DfsFloodFill(int _pixelIndex, Color _newColor, Color _oldColor, int[] directions)
    {
        if (CheckFill(_pixelIndex, _newColor, _oldColor))
            return;

        data.pixelColors[_pixelIndex] = _newColor;
        Debug.Log("filled pixel: " + _pixelIndex);
        for (int i = 0; i < directions.Length; i++)
        {
            DfsFloodFill(_pixelIndex + directions[i], _newColor, _oldColor, directions);
        }
    }

    private bool CheckFill(int _pixelIndex, Color _newColor, Color _oldColor)
    {
        if (_pixelIndex < 0 || _pixelIndex > 255)
        {
            return true;
        }
        if (data.pixelColors[_pixelIndex] == _newColor || data.pixelColors[_pixelIndex] != _oldColor)
        {
            return true;
        }
        return false;
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

    // Convert normalized position to RenderTexture position
    private int ConvertPosToPixel(Vector2 _normalizedPixelPosition)
    {
        Vector2Int pixelPositionOnTexture = new((int)(_normalizedPixelPosition.x * (renderTexture.width)),
            (int)(_normalizedPixelPosition.y * (renderTexture.height)));
        int pixelIndex = pixelPositionOnTexture.y * renderTexture.width + pixelPositionOnTexture.x;
        return pixelIndex;
    }
}
