using System;
using UnityEngine;
using UnityEngine.UIElements;

// Base implemented from video "How to draw Pixel Art in Unity 6" by Sunny Valley Studio, https://youtu.be/_Y0GqcF5qqM
// Minor formatting changes applied to follow style guide, and own buttons added.

public class UIController : MonoBehaviour
{
    private UIDocument uiDocument;
    private VisualElement canvas, clearButton, saveButton, loadButton;

    public event Action<Vector2> OnPointerDown, OnPointerMoved;
    public event Action OnClearButtonClicked, OnSaveButtonClicked, OnLoadButtonClicked, OnPointerReleased, OnPointerOut;

    private void Awake()
    {
        uiDocument = GetComponent<UIDocument>();
        canvas = uiDocument.rootVisualElement.Q<VisualElement>("Canvas");
        clearButton = uiDocument.rootVisualElement.Q<VisualElement>("Clear");
        saveButton = uiDocument.rootVisualElement.Q<VisualElement>("Save");
        loadButton = uiDocument.rootVisualElement.Q<VisualElement>("Load");

        canvas.RegisterCallback<PointerDownEvent>(OnPointerDownEvent);
        canvas.RegisterCallback<PointerMoveEvent>(OnPointerMovedEvent);
        canvas.RegisterCallback<PointerUpEvent>(OnPointerReleasedEvent);
        canvas.RegisterCallback<PointerOutEvent>(OnPointerOutEvent);
        clearButton.RegisterCallback<ClickEvent>(OnClearButtonClickedEvent);
        saveButton.RegisterCallback<ClickEvent>(OnSaveButtonClickedEvent);
        loadButton.RegisterCallback<ClickEvent>(OnLoadButtonClickedEvent);
    }



    private void Start()
    {
        canvas.style.width = Screen.height;
        canvas.style.height = Screen.height;
    }

    private void OnClearButtonClickedEvent(ClickEvent evt)
    {
        OnClearButtonClicked?.Invoke();
    }

    private void OnLoadButtonClickedEvent(ClickEvent evt)
    {
        OnSaveButtonClicked?.Invoke();
    }

    private void OnSaveButtonClickedEvent(ClickEvent evt)
    {
        OnLoadButtonClicked?.Invoke();
    }

    private void OnPointerOutEvent(PointerOutEvent evt)
    {
        OnPointerOut?.Invoke();
    }

    private void OnPointerReleasedEvent(PointerUpEvent evt)
    {
        Vector2 normalizedPosition = ProcessPosition(evt.localPosition);
        Debug.Log($"Pointer position {normalizedPosition}");
        OnPointerReleased?.Invoke();
    }

    private void OnPointerMovedEvent(PointerMoveEvent evt)
    {
        Vector2 normalizedPosition = ProcessPosition(evt.localPosition);
        Debug.Log($"Pointer position {normalizedPosition}");
        OnPointerMoved?.Invoke(normalizedPosition);
    }

    private void OnPointerDownEvent(PointerDownEvent evt)
    {
        Vector2 normalizedPosition = ProcessPosition(evt.localPosition);
        Debug.Log($"Pointer position {normalizedPosition}");
        OnPointerDown?.Invoke(normalizedPosition);
    }

    //Flips Y axis to be pointing UP
    private Vector2 ProcessPosition(Vector2 localMousePosition)
    {
        Vector2 normalizedPosition = NormalizePixelPosition(localMousePosition);
        normalizedPosition.y = 1 - normalizedPosition.y;
        return normalizedPosition;
    }

    // Normalizes pixel position to the 0-1 range based on Ui element width and height
    private Vector2 NormalizePixelPosition(Vector2 pixelPosition)
    {
        float normalizedX = Mathf.InverseLerp(0, canvas.layout.width, pixelPosition.x);
        float normalizedY = Mathf.InverseLerp(0, canvas.layout.height, pixelPosition.y);
        return new(normalizedX, normalizedY);
    }
}
