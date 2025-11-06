using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// IEventFactory.cs
public interface IEventFactory
{
    /// <summary>
    /// Crea un evento según la clave/eventKey y parámetros 
    /// </summary>
    Event Create(string eventKey, float probability);
}
