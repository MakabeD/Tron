using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testCubeInteraction : interactibeObject
{
    public override void interact()
    {
        Destroy(gameObject);
    }
}
