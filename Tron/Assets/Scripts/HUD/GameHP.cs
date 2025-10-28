using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameHP : MonoBehaviour
{
    public static GameHP Instance;
    public int HPs =100;

    public TextMeshProUGUI textMeshPro;

    
    public void GetDownLife(int life)
    {
        HPs-=life;
        textMeshPro.text = string.Format("{0}%", HPs);
    }
    void Awake()
    {
        if (Instance == null) { Instance = this; DontDestroyOnLoad(gameObject); }
        else { Destroy(gameObject); return; }

        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
