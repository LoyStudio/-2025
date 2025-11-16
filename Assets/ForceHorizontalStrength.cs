using UnityEngine;
using YG;

public class SimpleSideBars : MonoBehaviour
{
    [Header("Side Bars Settings")]
    [Range(0.1f, 1.0f)]
    public float viewportWidth = 0.8f;

    [Header("UI References")]
    public Canvas gameCanvas; // Canvas для игровых элементов
    public Canvas uiCanvas;   // Canvas для UI элементов (кнопки, панели)

    private Camera gameCamera;
    public Camera gameCameraUI;

    void Start()
    {
        gameCamera = Camera.main;
        YandexGame.GetDataEvent += OnYGReady;
        if (YandexGame.SDKEnabled) OnYGReady();
    }

    void OnYGReady()
    {
        ApplySideBars();
    }

    void Update()
    {
        ApplySideBars();
    }

    void ApplySideBars()
    {
        if (gameCamera == null) return;

        float sideMargin = (1.0f - viewportWidth) / 2.0f;
        gameCamera.rect = new Rect(sideMargin, 0.0f, viewportWidth, 1.0f);
        gameCameraUI.rect = new Rect(sideMargin, 0.0f, viewportWidth, 1.0f);

        // Подстраиваем UI Canvas под игровую область
        if (uiCanvas != null)
        {
            RectTransform canvasRect = uiCanvas.GetComponent<RectTransform>();
            canvasRect.anchorMin = new Vector2(sideMargin, 0f);
            canvasRect.anchorMax = new Vector2(1f - sideMargin, 1f);
            canvasRect.offsetMin = Vector2.zero;
            canvasRect.offsetMax = Vector2.zero;
        }
    }

    void OnDestroy()
    {
        YandexGame.GetDataEvent -= OnYGReady;
    }
}