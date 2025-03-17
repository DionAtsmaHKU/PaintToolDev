using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

// Base implemented from video "How to draw Pixel Art in Unity 6" by Sunny Valley Studio, https://youtu.be/_Y0GqcF5qqM
// Minor formatting changes applied to follow style guide, and own buttons added.

public class UIController : MonoBehaviour
{
    private UIDocument uiDocument;
    private VisualElement canvas, clearButton, saveButton, loadButton, exportButton, paintButton, eraseButton, fillButton;
    private VisualElement color1, color2, color3, color4, color5, color6, color7, color8, color9;

    public event Action<Vector2> OnPointerDown, OnPointerMoved;
    public event Action OnClearButtonClicked, OnSaveButtonClicked, OnLoadButtonClicked, OnExportButtonClicked, OnPaintButtonClicked, OnEraseButtonClicked, OnFillButtonClicked, OnPointerReleased, OnPointerOut;
    public event Action<VisualElement> OnColorSelected;

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
        color1 = uiDocument.rootVisualElement.Q<VisualElement>("Color1");
        color2 = uiDocument.rootVisualElement.Q<VisualElement>("Color2");
        color3 = uiDocument.rootVisualElement.Q<VisualElement>("Color3");
        color4 = uiDocument.rootVisualElement.Q<VisualElement>("Color4");
        color5 = uiDocument.rootVisualElement.Q<VisualElement>("Color5");
        color6 = uiDocument.rootVisualElement.Q<VisualElement>("Color6");
        color7 = uiDocument.rootVisualElement.Q<VisualElement>("Color7");
        color8 = uiDocument.rootVisualElement.Q<VisualElement>("Color8");
        color9 = uiDocument.rootVisualElement.Q<VisualElement>("Color9");

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
        color1.RegisterCallback<ClickEvent, VisualElement>(OnColorSelectedEvent, color1);
        color2.RegisterCallback<ClickEvent, VisualElement>(OnColorSelectedEvent, color2);
        color3.RegisterCallback<ClickEvent, VisualElement>(OnColorSelectedEvent, color3);
        color4.RegisterCallback<ClickEvent, VisualElement>(OnColorSelectedEvent, color4);
        color5.RegisterCallback<ClickEvent, VisualElement>(OnColorSelectedEvent, color5);
        color6.RegisterCallback<ClickEvent, VisualElement>(OnColorSelectedEvent, color6);
        color7.RegisterCallback<ClickEvent, VisualElement>(OnColorSelectedEvent, color7);
        color8.RegisterCallback<ClickEvent, VisualElement>(OnColorSelectedEvent, color8);
        color9.RegisterCallback<ClickEvent, VisualElement>(OnColorSelectedEvent, color9);
    }

    private void OnColorSelectedEvent(ClickEvent _evt, VisualElement _button)
    {
        OnColorSelected.Invoke(_button);
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
        // Debug.Log($"Pointer position {normalizedPosition}");
        OnPointerReleased?.Invoke();
    }

    private void OnPointerMovedEvent(PointerMoveEvent _evt)
    {
        Vector2 normalizedPosition = ProcessPosition(_evt.localPosition);
        // Debug.Log($"Pointer position {normalizedPosition}");
        OnPointerMoved?.Invoke(normalizedPosition);
    }

    private void OnPointerDownEvent(PointerDownEvent _evt)
    {
        Vector2 normalizedPosition = ProcessPosition(_evt.localPosition);
        // Debug.Log($"Pointer position {normalizedPosition}");
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
