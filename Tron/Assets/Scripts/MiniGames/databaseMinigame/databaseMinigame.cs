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
        havelose = true;
        GameHP.Instance.GetDownLife(Malicious_count);
        MinigameManager.Instance.EndMinigame();
        GameManager.Instance.isinCombat = true;
        Debug.Log("En Combate");
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

        if(testRoomController.Instance.getEmergencyTimer()==50)
        {
            OnLose();
            return;
        }
    }
    

    private  void maliciusClicked()
    {
        Malicious_count--;
    }
    public void beningClicked()
    {
        testRoomController.Instance.timer += 5;
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
        textos1 = new databaseLogStructure[]
           {
            databaseLogFactory.Create("[2025-10-27 12:00:01] DB: Connection accepted from 192.168.1.10", false),
            databaseLogFactory.Create("[2025-10-27 12:00:05] DB: Query OK - SELECT * FROM users", false),
            databaseLogFactory.Create("[2025-10-27 12:00:12] DB: User 'guest' failed login (3 attempts)", true),
            databaseLogFactory.Create("[2025-10-27 12:00:20] DB: Backup completed (size 12.3MB)", false),
            databaseLogFactory.Create("[2025-10-27 12:00:25] DB: Warning - slow query detected (0.8s)", false),
            databaseLogFactory.Create("[2025-10-27 12:00:31] DB: Connection closed by client 192.168.1.11", false),
            databaseLogFactory.Create("[2025-10-27 12:00:45] DB: INFO: Routine index rebuild started", false),
            databaseLogFactory.Create("[2025-10-27 12:00:50] DB: INFO: Routine index rebuild finished", false),
            databaseLogFactory.Create("[2025-10-27 12:00:58] DB: User 'admin' login successful", false),
            databaseLogFactory.Create("[2025-10-27 12:01:03] DB: Checksum OK for log segment #1", false)
           };

        // Logs medios: algunos sospechosos o maliciosos leves
        textos2 = new databaseLogStructure[]
        {
            databaseLogFactory.Create("[2025-10-27 12:05:01] DB: Query OK - UPDATE users SET active=1", false),
            databaseLogFactory.Create("[2025-10-27 12:05:03] DB: ALERT: Unexpected schema change detected", true),
            databaseLogFactory.Create("[2025-10-27 12:05:07] DB: Failed login from external IP 10.255.255.7", true),
            databaseLogFactory.Create("[2025-10-27 12:05:10] DB: LOG_TRUNCATE: tail truncated (reason: policy)", false),
            databaseLogFactory.Create("[2025-10-27 12:05:15] DB: Warning - duplicate transaction id 0x00FF", false),
            databaseLogFactory.Create("[2025-10-27 12:05:18] DB: File write error: /var/db/data.db (errno 5)", true),
            databaseLogFactory.Create("[2025-10-27 12:05:22] DB: Recovered 12 orphan pages after checkpoint", false),
            databaseLogFactory.Create("[2025-10-27 12:05:30] DB: Suspicious: query 'DROP TABLE tmp' rolled back", true),
            databaseLogFactory.Create("[2025-10-27 12:05:36] DB: IP 172.16.254.1 attempted auth w/ blank password", true),
            databaseLogFactory.Create("[2025-10-27 12:05:42] DB: HASH_MISMATCH detected on segment #3", true)
        };

        // Logs avanzados: mezcla de comportamiento irregular y exploits claros
        textos3 = new databaseLogStructure[]
        {
            databaseLogFactory.Create("[2025-10-27 12:10:01] DB: Notice - log entry duplicated (id: 9834) => see 9833", false),
            databaseLogFactory.Create("[2025-10-27 12:09:59] DB: Time anomaly: event timestamp moved backward 00:00:12", true),
            databaseLogFactory.Create("[2025-10-27 12:10:08] DB: Detected SQL injection pattern in query: \"' OR '1'='1\"", true),
            databaseLogFactory.Create("[2025-10-27 12:10:14] DB: File /etc/db/keys.pem opened by unknown PID 4421", true),
            databaseLogFactory.Create("[2025-10-27 12:10:21] DB: ALERT: Base64 payload in field 'notes' (length 1024)", true),
            databaseLogFactory.Create("[2025-10-27 12:10:30] DB: Warning - replication lag: master ahead by 00:02:13", false),
            databaseLogFactory.Create("[2025-10-27 12:10:38] DB: LOG_INCONSISTENCY: segment #7 overlaps with #6", false),
            databaseLogFactory.Create("[2025-10-27 12:10:45] DB: User 'service' escalated privileges unexpectedly", true),
            databaseLogFactory.Create("[2025-10-27 12:10:52] DB: NTP delta detected: clock jumped +300s", false),
            databaseLogFactory.Create("[2025-10-27 12:11:00] DB: Checksum altered: expected 0xA3F2 got 0x0000", true)
        };

        // Logs difíciles: ya son ciberataques o manipulación directa
        textosDificiles1 = new databaseLogStructure[]
        {
            databaseLogFactory.Create("[2025-10-27 12:15:01] DB: SIG_INVALID: digital signature mismatch on archive.bin (cert: CN=db-root)", true),
            databaseLogFactory.Create("[2025-10-27 12:15:07] DB: Log replay attack suspected: repeated sequence found (9834..9842) with different hashes", true),
            databaseLogFactory.Create("[2025-10-27 12:15:12] DB: Hidden column 'x0r_exec' appears in table 'configs' (not in schema)", true),
            databaseLogFactory.Create("[2025-10-27 12:15:20] DB: Process 'svc-updater' spawns only when observer disconnected", true),
            databaseLogFactory.Create("[2025-10-27 12:15:28] DB: Encrypted blob detected in log: 'U2FsdGVkX1+Qp0...' (AES?)", true),
            databaseLogFactory.Create("[2025-10-27 12:15:36] DB: Firmware signature mismatch on camera-feed#3 (reported ver 1.2.0 != expected 1.1.7)", true),
            databaseLogFactory.Create("[2025-10-27 12:15:44] DB: Persistent backdoor: auth token reused across sessions (token id: tk-77C)", true),
            databaseLogFactory.Create("[2025-10-27 12:15:52] DB: Rootkit indicator: /dev/pts/hidden_fd opened by kernel module 'kmod_x'", true),
            databaseLogFactory.Create("[2025-10-27 12:15:58] DB: Certificate revoked but connection accepted: CN=svc-node-9", true),
            databaseLogFactory.Create("[2025-10-27 12:16:05] DB: Replay timestamp insertion: events reordered to hide action at 12:14:33", true)
        };

        // Logs extremadamente avanzados (APT, malware encubierto, etc.)
        textosDificiles2 = new databaseLogStructure[]
        {
            databaseLogFactory.Create("[2025-10-27 12:20:01] DB: APT telemetry: lateral movement signature matches IOC set 'night-owl-v2'", true),
            databaseLogFactory.Create("[2025-10-27 12:20:09] DB: NTP poisoning chain: multiple servers report conflicting stratum", true),
            databaseLogFactory.Create("[2025-10-27 12:20:17] DB: Covert channel detected: low-frequency timing pattern between nodes 4 & 9", true),
            databaseLogFactory.Create("[2025-10-27 12:20:25] DB: Signed manifesto injected in logs: '/* kernel hook enabled */' (obfuscated)", true),
            databaseLogFactory.Create("[2025-10-27 12:20:33] DB: Hardware trojan suspicion: unexpected firmware write to EEPROM", true),
            databaseLogFactory.Create("[2025-10-27 12:20:41] DB: Private key exfiltration pattern: chunks written to 'tmp/.cache' with steganography markers", true),
            databaseLogFactory.Create("[2025-10-27 12:20:49] DB: Time-based polymorphic payload executed at leap-second boundary", true),
            databaseLogFactory.Create("[2025-10-27 12:20:57] DB: Command-and-Control beacon encoded inside seemingly benign telemetry", true),
            databaseLogFactory.Create("[2025-10-27 12:21:05] DB: Multi-vector persistence: BIOS write + kernel module + scheduled task match", true),
            databaseLogFactory.Create("[2025-10-27 12:21:13] DB: Log fabricator active: synthetic entries injected with forged HMACs", true)
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
