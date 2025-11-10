using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro;
using UnityEngine;
using FGUIStarter;

public class databaseMinigame : MonoBehaviour, IMinigame
{
    int id = 0;
    public GameObject[] gameObjects; databaseLogStructure[] textos1, textos2, textos3, textosDificiles1, textosDificiles2;
    int Malicious_count = 0;
    bool havelose = false;
    public void StartMinigame()
    {
        Initialize();
        getDisableAll(gameObjects);
        activateInOrder(gameObjects);
    }
    public void EndMinigame()
    {
        Debug.Log("Saliendo");
    }

    public void OnLose()
    {

        if (!EventManager.Instance.GetEvent(id).EventEmergency)
        {
            GameManager.Instance.isinCombat++;
            EventManager.Instance.GetEvent(id).EventEmergency = true;
            EventManager.Instance.IncrementEmergencyEventCount();
            Debug.Log("emergencia por perder");
        }
            havelose = true;
            GameHP.Instance.GetDownLife(Malicious_count);
            MinigameManager.Instance.EndMinigame();
       
    }

    public void OnWin()
    {
        
        EventManager.Instance.GetEvent(id).StopEventExecution();
        MinigameManager.Instance.EndMinigame();
        Debug.Log("Ganaste");
    }


    // Start is called before the first frame update
    void Start()
    {
        CustomButton.onMalicious += maliciusClicked;
        CustomButton.onBenign += beningClicked;

    }

    // Update is called once per frame
    void Update()
    {
        if(Malicious_count==0&&!havelose)
        {
            OnWin();
            return;
        }

        if(TestRoomController.Instance.GetEmergencyTimer()>=60)
        {
            OnLose();
            return;
        }
    }
    

    private  void maliciusClicked()
    {
        Malicious_count--;
    }
    public void beningClicked(LogData log)
    {
        FeedbackController.Instance.datas.Add(log); 
        TestRoomController.Instance.IncreaseEmergencyTimer(5);
    }
    void AsignarTextosAleatorios(databaseLogStructure[] textos)
    {
        if (textos.Length < gameObjects.Length)
        {
            Debug.LogError("No hay suficientes textos para todos los objetos.");
            return;
        }

        // 1️⃣ Mezclamos el array de textos (algoritmo Fisher–Yates)
        databaseLogStructure[] textosMezclados = (databaseLogStructure[])textos.Clone();
        for (int i = 0; i < textosMezclados.Length; i++)
        {
            int randomIndex = UnityEngine.Random.Range(i, textosMezclados.Length);
            (textosMezclados[i], textosMezclados[randomIndex]) = (textosMezclados[randomIndex], textosMezclados[i]);
        }

        // 2️⃣ Asignamos a cada objeto un texto único
        for (int i = 0; i < gameObjects.Length; i++)
        {
            var tmp = gameObjects[i].GetComponent<TextMeshProUGUI>();
            if (tmp != null)
            {
                // Asigna el texto al componente visual
                tmp.text = textosMezclados[i].text;

                // Añade o reutiliza el componente LogData
                var logData = gameObjects[i].GetComponent<LogData>();
                if (logData == null)
                    logData = gameObjects[i].AddComponent<LogData>();

                // Guarda los datos de ese texto
                logData.texto = textosMezclados[i].text;
                logData.isMalicious = textosMezclados[i].isMalicious;
                logData.feedback = textosMezclados[i].feedback;
            }
            else
            {
                Debug.LogWarning($"El objeto {gameObjects[i].name} no tiene un componente TextMeshPro.");
            }
        }

    }
    public int ContarLogsMaliciosos(databaseLogStructure[] logs)
    {
        int contador = 0;

        // Recorremos todo el array
        foreach (var log in logs)
        {
            if (log.isMalicious) // si el atributo es verdadero
            {
                contador++;
            }
        }

        return contador; // devolvemos la cantidad total
    }

    void Initialize()
    {
        // Textos 1 - Básicos (balance: 5 benignos, 5 maliciosos)
        textos1 = new databaseLogStructure[]
        {
    databaseLogFactory.Create("[2025-10-27 12:00:01] DB: Connection accepted from 192.168.1.10", false, "Conexión interna esperada; IP interna en rango privado."),
    databaseLogFactory.Create("[2025-10-27 12:00:05] DB: Query OK - SELECT * FROM users", false, "Consulta SELECT normal; sin parámetros dinámicos sospechosos."),
    databaseLogFactory.Create("[2025-10-27 12:00:12] DB: User 'guest' failed login (3 attempts)", true, "Múltiples intentos fallidos en corto periodo — posible brute-force."),
    databaseLogFactory.Create("[2025-10-27 12:00:20] DB: Backup completed (size 12.3MB)", false, "Backup regular completado; tamaño acorde a datos esperados."),
    databaseLogFactory.Create("[2025-10-27 12:00:25] DB: Warning - slow query detected (0.8s)", false, "Query lenta pero dentro de umbrales; investigar rendimiento si persiste."),
    databaseLogFactory.Create("[2025-10-27 12:00:31] DB: Connection closed by client 192.168.1.11", false, "Cierre de conexión por cliente interno; comportamiento normal."),
    databaseLogFactory.Create("[2025-10-27 12:00:45] DB: Multiple failed auth attempts observed from 203.0.113.55", true, "IP pública con múltiples fallos; patrón de ataque remoto."),
    databaseLogFactory.Create("[2025-10-27 12:00:50] DB: INFO: Routine index rebuild finished", false, "Tarea de mantenimiento completada sin errores."),
    databaseLogFactory.Create("[2025-10-27 12:00:58] DB: Unexpected command: ALTER SYSTEM SET config='unsafe'", true, "Comando de configuración no autorizado — cambio de sistema potencialmente peligroso."),
    databaseLogFactory.Create("[2025-10-27 12:01:03] DB: Checksum OK for log segment #1", false, "Checksum válido; integridad del segmento verificada.")
        };

        // Textos 2 - Medios (balance: 5 benignos, 5 maliciosos)
        textos2 = new databaseLogStructure[]
        {
    databaseLogFactory.Create("[2025-10-27 12:05:01] DB: Query OK - UPDATE users SET active=1 WHERE id=42", false, "Actualización legítima de usuario identificada por id; patrón normal."),
    databaseLogFactory.Create("[2025-10-27 12:05:03] DB: ALERT: Unexpected schema change detected (table: payments)", true, "Cambio de esquema no planificado en tabla sensible; posible alteración maliciosa."),
    databaseLogFactory.Create("[2025-10-27 12:05:07] DB: Failed login from external IP 10.255.255.7", true, "IP externa con fallos de autenticación; potencial intento de acceso."),
    databaseLogFactory.Create("[2025-10-27 12:05:10] DB: LOG_TRUNCATE: tail truncated (reason: policy)", false, "Truncamiento por política configurada; operación administrativa esperada."),
    databaseLogFactory.Create("[2025-10-27 12:05:15] DB: Warning - duplicate transaction id 0x00FF", false, "Duplicado de transacción detectado; puede ser condición transitoria, requiere seguimiento."),
    databaseLogFactory.Create("[2025-10-27 12:05:18] DB: File write error: /var/db/data.db (errno 5)", true, "Error de escritura en base de datos; posible corrupción o intento de sabotaje."),
    databaseLogFactory.Create("[2025-10-27 12:05:22] DB: Recovered 12 orphan pages after checkpoint", false, "Recuperación de páginas huérfanas completada; operación de mantenimiento."),
    databaseLogFactory.Create("[2025-10-27 12:05:30] DB: Suspicious: query 'DROP TABLE tmp' rolled back", true, "DROP TABLE ejecutado y revertido — operación destructiva detectada."),
    databaseLogFactory.Create("[2025-10-27 12:05:36] DB: IP 172.16.254.1 attempted auth w/ blank password", true, "Intento de autenticación con contraseña vacía; riesgo alto de acceso no autorizado."),
    databaseLogFactory.Create("[2025-10-27 12:05:42] DB: HASH_MISMATCH detected on segment #3", false, "Desajuste de hash detectado pero dentro de proceso de reparación automática; requiere auditoría.")
        };

        // Textos 3 - Avanzados (balance: 5 benignos, 5 maliciosos)
        textos3 = new databaseLogStructure[]
        {
    databaseLogFactory.Create("[2025-10-27 12:10:01] DB: Notice - log entry duplicated (id: 9834) => see 9833", false, "Duplicado de entrada de logs; generalmente benigno pero revisar origen de duplicación."),
    databaseLogFactory.Create("[2025-10-27 12:09:59] DB: Time anomaly: event timestamp moved backward 00:00:12", true, "Anomalía de tiempo; puede indicar manipulación de timestamps para ocultar actividad."),
    databaseLogFactory.Create("[2025-10-27 12:10:08] DB: Detected SQL injection pattern in query: \"' OR '1'='1' --\"", true, "Patrón clásico de SQL injection detectado en la consulta; riesgo de exfiltración de datos."),
    databaseLogFactory.Create("[2025-10-27 12:10:14] DB: File /etc/db/keys.pem opened by unknown PID 4421", true, "Acceso a clave privada por PID desconocido; posible intento de extracción de claves."),
    databaseLogFactory.Create("[2025-10-27 12:10:21] DB: ALERT: Base64 payload in field 'notes' (length 1024)", true, "Carga Base64 inusualmente grande en campo texto; puede ser exfiltración o malware incrustado."),
    databaseLogFactory.Create("[2025-10-27 12:10:30] DB: Warning - replication lag: master ahead by 00:02:13", false, "Lag de replicación detectado; problema de performance, no necesariamente malicioso."),
    databaseLogFactory.Create("[2025-10-27 12:10:38] DB: LOG_INCONSISTENCY: segment #7 overlaps with #6", false, "Inconsistencia de logs detectada; probablemente por recuperación parcial, requiere análisis."),
    databaseLogFactory.Create("[2025-10-27 12:10:45] DB: User 'service' escalated privileges unexpectedly", true, "Escalamiento de privilegios inesperado para cuenta de servicio — alto riesgo internal breach."),
    databaseLogFactory.Create("[2025-10-27 12:10:52] DB: NTP delta detected: clock jumped +300s", false, "Salto de tiempo en NTP; puede ser ajuste de reloj, importante para correlación de eventos."),
    databaseLogFactory.Create("[2025-10-27 12:11:00] DB: Checksum altered: expected 0xA3F2 got 0x0000", true, "Checksum alterado a valor nulo; indica posible corrupción intencional o manipulación de registros.")
        };



        int choise = UnityEngine.Random.Range(0, 3);
        switch (choise)
        {
            case 0:
                Debug.Log("asignado");
                Malicious_count=ContarLogsMaliciosos(textos1);
                AsignarTextosAleatorios(textos1);
                break;
            case 1:
                Debug.Log("asignado");
                Malicious_count=ContarLogsMaliciosos(textos2);
                AsignarTextosAleatorios(textos2);
                break;
            case 2:
                Debug.Log("asignado");
                Malicious_count=ContarLogsMaliciosos(textos3);
                AsignarTextosAleatorios(textos3);
                break;
            default:
                Debug.Log("Fallo en la matrix");
                break;
        }

    }

    public void getDisableAll(GameObject[] objects)
    {
        foreach (GameObject obj in objects)
        {
            if (obj != null)
                obj.SetActive(false);
        }
    }
    public void activateInOrder(GameObject[] objects)
    {
        StartCoroutine(activateInOrderCoroutine(objects));
    }

    private IEnumerator activateInOrderCoroutine(GameObject[] objects)
    {
        for (int i = 0; i < objects.Length; i++)
        {
            if (objects[i] != null)
            {
                objects[i].SetActive(true);
                // Espera un tiempo aleatorio entre 0.25 y 2 segundos
                float delay = UnityEngine.Random.Range(0.25f, 2f);
                yield return new WaitForSeconds(delay);
            }
        }
    }

}
