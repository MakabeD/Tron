using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro;
using UnityEngine;

public class databaseMinigame : MonoBehaviour, IMinigame
{
    int id = 0;
    public GameObject[] gameObjects; string[] textos1, textos2, textos3, textosDificiles1, textosDificiles2;
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
        throw new System.NotImplementedException();
    }

    public void OnWin()
    {
        throw new System.NotImplementedException();
    }


    // Start is called before the first frame update
    void Start()
    {
        StartMinigame();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            MinigameManager.Instance.EndMinigame(id);
        }
    }

    void AsignarTextosAleatorios(string[] textos)
    {
        if (textos.Length < gameObjects.Length)
        {
            Debug.LogError("No hay suficientes textos para todos los objetos.");
            return;
        }

        // 1️⃣ Mezclamos el array de textos (algoritmo Fisher–Yates)
        string[] textosMezclados = (string[])textos.Clone();
        for (int i = 0; i < textosMezclados.Length; i++)
        {
            int randomIndex = UnityEngine.Random.Range(i, textosMezclados.Length);
            (textosMezclados[i], textosMezclados[randomIndex]) = (textosMezclados[randomIndex], textosMezclados[i]);
        }

        // 2️⃣ Asignamos a cada objeto un texto único
        for (int i = 0; i < gameObjects.Length; i++)
        {
            var tmp = gameObjects[i].GetComponent<TextMeshProUGUI>();//gameObjects[i].GetComponent<TextMeshPro>() ?? 
            if (tmp != null)
            {
                tmp.text = textosMezclados[i];
            }
            else
            {
                Debug.LogWarning($"El objeto {gameObjects[i].name} no tiene un componente TextMeshPro.");
            }
        }
    }
    void Initialize()
    {
        string[] textos1 = {
    "[2025-10-27 12:00:01] DB: Connection accepted from 192.168.1.10",
    "[2025-10-27 12:00:05] DB: Query OK - SELECT * FROM users",
    "[2025-10-27 12:00:12] DB: User 'guest' failed login (3 attempts)",
    "[2025-10-27 12:00:20] DB: Backup completed (size 12.3MB)",
    "[2025-10-27 12:00:25] DB: Warning - slow query detected (0.8s)",
    "[2025-10-27 12:00:31] DB: Connection closed by client 192.168.1.11",
    "[2025-10-27 12:00:45] DB: INFO: Routine index rebuild started",
    "[2025-10-27 12:00:50] DB: INFO: Routine index rebuild finished",
    "[2025-10-27 12:00:58] DB: User 'admin' login successful",
    "[2025-10-27 12:01:03] DB: Checksum OK for log segment #1"
};

        string[] textos2 = {
    "[2025-10-27 12:05:01] DB: Query OK - UPDATE users SET active=1",
    "[2025-10-27 12:05:03] DB: ALERT: Unexpected schema change detected",
    "[2025-10-27 12:05:07] DB: Failed login from external IP 10.255.255.7",
    "[2025-10-27 12:05:10] DB: LOG_TRUNCATE: tail truncated (reason: policy)",
    "[2025-10-27 12:05:15] DB: Warning - duplicate transaction id 0x00FF",
    "[2025-10-27 12:05:18] DB: File write error: /var/db/data.db (errno 5)",
    "[2025-10-27 12:05:22] DB: Recovered 12 orphan pages after checkpoint",
    "[2025-10-27 12:05:30] DB: Suspicious: query 'DROP TABLE tmp' rolled back",
    "[2025-10-27 12:05:36] DB: IP 172.16.254.1 attempted auth w/ blank password",
    "[2025-10-27 12:05:42] DB: HASH_MISMATCH detected on segment #3"
};

        string[] textos3 = {
    "[2025-10-27 12:10:01] DB: Notice - log entry duplicated (id: 9834) => see 9833",
    "[2025-10-27 12:09:59] DB: Time anomaly: event timestamp moved backward 00:00:12",
    "[2025-10-27 12:10:08] DB: Detected SQL injection pattern in query: \"' OR '1'='1\"",
    "[2025-10-27 12:10:14] DB: File /etc/db/keys.pem opened by unknown PID 4421",
    "[2025-10-27 12:10:21] DB: ALERT: Base64 payload in field 'notes' (length 1024)",
    "[2025-10-27 12:10:30] DB: Warning - replication lag: master ahead by 00:02:13",
    "[2025-10-27 12:10:38] DB: LOG_INCONSISTENCY: segment #7 overlaps with #6",
    "[2025-10-27 12:10:45] DB: User 'service' escalated privileges unexpectedly",
    "[2025-10-27 12:10:52] DB: NTP delta detected: clock jumped +300s",
    "[2025-10-27 12:11:00] DB: Checksum altered: expected 0xA3F2 got 0x0000"
};

        string[] textosDificiles1 = {
    "[2025-10-27 12:15:01] DB: SIG_INVALID: digital signature mismatch on archive.bin (cert: CN=db-root)",
    "[2025-10-27 12:15:07] DB: Log replay attack suspected: repeated sequence found (9834..9842) with different hashes",
    "[2025-10-27 12:15:12] DB: Hidden column 'x0r_exec' appears in table 'configs' (not in schema)",
    "[2025-10-27 12:15:20] DB: Process 'svc-updater' spawns only when observer disconnected",
    "[2025-10-27 12:15:28] DB: Encrypted blob detected in log: 'U2FsdGVkX1+Qp0...' (AES?)",
    "[2025-10-27 12:15:36] DB: Firmware signature mismatch on camera-feed#3 (reported ver 1.2.0 != expected 1.1.7)",
    "[2025-10-27 12:15:44] DB: Persistent backdoor: auth token reused across sessions (token id: tk-77C)",
    "[2025-10-27 12:15:52] DB: Rootkit indicator: /dev/pts/hidden_fd opened by kernel module 'kmod_x' ",
    "[2025-10-27 12:15:58] DB: Certificate revoked but connection accepted: CN=svc-node-9",
    "[2025-10-27 12:16:05] DB: Replay timestamp insertion: events reordered to hide action at 12:14:33"
};

        string[] textosDificiles2 = {
    "[2025-10-27 12:20:01] DB: APT telemetry: lateral movement signature matches IOC set 'night-owl-v2'",
    "[2025-10-27 12:20:09] DB: NTP poisoning chain: multiple servers report conflicting stratum",
    "[2025-10-27 12:20:17] DB: Covert channel detected: low-frequency timing pattern between nodes 4 & 9",
    "[2025-10-27 12:20:25] DB: Signed manifesto injected in logs: '/* kernel hook enabled */' (obfuscated)",
    "[2025-10-27 12:20:33] DB: Hardware trojan suspicion: unexpected firmware write to EEPROM",
    "[2025-10-27 12:20:41] DB: Private key exfiltration pattern: chunks written to 'tmp/.cache' with steganography markers",
    "[2025-10-27 12:20:49] DB: Time-based polymorphic payload executed at leap-second boundary",
    "[2025-10-27 12:20:57] DB: Command-and-Control beacon encoded inside seemingly benign telemetry",
    "[2025-10-27 12:21:05] DB: Multi-vector persistence: BIOS write + kernel module + scheduled task match",
    "[2025-10-27 12:21:13] DB: Log fabricator active: synthetic entries injected with forged HMACs"
};

        int choise = UnityEngine.Random.Range(0, 3);
        switch (choise)
        {
            case 0:
                Debug.Log("asignado");
                AsignarTextosAleatorios(textos1);
                break;
            case 1:
                Debug.Log("asignado");
                AsignarTextosAleatorios(textos2);
                break;
            case 2:
                Debug.Log("asignado");
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
