using UnityEngine;

// Implemented from video "How to draw Pixel Art in Unity 6" by Sunny Valley Studio, https://youtu.be/_Y0GqcF5qqM
// Minor formatting changes applied to follow style guide.
public class DrawingManager : MonoBehaviour
{
    [SerializeField] UIController uiController;
    [SerializeField] DrawingRenderer drawingRenderer;

    //Connect events
    private void Start()
    {
        uiController.OnPointerDown += StartDrawing;
        uiController.OnPointerReleased += StopDrawing;
        uiController.OnClearButtonClicked += ClearDrawing;
        uiController.OnPointerOut += StopDrawing;
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
        drawingRenderer.DrawOnTexture(_normalizedPixelPosition, Color.yellow);
    }

    private void StartDrawing(Vector2 _normalizedPixelPosition)
    {
        Draw(_normalizedPixelPosition);
        uiController.OnPointerMoved += Draw;
    }
}
