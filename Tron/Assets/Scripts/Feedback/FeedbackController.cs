using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FeedbackController : MonoBehaviour
{
    public static FeedbackController Instance { get; private set; }
    public List<LogData> datas;
    public List<FirewallLogData> firewallLogDatas;
    public GameObject feedback;
    public GameObject panel;
    public bool preparing=false;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void prepare()
    {
        StartCoroutine(MovePanelUp());
        deliverFeedback();
        playerMovement.Instance.canMove = false;
        GameManager.Instance.isInGame = true;
        Cursor.lockState = CursorLockMode.None;
        preparing = true;

    }

    private IEnumerator MovePanelUp()
    {
        Vector3 startPos = panel.transform.position;
        Vector3 endPos = startPos + new Vector3(0, 1300f, 0); 
        float duration = 1f; 
        float elapsed = 0f;

        while (elapsed < duration)
        {
            panel.transform.position = Vector3.Lerp(startPos, endPos, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        panel.transform.position = endPos; 
    }
    public void deliverFeedback()
    {
        // Combinar ambas listas en una sola colección genérica de tipo object (porque heredan de MonoBehaviour)
        List<MonoBehaviour> allLogs = new List<MonoBehaviour>();
        allLogs.AddRange(datas);
        allLogs.AddRange(firewallLogDatas);

        // Usamos un diccionario para eliminar duplicados por el campo "texto"
        Dictionary<string, MonoBehaviour> uniqueLogs = new Dictionary<string, MonoBehaviour>();

        foreach (var log in allLogs)
        {
            // Extraer el texto según el tipo
            string texto = "";
            string feedbackTexto = "";

            if (log is LogData logData)
            {
                texto = logData.texto;
                feedbackTexto = logData.feedback;
            }
            else if (log is FirewallLogData firewallData)
            {
                texto = firewallData.texto;
                feedbackTexto = firewallData.feedback;
            }

            // Agregar solo si el texto no existe ya
            if (!uniqueLogs.ContainsKey(texto))
            {
                uniqueLogs.Add(texto, log);
            }
        }

        // Construimos el string final con los feedbacks únicos
        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        foreach (var entry in uniqueLogs.Values)
        {
            string feedbackTexto = "";

            if (entry is LogData logData)
            {
                feedbackTexto =logData.texto+"-->" +logData.feedback;
            }
            else if (entry is FirewallLogData firewallData)
            {
                feedbackTexto =firewallData.texto+"-->"+ firewallData.feedback;
            }

            sb.AppendLine(feedbackTexto);
        }

        // Finalmente asignamos el texto combinado al TextMeshProUGUI
        feedback.GetComponent<TextMeshProUGUI>().text = sb.ToString();
    }
    
}
