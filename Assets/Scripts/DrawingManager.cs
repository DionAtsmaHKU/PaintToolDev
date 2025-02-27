using UnityEngine;

public class DrawingManager : MonoBehaviour
{
    //Get references to UIController and DrawingRenderer
    [SerializeField]
    private UIController m_uiController;
    [SerializeField]
    private DrawingRenderer m_drawingRenderer;

    //Connect events
    private void Start()
    {
        m_uiController.OnPointerDown += StartDrawing;
        m_uiController.OnPointerReleased += StopDrawing;
        m_uiController.OnClearButtonClicked += ClearDrawing;
        m_uiController.OnPointerOut += StopDrawing;
    }

    private void ClearDrawing()
    {
        m_drawingRenderer.ClearCanvas();
    }

    //Manage drawing process
    private void StopDrawing()
    {
        m_uiController.OnPointerMoved -= Draw;
    }

    private void Draw(Vector2 normalizedPixelPosition)
    {
        m_drawingRenderer.DrawOnTexture(normalizedPixelPosition, Color.yellow);
    }

    private void StartDrawing(Vector2 normalizedPixelPosition)
    {
        Draw(normalizedPixelPosition);
        m_uiController.OnPointerMoved += Draw;
    }
}
