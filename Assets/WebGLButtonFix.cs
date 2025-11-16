using UnityEngine;
using UnityEngine.UI;

public class ReliableWebGLButton : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    private float lastClickTime = -999f; // Очень старое время

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        // Простая, но эффективная проверка
        if (Time.unscaledTime - lastClickTime < 1f)
        {
            Debug.Log("Click ignored - too fast");
            return;
        }

        lastClickTime = Time.unscaledTime;
        Debug.Log("Click processed successfully!");

        if (Time.timeScale > 0f)
        {
            Time.timeScale = 0f;
            if (panel != null)
            {
                panel.SetActive(true);
            }
        }
    }
}