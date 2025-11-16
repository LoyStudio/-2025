using UnityEngine;
using YG;

public class AdaptiveSideBars : MonoBehaviour
{
    [Header("Side Bars Settings")]
    [Range(0.1f, 1.0f)]
    public float viewportWidth = 0.8f;

    [Header("UI Container")]
    public RectTransform uiContainer;

    private Camera gameCamera;

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

        // Сужаем игровую камеру
        gameCamera.rect = new Rect(sideMargin, 0.0f, viewportWidth, 1.0f);

        // Подстраиваем UI контейнер под игровую область
        if (uiContainer != null)
        {
            uiContainer.anchorMin = new Vector2(sideMargin, 0f);
            uiContainer.anchorMax = new Vector2(1f - sideMargin, 1f);
            uiContainer.offsetMin = Vector2.zero;
            uiContainer.offsetMax = Vector2.zero;
        }
    }

    void OnDestroy()
    {
        YandexGame.GetDataEvent -= OnYGReady;
    }
}