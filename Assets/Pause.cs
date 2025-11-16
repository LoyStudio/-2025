using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class Pause : MonoBehaviour
{
    public GameObject panel;

    void Start()
    {
        // Подписываемся на событие загрузки Yandex Games
        YandexGame.GetDataEvent += OnYandexDataLoaded;

        // Если данные уже загружены
        if (YandexGame.SDKEnabled)
            OnYandexDataLoaded();
    }

    void OnYandexDataLoaded()
    {
        Debug.Log("=== YANDEX GAMES DATA LOADED ===");
        Debug.Log("Pause script initialized with YG");
        Debug.Log("Panel assigned: " + (panel != null));
    }

    public void PauseButton()
    {
        Debug.Log("=== PAUSE BUTTON CLICKED IN YANDEX ===");
        Debug.Log("Yandex SDK Enabled: " + YandexGame.SDKEnabled);
        Debug.Log("TimeScale: " + Time.timeScale);
        Debug.Log("Panel: " + (panel != null ? panel.name : "NULL"));

        // Проверяем условия через Yandex Game
        if (!YandexGame.SDKEnabled)
        {
            Debug.LogWarning("Yandex SDK not enabled - running in test mode");
        }

        if (panel == null)
        {
            Debug.LogError("❌ ERROR: Panel is NULL!");
            return;
        }

        // Проверка условий паузы
        if (Timer.countdownFinished && ButtonsGame.countdownFinished)
        {
            Debug.Log("✅ All conditions met - activating pause");
            ButtonsGame.isPause = true;
            Time.timeScale = 0f;
            panel.SetActive(true);

            // Показываем рекламу или делаем другие YG действия
            if (YandexGame.SDKEnabled)
            {
                Debug.Log("Pause in Yandex Games environment");
            }

            Debug.Log("✅ Pause activated successfully in YG!");
        }
        else
        {
            Debug.LogWarning("⚠ Cannot pause - conditions not met");
            Debug.Log("Timer: " + Timer.countdownFinished);
            Debug.Log("ButtonsGame: " + ButtonsGame.countdownFinished);
        }
    }

    void OnDestroy()
    {
        // Отписываемся от события
        YandexGame.GetDataEvent -= OnYandexDataLoaded;
    }
}