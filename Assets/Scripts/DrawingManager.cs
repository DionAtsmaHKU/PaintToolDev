using UnityEngine;

public enum DrawMode
{
    None = 0,
    Brush = 1,
    Erase = 2,
    Fill = 3
}

// Implemented from video "How to draw Pixel Art in Unity 6" by Sunny Valley Studio, https://youtu.be/_Y0GqcF5qqM
// Minor formatting changes applied to follow style guide.
public class DrawingManager : MonoBehaviour
{
    [SerializeField] UIController uiController;
    [SerializeField] DrawingRenderer drawingRenderer;
    public DrawMode drawMode = DrawMode.Brush;

    //Connect events
    private void Start()
    {
        uiController.OnPointerDown += StartDrawing;
        uiController.OnPointerReleased += StopDrawing;
        uiController.OnClearButtonClicked += ClearDrawing;
        uiController.OnPointerOut += StopDrawing;
        uiController.OnPaintButtonClicked += EnterPaintMode;
        uiController.OnEraseButtonClicked += EnterEraseMode;
        uiController.OnFillButtonClicked += EnterFillMode;
    }

    private void ClearDrawing()
    {
        drawingRenderer.ClearCanvas();
    }

    //Manage drawing process
    private void StopDrawing()
    {
        uiController.OnPointerMoved -= Draw;
    }

    private void Draw(Vector2 _normalizedPixelPosition)
    {
        switch (drawMode)
        {
            case DrawMode.None:
                Debug.Log("No draw mode?? HUH??");
                break;

            case DrawMode.Brush:
                drawingRenderer.DrawOnTexture(_normalizedPixelPosition, Color.yellow);
                break;

            case DrawMode.Erase:
                drawingRenderer.EraseOnTexture(_normalizedPixelPosition);
                break;

            case DrawMode.Fill:
                drawingRenderer.FillOnTexture(_normalizedPixelPosition, Color.green);
                break;
        }
    }

    private void EnterPaintMode()
    {
        drawMode = DrawMode.Brush;
    }

    private void EnterEraseMode()
    {
        drawMode = DrawMode.Erase;
    }

    private void EnterFillMode()
    {
        drawMode = DrawMode.Fill;
    }

    private void StartDrawing(Vector2 _normalizedPixelPosition)
    {
        Draw(_normalizedPixelPosition);
        uiController.OnPointerMoved += Draw;
    }
}
