using System;
using UnityEngine;
using UnityEngine.UIElements;

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
    private Color currentColor = Color.black;
    public PaletteList paletteList;
    private ColorPalette currentPalette;

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
        uiController.OnColorSelected += SelectColor;
        uiController.OnNextPaletteClicked += SwapPalette;
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
                drawingRenderer.DrawOnTexture(_normalizedPixelPosition, currentColor);
                break;

            case DrawMode.Erase:
                drawingRenderer.EraseOnTexture(_normalizedPixelPosition);
                break;

            case DrawMode.Fill:
                drawingRenderer.FillOnTexture(_normalizedPixelPosition, currentColor);
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

    private void SelectColor(VisualElement _button)
    {
        currentColor = _button.resolvedStyle.backgroundColor;
    }

    private void StartDrawing(Vector2 _normalizedPixelPosition)
    {
        Draw(_normalizedPixelPosition);
        uiController.OnPointerMoved += Draw;
    }

    private void SwapPalette(VisualElement[] _buttons)
    {
        currentPalette = paletteList.palettes[1];
        UpdateColors(_buttons);
    }

    private void UpdateColors(VisualElement[] _buttons)
    {
        for (int i = 0; i < _buttons.Length; i++)
        {
            Debug.Log("Setting button " + _buttons[i] + " to colour: " + currentPalette.colors[i]);
            _buttons[i].style.backgroundColor = currentPalette.colors[i];
        }
    }
}

[Serializable]
public class PaletteList
{
    public ColorPalette[] palettes;
}


[Serializable]
public class ColorPalette
{
    public Color[] colors;
}
