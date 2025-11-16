using UnityEngine;
using YG;

public class RenderTexturePortrait : MonoBehaviour
{
    [Header("Render Texture Setup")]
    public Camera gameCamera;
    public Camera outputCamera;
    public RenderTexture portraitTexture;

    void Start()
    {
        // Создаем Render Texture с портретным соотношением
        portraitTexture = new RenderTexture(720, 1280, 24);

        if (gameCamera != null)
        {
            gameCamera.targetTexture = portraitTexture;
        }

        if (outputCamera != null)
        {
            outputCamera.targetTexture = null;
        }

        YandexGame.GetDataEvent += OnYGReady;
        if (YandexGame.SDKEnabled) OnYGReady();
    }

    void OnYGReady()
    {
        SetupCameras();
    }

    void SetupCameras()
    {
        // Основная камера рендерит в текстуру
        if (gameCamera != null)
        {
            gameCamera.orthographicSize = CalculateOrthoSize();
        }

        // Камера вывода масштабирует текстуру на экран
        if (outputCamera != null)
        {
            outputCamera.rect = new Rect(0, 0, 1, 1);
        }
    }

    float CalculateOrthoSize()
    {
        // Для ортографической камеры
        return 5f; // Настройте под вашу игру
    }

    void OnDestroy()
    {
        YandexGame.GetDataEvent -= OnYGReady;
        if (portraitTexture != null)
        {
            portraitTexture.Release();
        }
    }
}