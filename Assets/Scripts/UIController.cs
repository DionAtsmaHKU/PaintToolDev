using System;
using UnityEngine;
using UnityEngine.UIElements;

// Base implemented from video "How to draw Pixel Art in Unity 6" by Sunny Valley Studio, https://youtu.be/_Y0GqcF5qqM
// Minor formatting changes applied to follow style guide, and own buttons added.

public class UIController : MonoBehaviour
{
    private UIDocument uiDocument;
    private VisualElement canvas, clearButton, saveButton, loadButton, exportButton, paintButton, eraseButton, fillButton, nextPalette;
    private VisualElement color1, color2, color3, color4, color5, color6, color7, color8, color9;
    private VisualElement[] colorButtons;

    public event Action<Vector2> OnPointerDown, OnPointerMoved;
    public event Action OnClearButtonClicked, OnSaveButtonClicked, OnLoadButtonClicked, OnExportButtonClicked, OnPaintButtonClicked, OnEraseButtonClicked, OnFillButtonClicked, OnPointerReleased, OnPointerOut;
    public event Action<VisualElement> OnColorSelected;
    public event Action<VisualElement[]> OnNextPaletteClicked;

    private void Awake()
    {
        uiDocument = GetComponent<UIDocument>();
        canvas = uiDocument.rootVisualElement.Q<VisualElement>("Canvas");
        clearButton = uiDocument.rootVisualElement.Q<VisualElement>("Clear");
        saveButton = uiDocument.rootVisualElement.Q<VisualElement>("Save");
        loadButton = uiDocument.rootVisualElement.Q<VisualElement>("Load");
        exportButton = uiDocument.rootVisualElement.Q<VisualElement>("Export");
        paintButton = uiDocument.rootVisualElement.Q<VisualElement>("Paint");
        eraseButton = uiDocument.rootVisualElement.Q<VisualElement>("Erase");
        fillButton = uiDocument.rootVisualElement.Q<VisualElement>("Fill");
        colorButtons = new VisualElement[] { color1, color2, color3, color4, color5, color6, color7, color8, color9 };
        for (int i = 0; i < colorButtons.Length; i++)
        {
            colorButtons[i] = uiDocument.rootVisualElement.Q<VisualElement>(("Color" + (i + 1)));
        }
        nextPalette = uiDocument.rootVisualElement.Q<VisualElement>("NextPalette");

        canvas.RegisterCallback<PointerDownEvent>(OnPointerDownEvent);
        canvas.RegisterCallback<PointerMoveEvent>(OnPointerMovedEvent);
        canvas.RegisterCallback<PointerUpEvent>(OnPointerReleasedEvent);
        canvas.RegisterCallback<PointerOutEvent>(OnPointerOutEvent);
        clearButton.RegisterCallback<ClickEvent>(OnClearButtonClickedEvent);
        saveButton.RegisterCallback<ClickEvent>(OnSaveButtonClickedEvent);
        loadButton.RegisterCallback<ClickEvent>(OnLoadButtonClickedEvent);
        exportButton.RegisterCallback<ClickEvent>(OnExportButtonClickedEvent);
        paintButton.RegisterCallback<ClickEvent>(OnPaintButtonClickedEvent);
        eraseButton.RegisterCallback<ClickEvent>(OnEraseButtonClickedEvent);
        fillButton.RegisterCallback<ClickEvent>(OnFillButtonClickedEvent);
        for (int i = 0; i < colorButtons.Length; i++)
        {
            colorButtons[i].RegisterCallback<ClickEvent, VisualElement>(OnColorSelectedEvent, colorButtons[i]);
        }
        nextPalette.RegisterCallback<ClickEvent>(OnNextPaletteClickedEvent);
    }

    private void OnColorSelectedEvent(ClickEvent _evt, VisualElement _button)
    {
        OnColorSelected.Invoke(_button);
    }

    private void OnNextPaletteClickedEvent(ClickEvent _evt)
    {
        OnNextPaletteClicked.Invoke(colorButtons);
    }

    private void Start()
    {
        canvas.style.width = Screen.height;
        canvas.style.height = Screen.height;
    }

    private void OnClearButtonClickedEvent(ClickEvent _evt)
    {
        OnClearButtonClicked?.Invoke();
    }

    private void OnLoadButtonClickedEvent(ClickEvent _evt)
    {
        OnLoadButtonClicked?.Invoke();
    }

    private void OnSaveButtonClickedEvent(ClickEvent _evt)
    {
        OnSaveButtonClicked?.Invoke();
    }

    private void OnExportButtonClickedEvent(ClickEvent _evt)
    {
        OnExportButtonClicked?.Invoke();
    }

    private void OnPaintButtonClickedEvent(ClickEvent _evt)
    {
        OnPaintButtonClicked?.Invoke();
    }
    private void OnEraseButtonClickedEvent(ClickEvent _evt)
    {
        OnEraseButtonClicked?.Invoke();
    }
    private void OnFillButtonClickedEvent(ClickEvent _evt)
    {
        OnFillButtonClicked?.Invoke();
    }

    private void OnPointerOutEvent(PointerOutEvent _evt)
    {
        OnPointerOut?.Invoke();
    }

    private void OnPointerReleasedEvent(PointerUpEvent _evt)
    {
        Vector2 normalizedPosition = ProcessPosition(_evt.localPosition);
        OnPointerReleased?.Invoke();
    }

    private void OnPointerMovedEvent(PointerMoveEvent _evt)
    {
        Vector2 normalizedPosition = ProcessPosition(_evt.localPosition);
        OnPointerMoved?.Invoke(normalizedPosition);
    }

    private void OnPointerDownEvent(PointerDownEvent _evt)
    {
        Vector2 normalizedPosition = ProcessPosition(_evt.localPosition);
        OnPointerDown?.Invoke(normalizedPosition);
    }

    //Flips Y axis to be pointing UP
    private Vector2 ProcessPosition(Vector2 _localMousePosition)
    {
        Vector2 normalizedPosition = NormalizePixelPosition(_localMousePosition);
        normalizedPosition.y = 1 - normalizedPosition.y;
        return normalizedPosition;
    }

    // Normalizes pixel position to the 0-1 range based on Ui element width and height
    private Vector2 NormalizePixelPosition(Vector2 _pixelPosition)
    {
        float normalizedX = Mathf.InverseLerp(0, canvas.layout.width, _pixelPosition.x);
        float normalizedY = Mathf.InverseLerp(0, canvas.layout.height, _pixelPosition.y);
        return new(normalizedX, normalizedY);
    }
}
