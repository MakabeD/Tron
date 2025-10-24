using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactionBase : MonoBehaviour
{
    LayerMask mask;
    public float distance = 1.5f;

    public Texture2D pointer;
    public GameObject textDetected;
    GameObject lastTaken=null;


    // Start is called before the first frame update
    void Start()
    {
        mask = LayerMask.GetMask("rayCastD");    
        textDetected.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //raycast(origen, direccion, out hit, distancia, mascara)
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, distance, mask))
        {
            Deselected();
            selectedObject(hit.transform);
            if (hit.collider.tag == "interactable")
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    //hit.collider.transform.GetComponent<script>().funcion();
                    hit.collider.transform.GetComponent<testCubeInteraction>().interact();

                }
            }
        }
        else
        {
            Deselected() ; 
        }
    }

    void selectedObject(Transform transform)
    {
        transform.GetComponent<MeshRenderer>().material.color = Color.green;
        lastTaken = transform.gameObject;
    }
    void Deselected()
    {
        if (lastTaken)
        {
            lastTaken.GetComponent<Renderer>().material.color = Color.white;
            lastTaken=null;

        }
    }
     void OnGUI()
    {

        Rect rect=new Rect(Screen.width/2, Screen.height/2, pointer.width, pointer.height);
        GUI.DrawTexture(rect, pointer);

        if(lastTaken)
        {
            textDetected.SetActive(true);
        }
        else
        {
            textDetected.SetActive(false);
        }
    }
}
