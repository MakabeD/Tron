using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameClock : MonoBehaviour
{
    public static GameClock Instance;

    [Header("Reloj (configurable)")]
    [Tooltip("Cuántos segundos reales equivalen a 1 hora de juego. Ej: 180 = 3 minutos reales = 1 hora de juego.")]
    public float realSecondsPerGameHour = 180f;

    [Tooltip("Ignorar Time.timeScale (usa Time.unscaledDeltaTime). Útil si pausas con Time.timeScale = 0")]
    public bool useUnscaledTime = false;

    [Header("Hora inicial (0-23)")]
    public int startHour = 0;
    [Range(0, 59)]
    public int startMinute = 0;

    [Header("UI")]
    public TextMeshProUGUI timeText;

    // Evento para que otros scripts se suscriban
    public event Action<int, int> OnTimeChanged;

    // Estado interno
    int gameHour;
    int gameMinute;
    float timer = 0f; // acumula segundos reales
    float secondsPerGameMinute;

    void Awake()
    {
        if (Instance == null) { Instance = this; DontDestroyOnLoad(gameObject); }
        else { Destroy(gameObject); return; }

        // Validaciones mínimas
        if (realSecondsPerGameHour <= 0f) realSecondsPerGameHour = 60f;

        secondsPerGameMinute = realSecondsPerGameHour / 60f;

        gameHour = Mathf.Clamp(startHour, 0, 23);
        gameMinute = Mathf.Clamp(startMinute, 0, 59);
    }

    void Start()
    {
        RefreshUI();
    }

    void Update()
    {
        float dt = useUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;
        timer += dt;

        // Mientras haya suficiente tiempo acumulado, avanzo minutos (evita perder tiempo si hay saltos grandes)
        while (timer >= secondsPerGameMinute)
        {
            timer -= secondsPerGameMinute;
            AdvanceGameMinute();
        }
    }

    void AdvanceGameMinute()
    {
        gameMinute++;
        if (gameMinute >= 60)
        {
            gameMinute = 0;
            gameHour++;
             
        }

        RefreshUI();
        OnTimeChanged?.Invoke(gameHour, gameMinute);
    }

    void RefreshUI()
    {
        if (timeText != null)
        {
            // Formato HH:MM (con ceros)
            timeText.text = string.Format("{0:00} pm", gameHour);
        }
    }

    // APIs utiles
    public void SetTime(int hour, int minute)
    {
        gameHour = Mathf.Clamp(hour, 0, 23);
        gameMinute = Mathf.Clamp(minute, 0, 59);
        timer = 0f;
        RefreshUI();
        OnTimeChanged?.Invoke(gameHour, gameMinute);
    }

    public (int hour, int minute) GetTime() => (gameHour, gameMinute);

    public void SetRealSecondsPerGameHour(float seconds)
    {
        realSecondsPerGameHour = Mathf.Max(0.01f, seconds);
        secondsPerGameMinute = realSecondsPerGameHour / 60f;
    }
}
