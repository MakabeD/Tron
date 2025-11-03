using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class emergencyHUD : MonoBehaviour
{
    public static emergencyHUD Instance;
    public TextMeshProUGUI textMeshProUGUI;

    public GameObject panel;
    bool isblinking = false;
    public float blinkSpeed = 0.5f;

    public bool isactivated = false;
    public void activateAdvertisement()
    {
        if (!isactivated)
        {
            panel.SetActive(true);
            startBlinking();
            isactivated = true;
        }
    }

    public void deactivateAdvertisement()
    {
        if (isactivated)
        {
            stopBlinking();
            panel.SetActive(false);
            isactivated = false;
        }
    }
    public void EmergencyEventCheck(int EmergencyEventCount)
    {
        Debug.Log("Checkeando emergency event count");
        if (EmergencyEventCount == 0) deactivateAdvertisement();
        else activateAdvertisement();
    }

    void startBlinking()
    {
        if (!isblinking)
        {
            StartCoroutine(blinkText());
        }
    }
    void stopBlinking()
    {
        isblinking = false;
        textMeshProUGUI.enabled = true;
    }
    private IEnumerator blinkText()
    {
        isblinking = true;
        while (isblinking)
        {
            textMeshProUGUI.enabled = !textMeshProUGUI.enabled;
            yield return new WaitForSeconds(blinkSpeed);
        }
    }

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
    // Start is called before the first frame update
    void Start()
    {
        panel.SetActive(false);
        EventManager.Instance.OnEmergencyEventCountChanged += EmergencyEventCheck;
    }

    // Update is called once per frame
    void Update()
    {


    }
}
