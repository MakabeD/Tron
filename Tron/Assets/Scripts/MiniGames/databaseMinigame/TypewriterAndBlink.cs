using UnityEngine;
using TMPro;
using System.Collections;

public class TypewriterAndBlink : MonoBehaviour
{
    [Header("Configuración de escritura")]
    public float typingSpeed = 0.01f; // tiempo entre letras

    [Header("Configuración de titileo")]
    public float blinkSpeed = 0.5f;   // velocidad de cambio de color
    public Color brightRed = new Color(1f, 0.2f, 0.2f); // rojo fuerte
    public Color darkRed = new Color(0.4f, 0f, 0f);     // rojo oscuro

    private TextMeshProUGUI tmp;
    private string fullText;
    public  bool isTyping = true;

    void Start()
    {
         
        tmp = GetComponent<TextMeshProUGUI>();
        if (tmp == null)
        {
            Debug.LogError("No se encontra TextMeshProUGUI en este GameObject.");
            return;
        }

        fullText = tmp.text;
        tmp.text = "";
        StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        isTyping = true;

        foreach (char c in fullText)
        {
            tmp.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
        StartCoroutine(BlinkText());
    }

    IEnumerator BlinkText()
    {
        yield return new WaitForSeconds(0.5f);

        Color targetColor = brightRed;
        Color currentColor = tmp.color;

        while (true)
        {
            // interpolación suave entre colores
            float t = 0f;
            Color startColor = tmp.color;

            while (t < 1f)
            {
                t += Time.deltaTime / blinkSpeed;
                tmp.color = Color.Lerp(startColor, targetColor, t);
                yield return null;
            }

            // alternar destino
            targetColor = targetColor == brightRed ? darkRed : brightRed;
        }
    }
}
