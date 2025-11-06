using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FirewallMinigame : MonoBehaviour, IMinigame
{
    FirewallLogStructure[] firewallLogStructures1, firewallLogStructures2;
    public GameObject[] gameObjects;
    public GameObject[] indicators;
    public FirewallLogFactory factory;
    public int MainCounter=0;
    public int GoodTry=0;
    int id = 1;
    public void EndMinigame() 
    {
        Debug.Log("saliendo");
    }

    public void OnLose()
    {
        //havelose = true;
        if(!EventManager.Instance.GetEvent(id).EventEmergency)
        {
            EventManager.Instance.IncrementEmergencyEventCount();
            GameManager.Instance.isinCombat++;
        EventManager.Instance.GetEvent(id).EventEmergency = true;
            Debug.Log("emergencia por perder");
        }
            GameHP.Instance.GetDownLife((MainCounter - GoodTry) * 5);
            MinigameManager.Instance.EndMinigame();
        
    }

    public void OnWin()
    {
        EventManager.Instance.GetEvent(id).StopEventExecution();
        MinigameManager.Instance.EndMinigame();
        Debug.Log("Ganaste");
    }

    public void StartMinigame()
    {
        Initialize();
        hideTexts();
    }
    public void hideTexts()
    {
        foreach (GameObject obj in indicators)
        {
            if (obj == null) continue;

            TextMeshProUGUI tmp = obj.GetComponent<TextMeshProUGUI>();
            if (tmp != null)
            {
                Color colorActual = tmp.color;
                colorActual.a = 0f; 
                tmp.color = colorActual;
            }
            else
            {
                TextMeshPro tmp3D = obj.GetComponent<TextMeshPro>();
                if (tmp3D != null)
                {
                    Color colorActual = tmp3D.color;
                    colorActual.a = 0f;
                    tmp3D.color = colorActual;
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        if(FirewallRoomController.Instance.getEmergencyTimer() >= 50) OnLose();
    }
    void Initialize()
    {
        factory = new FirewallLogFactory();
        firewallLogStructures1 = new FirewallLogStructure[]{
    // Ejemplo 1 — DNS legítimo (permitido)
    factory.CrearLog("TCP 192.168.1.45:49213 -> 172.217.3.110:80 [GET /index.html]", true,
        "Petición HTTP típica desde una IP interna hacia un servidor externo en puerto 80; comportamiento esperado."),
    // Ejemplo 2 — Petición HTTP normal (permitido)
    factory.CrearLog("TCP 192.168.1.45:49213 -> 172.217.3.110:80 [GET /index.html]", true,
        "Solicitud HTTP estándar (GET) sobre puerto 80; no se observan parámetros ni patrones sospechosos."),
    // Ejemplo 3 — Intento de SSH desde Internet (bloquear)
    factory.CrearLog("TCP 203.0.113.45:55441 -> 10.0.0.12:22 [Intento de conexión SSH externa]", false,
        "Intento de SSH desde una IP pública hacia un host interno en puerto 22: acceso remoto no autorizado, riesgo de intrusión."),
    // Ejemplo 4 — Escaneo de puertos (bloquear)
    factory.CrearLog("TCP 198.51.100.77:40000 -> 10.0.0.5:22,23,80,443 [Patrón de escaneo detectado]", false,
        "Conexiones simultáneas a múltiples puertos desde la misma IP: patrón típico de escaneo/sondeo de red; comportamiento malicioso.")
};

        firewallLogStructures2 = new FirewallLogStructure[] {
    // Ejemplo 5 — Conexión HTTPS legítima (permitido)
    factory.CrearLog("TCP 192.168.1.10:51532 -> 142.250.72.110:443 [Sesión HTTPS a Google]", true,
        "Sesión HTTPS saliente a IP conocida en puerto 443; tráfico cifrado esperado y origen interno."),
    // Ejemplo 6 — Exfiltración de datos (bloquear)
    factory.CrearLog("TCP 10.0.0.9:443 -> 54.23.12.99:443 [Flujo continuo de 500MB fuera del horario laboral]", false,
        "Tráfico continuo y voluminoso (500MB) hacia IP externa fuera de horario laboral: posible exfiltración de datos."),
    // Ejemplo 7 — Tráfico ICMP inusual (bloquear)
    factory.CrearLog("ICMP 203.0.113.25 -> 10.0.0.2 [Paquetes ICMP repetidos cada 2s, posible ping flood]", false,
        "Paquetes ICMP repetidos a intervalos cortos: patrón coherente con ping-flood/DoS o sondeo intensivo; bloquear y analizar."),
    // Ejemplo 8 — Conexión RDP interna legítima (permitido)
    factory.CrearLog("TCP 10.0.0.5:54022 -> 10.0.0.8:3389 [Sesión RDP interna autorizada]", true,
        "Sesión RDP entre hosts dentro de la red interna por puerto estándar 3389; corresponde a acceso remoto autorizado.")
};


        int choise = UnityEngine.Random.Range(0, 2);
        switch (choise)
        {
            case 0:
                Debug.Log("asignado");
                
                AsignarTextosAleatorios(firewallLogStructures1);
                break;
            case 1:
                Debug.Log("asignado");
                
                AsignarTextosAleatorios(firewallLogStructures2);
                break;
            
            default:
                Debug.Log("Fallo en la matrix");
                break;
        }

    }
    public void isPermitted(int id)
    {
        if (gameObjects[id].GetComponent<FirewallLogData>().touched) return;

        MainCounter++;
        
        gameObjects[id].GetComponent<FirewallLogData>().touched = true;
        if (!gameObjects[id].GetComponent<FirewallLogData>().ispermitted)
        {
            indicators[id].GetComponent<TextMeshProUGUI>().color = Color.red;
            FeedbackController.Instance.firewallLogDatas.Add(gameObjects[id].GetComponent<FirewallLogData>());


        }
        else { indicators[id].GetComponent<TextMeshProUGUI>().color = Color.green; GoodTry++;}

        if(MainCounter>=4 && GoodTry >= 3)
        {
            OnWin();
        }else if(MainCounter>=4) OnLose();

    }
    public void isNotPermitted(int id)
    {
        if (gameObjects[id].GetComponent<FirewallLogData>().touched) return;
        MainCounter++;
        gameObjects[id].GetComponent<FirewallLogData>().touched = true;
        if (gameObjects[id].GetComponent<FirewallLogData>().ispermitted)
        {
            indicators[id].GetComponent<TextMeshProUGUI>().color = Color.red;
            FeedbackController.Instance.firewallLogDatas.Add(gameObjects[id].GetComponent<FirewallLogData>());


        }
        else{ indicators[id].GetComponent<TextMeshProUGUI>().color = Color.green; GoodTry++; }
        

        if (MainCounter >= 4 && GoodTry >= 3)
        {
            OnWin();
        }
        else if (MainCounter >= 4) OnLose();

    }
    void AsignarTextosAleatorios(FirewallLogStructure[] textos)
    {
        if (textos.Length < gameObjects.Length)
        {
            Debug.LogError("No hay suficientes textos para todos los objetos.");
            return;
        }

        // 1️⃣ Mezclamos el array de textos (algoritmo Fisher–Yates)
        FirewallLogStructure[] textosMezclados = (FirewallLogStructure[])textos.Clone();
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
                var logData = gameObjects[i].GetComponent<FirewallLogData>();
                if (logData == null)
                    logData = gameObjects[i].AddComponent<FirewallLogData>();

                // Guarda los datos de ese texto
                logData.texto = textosMezclados[i].text;
                logData.ispermitted = textosMezclados[i].isPermitted;
                logData.feedback = textosMezclados[i].feedback;
            }
            else
            {
                Debug.LogWarning($"El objeto {gameObjects[i].name} no tiene un componente TextMeshPro.");
            }
        }

    }

}

