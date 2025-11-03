using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using TMPro;
using UnityEngine;

public class SuspiciousActivity : MonoBehaviour
{
    public static SuspiciousActivity Instance;
    public TextMeshProUGUI textMeshProUGUI;
    
    public GameObject panel;
    bool isblinking=false;
    public float blinkSpeed=0.5f;

    public bool isactivated=false;
    public void activateAdvertisement()
    {
        if (!isactivated)
        {
            panel.SetActive(true);
            startBlinking();
            isactivated= true;
        }
    }

    public void deactivateAdvertisement()
    {
        if(isactivated)
        {
            stopBlinking();
            panel.SetActive(false);
            isactivated= false;
        }
    }
    public void EventCountCheck(int EventCount)
    {
        Debug.Log("Checkeando event count");
        if (EventCount == 0) deactivateAdvertisement();
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
        EventManager.Instance.OnEventCountChanged += EventCountCheck;
    }

    // Update is called once per frame
    void Update()
    {
        

    }
}
